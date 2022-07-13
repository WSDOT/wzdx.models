using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Wsdot.Wzdx.Models.Tests.Core
{
    internal class CacheMessageHandler : DelegatingHandler
    {
        private static readonly ConcurrentDictionary<string, byte[]> ContentCache = new();

        public CacheMessageHandler() : base(new SocketsHttpHandler())
        {
            
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = request.RequestUri?.ToString() ?? throw new NullReferenceException("");

            if (ContentCache.ContainsKey(uri))
            {
                return ContentResponse(uri);
            }

            var response = await base.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode) return response;

            // 
            var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            
            ContentCache.TryAdd(uri, content);
            return ContentResponse(uri);
        }

        private static HttpResponseMessage ContentResponse(string key)
        {
            var content = ContentCache[key];
            
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(new MemoryStream(content))
            };
        }
    }
}