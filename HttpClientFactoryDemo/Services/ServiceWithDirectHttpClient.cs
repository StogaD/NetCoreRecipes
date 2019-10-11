using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Services
{
    public interface IRepoService
    {
        Task<object> GetFromRepository(int id);
        Task WriteToRepository(int id);
    }

   ///<remarks>
   /// It was written only to show what is the bad practice. Use HttpClientFactory (direct, named or typed client instead)
   ///</remarks>

    [Obsolete("Do not use in this way - use clientFactory instead")]
    public class ServiceWithDirectHttpClient : IRepoService
    {
        private readonly HttpClient _httpClient;
        public ServiceWithDirectHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<object> GetFromRepository(int id)
        {
           return await _httpClient.GetStringAsync($"http://www.foo.com/api/album/{id}");
        }

        public async Task WriteToRepository(int id)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://www.foo.com/api/album");

               await client.SendAsync(request);
            }
        }
    }
}
