using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PollyDemo.Polly;
using PollyDemo.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace PollyDemo
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
            services.AddHttpClient<IProtectedService, ProtectedService>();

            services.AddHttpClient<IPhotoService, PhotoService>(options =>
           {
               options.BaseAddress = new Uri("https://testpolly.free.beeceptor.com");
               //create own rules in beeceptor ie. return 408 when url ends with /api/photos/408 

           })
           .SetHandlerLifetime(TimeSpan.FromMinutes(5)) //default is 2
           .AddPolicyHandler((sp,req) => new CustomPolicies().GetRetryPolicyWithLogging(sp,req));
                     
           services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",new Info
                    {  Description = "Polly Demo API",  Title = "My API", Version = "Version 1"});
            });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Demo API");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
