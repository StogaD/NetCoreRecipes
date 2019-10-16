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
    public interface IDemoProxyHandler
    {
        bool IsProxyRequest(HttpRequest request);
        Task<HttpResponseMessage> Execute(HttpContext context);
    }

    public class DemoProxyHandler : IDemoProxyHandler
    {
        private readonly ProxyKitOptions _configs;
        public DemoProxyHandler(IOptions<ProxyKitOptions> config)
        {
            _configs = config.Value;
        }
        public Task<HttpResponseMessage> Execute(HttpContext context)
        {
            var forwardContext = context.ForwardTo(_configs.Host);
            var response = forwardContext.Send();
            return response;
        }

        public bool IsProxyRequest(HttpRequest request)
        {
            return request.Path.Value.Contains("api/Authors/102", StringComparison.OrdinalIgnoreCase);
        }
    }
}
