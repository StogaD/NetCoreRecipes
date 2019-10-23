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

        public static IApplicationBuilder UseFactoryBasedMiddleware(this IApplicationBuilder builder, string options)
        {
            // Passing 'option' as an argument throws a NotSupportedException at runtime.
            return builder.UseMiddleware<FactoryActivatedMiddleware>();
        }
    }
}
