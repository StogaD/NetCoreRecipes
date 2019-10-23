using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiddlewareDemo.Middlewares
{
    public class FactoryActivatedMiddleware : IMiddleware
    {
        private readonly string _options;
        public FactoryActivatedMiddleware()
        {
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next.Invoke(context);
        }
    }
}
