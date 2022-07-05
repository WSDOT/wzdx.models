using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Wsdot.Wzdx.Models.Tests.Core
{
    internal class CacheMessageHandler : DelegatingHandler
    {
        private readonly string _cachePath;

        public CacheMessageHandler() : this(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache))
        {

        }

        private CacheMessageHandler(string cachePath) :
            base(new SocketsHttpHandler())
        {
            _cachePath = cachePath;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = request.RequestUri ?? throw new NullReferenceException("");
            var path = Path.Combine(_cachePath, string.Join('-', uri.Segments).Replace("/", "-").Replace(".", "_"));

            if (File.Exists(path))
            {
                return FileContentResponse(path);


            }

            var response = await base.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode) return response;

            // 
            var content = await response.Content.ReadAsStreamAsync(cancellationToken);
            var reader = new StreamReader(content);
            await using var writer = new StreamWriter(File.OpenWrite(path));

            await writer.WriteAsync(await reader.ReadToEndAsync());
            await writer.FlushAsync();
            writer.Close();

            return FileContentResponse(path);
        }

        private HttpResponseMessage FileContentResponse(string path)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(File.Open(path, FileMode.Open, FileAccess.Read,
                    FileShare.Delete | FileShare.ReadWrite))
            };
        }
    }
}