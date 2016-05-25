using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace FtpTest
{
    public class State<TRequest, TResponse>
        where TRequest : WebRequest
        where TResponse : WebResponse
    {
        private readonly Stopwatch sw = new Stopwatch();

        private readonly DateTime start = DateTime.UtcNow;

        public int BytesRead { get; set; }

        public long TotalBytes { get; set; }

        public Stream StreamResponse { get; set; }

        public byte[] BufferRead { get; set; }

        public Uri Uri { get; set; }

        public string Method { get; set; }

        public Action<int, double, double, TimeSpan> Progress { get; set; }

        public Action Done { get; set; }

        public TRequest Request { get; set; }

        public TResponse Response { get; set; }

        public double TotalTime
        {
            get
            {
                return sw.Elapsed.TotalMilliseconds;
            }
        }

        public State(int buffSize)
        {
            BufferRead = new byte[buffSize];
        }

        public void Start()
        {
            sw.Start();
        }

        public void Stop()
        {
            sw.Stop();
            Done();
        }
    }
}
