using Newtonsoft.Json;
using PollyDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.Services
{
    public interface IPhotoService
    {
        Task<Photo> GetPhoto(int id);
        Task<IEnumerable<Photo>> GetPhotos();
    }
    public class PhotoService : IPhotoService
    {
        private readonly HttpClient _httpClient;

        public PhotoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Photo> GetPhoto(int id)
        {
            // create request explicitly
            var request = new HttpRequestMessage(HttpMethod.Get, $"/photos/{id}");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Photo>();
            }
            return null;
        }

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            // create request explicitly
            var request = new HttpRequestMessage(HttpMethod.Get, "/photos");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Photo>>();
            }
            return null;
        }
    }
}
