using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiddlewareDemo.Middlewares
{
    public class ConventionalMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly string _options;
        public ConventionalMiddleware(RequestDelegate next, string options)
        {
            _next = next;
            _options = options;
        }

        public Task Invoke(HttpContext httpContext)
        {
            return _next(httpContext);
        }
    }
}
