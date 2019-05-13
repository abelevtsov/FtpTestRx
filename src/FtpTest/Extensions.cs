using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;

namespace FtpTest
{
    public static class Extensions
    {
        public static IObservable<byte[]> ReadRx(this Stream stream, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            var asyncRead = stream.ReadRx();
            return Observable
                .While(
                    () => stream.CanRead,
                    Observable
                        .Defer(() => asyncRead(buffer, 0, bufferSize))
                        .Select(readBytes => buffer.Take(readBytes).ToArray()));
        }

        public static IObservable<WebResponse> GetResponseRx(this WebRequest webRequest) =>
            Observable.FromAsyncPattern(webRequest.BeginGetResponse, webRequest.EndGetResponse)();

        public static IObservable<Unit> WriteRx(this Stream stream, byte[] buffer) =>
            Observable.FromAsyncPattern<byte[], int, int>(stream.BeginWrite, stream.EndWrite)(buffer, 0, buffer.Length);

        private static Func<byte[], int, int, IObservable<int>> ReadRx(this Stream stream) =>
            Observable.FromAsyncPattern<byte[], int, int, int>(stream.BeginRead, stream.EndRead);
    }
}
