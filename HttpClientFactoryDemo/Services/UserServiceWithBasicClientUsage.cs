using HttpClientFactoryDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
    }
    public class UserServiceWithBasicClientUsage : IUserService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _remoteServiceBaseUrl = "https://jsonplaceholder.typicode.com/users";

        public UserServiceWithBasicClientUsage(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }
        public async Task<User> GetUser(int id)
        {
            var httpClient = _clientFactory.CreateClient();
            var responseString = await httpClient.GetStringAsync($"{_remoteServiceBaseUrl}/{id}");
            var user = JsonConvert.DeserializeObject<User>(responseString);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var httpClient = _clientFactory.CreateClient();
            var responseString = await httpClient.GetStringAsync(_remoteServiceBaseUrl);
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(responseString);

            return users;
        }
    }
}
