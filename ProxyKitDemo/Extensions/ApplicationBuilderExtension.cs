using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using ProxyKitDemo.ProxyHandler;
using Microsoft.Extensions.DependencyInjection;
using ProxyKit;

namespace ProxyKitDemo.Extensions
{
    public  static class ApplicationBuilderExtension
    {
        public static void UseAuthorsProxy(this IApplicationBuilder builder)
        {
            IDemoProxyHandler handler = null;

            builder.UseWhen(
                context =>
                {

                    handler = context.RequestServices.GetService<IDemoProxyHandler>();
                    return handler.IsProxyRequest(context.Request);
                },
                appInner =>
                {
                    appInner.RunProxy(
                        async ctx =>
                        {
                            return await handler.Execute(ctx);
                        });
                });
        }
    }
}
