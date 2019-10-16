using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProxyKit;
using ProxyKitDemo.Configuration;

namespace ProxyKitDemo.ProxyHandler
{
    public class BooksProxyHandler : IProxyHandler
    {
        private readonly ProxyKitOptions _configs;
        public BooksProxyHandler(IOptions<ProxyKitOptions> config)
        {
            _configs = config.Value;
        }
        public Task<HttpResponseMessage> HandleProxyRequest(HttpContext context)
        {
            var forwardContext = context.ForwardTo(_configs.Host);
            var response = forwardContext.Send();
            return response;
        }
    }
}
