using System;
using System.Diagnostics;
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
                
                var uri = new Uri(txtURI.Text);
                if (uri.Scheme == Uri.UriSchemeFtp)
                {
                    var frmCreds = new CredentialsForm();
                    var result = frmCreds.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }

                    ICredentials creds;
                    if (!(string.IsNullOrEmpty(frmCreds.Username) || string.IsNullOrEmpty(frmCreds.Password)))
                    {
                        creds = new NetworkCredential(frmCreds.Username, frmCreds.Password);
                    }
                    else
                    {
                        creds = null;
                    }

                    var observableWebResponses = (from url in Observable.Return(uri)
                                                 let req = CreateRequest(url, WebRequestMethods.Ftp.GetFileSize, creds, false, false)
                                                 from response in req.GetResponseRx()
                                                 let contentLength = response.ContentLength
                                                 let req1 = CreateRequest(url, WebRequestMethods.Ftp.DownloadFile, creds, true, true)
                                                 from response1 in req1.GetResponseRx()
                                                 let responseStream = response1.GetResponseStream()
                                                 from buffer in responseStream.ReadRx(BufferSize)
                                                                              .ObserveOn(SynchronizationContext.Current)
                                                                              .TakeWhile(returnBuffer => returnBuffer.Length > 0)
                                                                              .Do(_ => Debug.WriteLine("L = {0}", _.Length))
                                                  select buffer).Publish();
                    var fs = new FileStream(@"D:\test.txt", FileMode.Create, FileAccess.Write, FileShare.Write, BufferSize, FileOptions.Asynchronous);
                    var d = observableWebResponses.Connect();
                    observableWebResponses
                        .Subscribe(
                            buffer =>
                                fs.WriteRx(buffer),
                            ex =>
                                MessageBox.Show(ex.Message),
                            () => Done(fs.Dispose, d.Dispose));
                }
                else
                {
                    MessageBox.Show("URL must start from ftp://");
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(string.Format("Error requesting web page: {0}", ex.Message));
            }
            catch (IOException ex)
            {
                MessageBox.Show(string.Format("Error writing results to file: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("An error occured: {0}", ex.Message));
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Init();
        }

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

        private static FtpWebRequest CreateRequest(Uri uri, string method, ICredentials creds, bool keepAlive, bool useBinary)
        {
            var request = (FtpWebRequest)WebRequest.Create(uri);
            if (creds != null)
            {
                request.Credentials = creds;
            }

            request.KeepAlive = keepAlive;
            request.UseBinary = useBinary;
            request.Method = method;

            var myProxy = new WebProxy();
            request.Proxy = myProxy;

            return request;
        }
    }
}
