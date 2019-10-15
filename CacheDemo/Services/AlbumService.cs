using CacheDemo.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CacheDemo.Services
{
    public interface IAlbumService
    {
        Task<Album> GetAlbumItem(int id);
        Task<IEnumerable<Album>> GetAlbums();
    }
    public class AlbumService : IAlbumService
    {
        private readonly HttpClient _httpClient;
 
        public AlbumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            return await CallGetApi<IEnumerable<Album>>("/albums");
        }

        public async Task<Album> GetAlbumItem(int id)
        {
            return await CallGetApi<Album>($"/albums/{id}");
        }

        private async Task<T> CallGetApi<T>(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default(T);
        }
    }
}
