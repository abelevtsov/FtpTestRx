using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FtpTest
{
    public partial class MainForm : Form
    {
        private const int BufferSize = 4096;

        private readonly Stopwatch stopwatch = new Stopwatch();

        public MainForm()
        {
            InitializeComponent();

            lblDownloadComplete.Visible = false;
        }

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            try
            {
                lblDownloadComplete.Visible = false;

                var totalBytesRead = 0;
                var uri = new Uri(txtURI.Text);
                if (uri.Scheme == Uri.UriSchemeFtp)
                {
                    var frmCredentials = new CredentialsForm();
                    var result = frmCredentials.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }

                    ICredentials credentials;
                    if (string.IsNullOrEmpty(frmCredentials.Username) ||
                        string.IsNullOrEmpty(frmCredentials.Password))
                    {
                        credentials = null;
                    }
                    else
                    {
                        credentials = new NetworkCredential(frmCredentials.Username, frmCredentials.Password);
                    }

                    var observableWebResponses =
                            (from url in Observable.Return(uri)
                             let fileSizeRequest = CreateRequest(url, WebRequestMethods.Ftp.GetFileSize, credentials, false, false)
                             from fileSizeResponse in fileSizeRequest.GetResponseRx()
                             let fileSize = fileSizeResponse.ContentLength
                             let downloadFileRequest = CreateRequest(url, WebRequestMethods.Ftp.DownloadFile, credentials, true, true)
                             from downloadFileResponse in downloadFileRequest.GetResponseRx()
                             let responseStream = downloadFileResponse.GetResponseStream()
                             from buffer in responseStream.ReadRx(BufferSize)
                                                          .ObserveOn(SynchronizationContext.Current)
                                                          .TakeWhile(b => b.Length > 0)
                                                          .Do(
                                                              b =>
                                                              {
                                                                  totalBytesRead += b.Length;
                                                                  var completePercent = totalBytesRead / (double)fileSize * 100.0;
                                                                  var totalTime = stopwatch.Elapsed.TotalMilliseconds;
                                                                  var kbPerSec = totalBytesRead * 1000.0 / (totalTime * 1024.0);
                                                                  var estimatedTime = TimeSpan.FromMilliseconds(((double)fileSize - totalBytesRead) / kbPerSec);
                                                                  ReportProgress(totalBytesRead, completePercent, kbPerSec, estimatedTime);
                                                              })
                             select buffer).Publish();
                    var fs = GetFileStream($@"C:\temp\{Path.GetFileName(txtURI.Text)}");
                    var d = observableWebResponses.Connect();
                    stopwatch.Start();
                    observableWebResponses
                        .Subscribe(
                            buffer => fs.WriteRx(buffer),
                            ex => Log(ex.Message),
                            () =>
                            {
                                stopwatch.Stop();
                                Done(fs.Dispose, d.Dispose);
                            });
                }
                else
                {
                    Log("URL must start from ftp://");
                }
            }
            catch (WebException ex)
            {
                Log($"Error requesting web page: {ex.Message}");
            }
            catch (IOException ex)
            {
                Log($"Error writing results to file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log($"An error occured: {ex.Message}");
            }
        }

        private void MainForm_Load(object sender, EventArgs e) => Init();

        private void Done(params Action[] disposables)
        {
            Array.ForEach(disposables, d => d());
            progressBar.Value = 0;
            lblDownloadComplete.Visible = true;
        }

        private void Init()
        {
            lblTimeLeft.Text = string.Empty;
            lblBytesRead.Text = string.Empty;
            lblRate.Text = string.Empty;
        }

        private void ReportProgress(int totalBytes, double percentComplete, double transferRate, TimeSpan estimated)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int, double, double, TimeSpan>(ReportProgress), totalBytes, percentComplete, transferRate, estimated);
            }
            else
            {
                lblTimeLeft.Text = estimated.ToString("g");
                lblBytesRead.Text = totalBytes.ToString(CultureInfo.InvariantCulture);
                progressBar.Value = (int)percentComplete;
                lblRate.Text = transferRate.ToString("F0");
            }
        }

        private static FtpWebRequest CreateRequest(Uri uri, string method, ICredentials credentials, bool keepAlive, bool useBinary)
        {
            var request = (FtpWebRequest)WebRequest.Create(uri);
            if (credentials != null)
            {
                request.Credentials = credentials;
            }

            request.KeepAlive = keepAlive;
            request.UseBinary = useBinary;
            request.Method = method;

            var myProxy = new WebProxy();
            request.Proxy = myProxy;

            return request;
        }

        private static FileStream GetFileStream(string path)
        {
            var directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            return new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, BufferSize, FileOptions.Asynchronous);
        }

        private static void Log(string message) => MessageBox.Show(message);
    }
}
