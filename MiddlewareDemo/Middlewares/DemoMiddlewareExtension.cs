using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace MiddlewareDemo.Middlewares
{
    public static class DemoMiddlewareExtension
    {
        public static IApplicationBuilder UseDemoMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DemoMiddleware>();
        }
    }
}
