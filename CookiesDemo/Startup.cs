using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CookiesDemo.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace CookiesDemo
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ReqNamePolicy",
                    policy => policy.RequireClaim("FullName", "spiderman", "Batman"));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
                {
                    opt.EventsType = typeof(CustomCookieAuthenticationEvents);
                });

            services.AddScoped<CustomCookieAuthenticationEvents>(sp => new CustomCookieAuthenticationEvents()
            {
                OnRedirectToLogin = (r) =>
                {
                    r.Response.StatusCode = 401;
                    return Task.FromResult(0);
                },
                OnRedirectToAccessDenied = (r) =>
                {
                    r.Response.StatusCode = 403;

                    
                    var byteArray = Encoding.ASCII.GetBytes("Use correct credentials from Readme.txt to test the demo");
                    r.Response.Body.Write(byteArray);
                    return Task.FromResult(0);
                }
            });

            services.AddSingleton(Log.Logger);
            services.AddSwaggerGen(c => c.SwaggerDoc("crv", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Demo Auth API" }));

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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/crv/swagger.json", "My Demo API");

            });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
