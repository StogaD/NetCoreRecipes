using Microsoft.Extensions.Logging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Registry;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace PollyDemo.Polly
{
    public class CustomPolicies
    { 
        public IAsyncPolicy<HttpResponseMessage> GetWaitAndRetryPolicy()
        {
            return HttpPolicyExtensions
                  .HandleTransientHttpError() //50xx or 408 (timeout)
                  .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                  .WaitAndRetryAsync(6, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)));
        }
        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicyWithJitter()
        {
            Random jitterer = new Random();
            return HttpPolicyExtensions
                  .HandleTransientHttpError() //50xx or 408 (timeout)
                  .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                  .WaitAndRetryAsync(6, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp))
                  + TimeSpan.FromMilliseconds(jitterer.Next(0, 100))
                  );
        }
        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicyWithContribJitter()
        {
            var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5,fastFirst: true);

            return HttpPolicyExtensions
                  .HandleTransientHttpError() //50xx or 408 (timeout)
                  .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                  .WaitAndRetryAsync(delay);
        }
        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicyWithLogging(IServiceProvider provider, HttpRequestMessage request)
        {
            return HttpPolicyExtensions
                  .HandleTransientHttpError() //50xx or 408 (timeout)
                  .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                  .WaitAndRetryAsync(
                    new[]
                    {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(3)
                    },
                    onRetryAsync: async (outcome, timespan, retryAttempt, context) =>
                    {
                        var logger = provider.GetService<ILogger<HttpClient>>();
                        var logLevel = outcome.Exception != null ? LogLevel.Error : LogLevel.Warning;

                        logger.Log(
                            logLevel, outcome.Exception,
                            "Delaying for {delay}ms, then making retry {retry}.",
                            timespan.TotalMilliseconds,
                            retryAttempt);
                        await Task.CompletedTask;
                    });
                   
        }
        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicyHonourRetryAfter_429(IServiceProvider provider, HttpRequestMessage request)
        {
            return HttpPolicyExtensions
                  .HandleTransientHttpError() //50xx or 408 (timeout)
                  .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                  .WaitAndRetryAsync(3,
                    sleepDurationProvider: (retryAttemp, response, ctx) =>
                    {
                        var retryAfter = response?.Result?.Headers.RetryAfter;
                        if (retryAfter !=null && retryAfter.Date .HasValue)
                        {
                            return (retryAfter.Date.Value - DateTime.UtcNow);
                        }
                        return TimeSpan.Zero;

                        // Remark: Azure services i.e CosmosDb throw DocumentClientException and 429 Code
                        // need to be handled in other way
                    },
                    onRetryAsync: async (outcome, timespan, retryAttempt, context) =>
                    {
                        var logger = provider.GetService<ILogger<HttpClient>>();
                        var logLevel = outcome.Exception != null ? LogLevel.Error : LogLevel.Warning;

                        logger.Log(
                            logLevel, outcome.Exception,
                            "Delaying for {delay}ms, then making retry {retry}.",
                            timespan.TotalMilliseconds,
                            retryAttempt);
                        await Task.CompletedTask;
                    });

        }
    }
}
