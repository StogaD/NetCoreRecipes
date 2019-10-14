//#define EnableKeyVaultAccess
#if EnableKeyVaultAccess
  //  #define ManagedIdentities
#endif

//When host on Azure enable ManagedIdentities 
//If run local or host outside Azure use cert solution

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
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
#if (EnableKeyVaultAccess)
    #if (ManagedIdentities)
                var azureServiceTokenProvider = new AzureServiceTokenProvider();

                var keyVaultClient = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(
                        azureServiceTokenProvider.KeyVaultTokenCallback));

                var keyVaultName = configuration["KeyVaultOptions:KeyVaultName"];
                var vaultUrl = $"https://{keyVaultName}.vault.azure.net/";


                con.AddAzureKeyVault(
                           vaultUrl,
                           keyVaultClient,
                           new DefaultKeyVaultSecretManager());

#else


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
    #endif
#endif
            })
                    .UseStartup<Startup>();
    }
}
