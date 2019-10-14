using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SecretDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((ctx, con) =>
            {
                var configuration = con.Build();

                var appId = configuration["KeyVaultOptions:AzureADApplicationId"];
                var keyVaultName = configuration["KeyVaultOptions:KeyVaultName"];
                var certThumprint = configuration["KeyVaultOptions:AzureADCertThumbprint"];

                var vaultUrl = $"https://{keyVaultName}.vault.azure.net/";

                //work only with CurrentUser cert. LocalMachine return 500.03 
                var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);
                var certs = store.Certificates.Find(X509FindType.FindByThumbprint, certThumprint, false);
                var cert = certs.OfType<X509Certificate2>().Single();

                con.AddAzureKeyVault(vaultUrl,
                                     appId,
                                     cert);

                store.Close();
    })
                    .UseStartup<Startup>();
    }
}
