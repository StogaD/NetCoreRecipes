using HttpClientFactoryDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Services
{
    public interface IPhotoService
    {
        Task<Photo> GetPhotoItem(int id);
        Task<IEnumerable<Photo>> GetPhotos();
    }
    public class PhotoServiceWithNamedClient : IPhotoService
    {
        private readonly IHttpClientFactory _clientFactory;

        public PhotoServiceWithNamedClient(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }
        public async Task<Photo> GetPhotoItem(int page)
        {
            string _remoteServiceBaseUrl = "https://jsonplaceholder.typicode.com/photos";

            var httpClient = _clientFactory.CreateClient("photos");

            //will overwrite the default base url from Startup.cs
            var responseString = await httpClient.GetStringAsync($"{_remoteServiceBaseUrl}/{page}");
            var photo = JsonConvert.DeserializeObject<Photo>(responseString);

            return photo;
        }

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            var httpClient = _clientFactory.CreateClient("photos");

            // create request explicitly
            var request = new HttpRequestMessage(HttpMethod.Get, "/photos");
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Photo>>();
            }
            return null;
        }
    }
}
