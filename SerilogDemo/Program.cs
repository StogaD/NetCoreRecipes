using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using SerilogDemo.Logger;

namespace SerilogDemo
{
    public class Program
    {
        private static readonly string Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(typeof(Program).Assembly.Location))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Env}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();


        public static void Main(string[] args)
        {
            //var outputTemplate = "[{Timestamp:HH:mm:ss} --> {Level:u3}] {Message:lj}  {AppInfo}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                //.Enrich.WithAppInfo() //custom enricher
                //.Enrich.WithMachineName() //from nuGet
                //.WriteTo.Console(LogEventLevel.Verbose, outputTemplate)
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            Log.Information("Start Application {MachineName}");
            Log.Debug("Debug message");

            CreateWebHostBuilder(args).Build().Run();

            Log.CloseAndFlush();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
