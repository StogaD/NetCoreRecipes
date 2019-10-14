using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestEaseDemo.Model;
using RestEaseDemo.Repository;
using RestEaseDemo.Services;

namespace RestEaseDemo
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

            var baseUrl = "https://jsonplaceholder.typicode.com";
            var restClient = RestEase.RestClient.For<ICommentRepository>(baseUrl);

            //services.AddHttpClient("restEasyClient", options =>
            //{
            //    options.BaseAddress = new Uri(baseUrl);
            //});

            //using scope - not convinient way but it works
            //using (var scope = services.BuildServiceProvider().CreateScope())
            //{
            //    var clinetFactory = scope.ServiceProvider.GetService<IHttpClientFactory>();
            //    var httpClientPost = clinetFactory.CreateClient("restEasyClient");


            //    var restClientBlog = RestEase.RestClient.For<IBlogRepository>(httpClientPost);

            //    services.AddSingleton<IBlogRepository>(restClientBlog);
            //}

            services.AddHttpClient("restEasyClient_v2", c =>
            {
                c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            })
            //. Polly can be also integrated
            .AddTypedClient(c => RestEase.RestClient.For<IBlogRepository>(c));


        services.AddSingleton<ICommentRepository>(p => restClient);

            services.AddTransient<ICommentsService, CommentsService>();


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
