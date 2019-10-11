using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigDemo.ConfigDemo.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConfigDemo
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
            var url = Configuration.GetValue<string>("DescriptionUrl");

            var defaultUrl = Configuration.GetValue<string>("url", "http://localhost:9200");

            var speed = Configuration.GetValue<int>("Parameters:Speed");

            var section = Configuration.GetSection("Parameters");

            services.AddOptions<Parameters>().Configure(o => o.Speed = 2);

            services.Configure<Parameters>(Configuration.GetSection("Parameters"));

            services.PostConfigure<Parameters>(x => x.Speed = x.Speed * 2);

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var paramtersRetrivedFromIOptions = scope.ServiceProvider.GetService<IOptions<Parameters>>();
            }

            if (section.Exists())
            {
                var getValue = section.GetValue(typeof(int), "Speed");

                var bindValue = new Parameters();
                section.Bind(bindValue);
            };

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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
