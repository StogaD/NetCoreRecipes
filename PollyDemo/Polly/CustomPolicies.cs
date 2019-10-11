using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.Polly
{
    public class CustomPolicies
    {
        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                  .HandleTransientHttpError() //50xx or 408 (timeout)
                  .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                  .WaitAndRetryAsync(6, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)));
        }
    }
}
