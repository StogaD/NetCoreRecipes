using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using PollyDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.Services
{
    /// <summary>
    /// Based on https://www.jerriepelser.com/blog/refresh-google-access-token-with-polly/
    /// </summary>
    public interface IProtectedService
    {
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetWithTokenRefresh(string accessToken, string refreshToken, Func<string, Task> tokenRefreshed);
    }
    public class ProtectedService : IProtectedService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProtectedService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            //get from database
            string accessToken = "...";
            string refreshToken = "...";

            return await (this as IProtectedService).GetWithTokenRefresh(accessToken, refreshToken, async token =>
            {
                // Update access token in the database
                // ...
                await Task.CompletedTask;
            });
        }

        /// <summary>
        /// Use refresh token to get access token when expired during the call to repository.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <param name="tokenRefreshed"></param>
        /// <returns></returns>
        async Task<Photo> IProtectedService.GetWithTokenRefresh(string accessToken, string refreshToken, Func<string, Task> tokenRefreshed)
        {
            var policy = CreateTokenRefreshPolicy(tokenRefreshed);

            var response = await policy.ExecuteAsync(context =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/drive/v3/files");
                requestMessage.Headers.Add("Authorization", $"Bearer {context["access_token"]}");

                return _httpClient.SendAsync(requestMessage);
            }, new Dictionary<string, object>
        {
            {"access_token", accessToken},
            {"refresh_token", refreshToken}
        });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Photo>();
        }

        private AsyncRetryPolicy<HttpResponseMessage> CreateTokenRefreshPolicy(Func<string, Task> tokenRefreshed)
        {
            var policy = Policy
                        .HandleResult<HttpResponseMessage>(message => message.StatusCode == HttpStatusCode.Unauthorized)
                        .RetryAsync(1, async (result, retryCount, context) =>
                        {
                            if (context.ContainsKey("refresh_token"))
                            {
                                var newAccessToken = await RefreshAccessToken(context["refresh_token"].ToString());
                                if (newAccessToken != null)
                                {
                                    await tokenRefreshed(newAccessToken);

                                    context["access_token"] = newAccessToken;
                                }
                            }
                        });

            return policy;
        }

        private  async Task<string> RefreshAccessToken(string refreshToken)
        {
            var refreshMessage = new HttpRequestMessage(HttpMethod.Post, "/oauth2/v4/token")
            {
                Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                        {
                new KeyValuePair<string, string>("client_id", _configuration["Authentication:Google:ClientId"]),
                new KeyValuePair<string, string>("client_secret", _configuration["Authentication:Google:ClientSecret"]),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
                new KeyValuePair<string, string>("grant_type", "refresh_token")
                        })
            };

            var response = await _httpClient.SendAsync(refreshMessage);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadAsAsync<TokenResponse>();

                return tokenResponse.AccessToken;
            }

            // return null if we cannot request a new token
            return null;
        }
    }
}
