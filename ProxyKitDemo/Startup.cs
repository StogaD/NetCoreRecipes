using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProxyKit;
using ProxyKitDemo.Configuration;
using ProxyKitDemo.ProxyHandler;

namespace ProxyKitDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ProxyKitOptions>(Configuration.GetSection("ProxyKit"));

            services.AddProxy();
 
            services.AddSingleton<BooksProxyHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.RunProxy(ProxyHandler);
            // or use class implemented IProxyHandler

            app.MapWhen(BooksWanted, RunProxyKit);
 
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private bool BooksWanted(HttpContext ctx)
        {
            return ctx.Request.Path.Value.Contains("api/Book", StringComparison.OrdinalIgnoreCase);
        }

        private void RunProxyKit(IApplicationBuilder obj)
        {
            obj.RunProxy<BooksProxyHandler>();
        }

        private Task<HttpResponseMessage> ProxyHandler(HttpContext httpContext)
        {
            string host = Configuration["ProxyKit:Host"];
            var forwardContext = httpContext.ForwardTo(host);
            var response = forwardContext.Send();
            return response;
        }
    }
}
