using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace MiddlewareDemo.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDemoMiddleware(this IApplicationBuilder builder, string options)
        {
            return builder.UseMiddleware<ConventionalMiddleware>(options);
        }

        public static IApplicationBuilder UseNewiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FactoryActivatedMiddleware>();
        }
    }
}
