using CacheDemo.Models;
using CacheDemo.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheDemo.CahceDemo
{
    public interface IDistributedCacheDemo
    {
        Task<Album> GetUsingDistributedCacheRedisAsync(int id);
    }

    public class DistributedCacheDemo : IDistributedCacheDemo
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IAlbumService _albumService;
 
        public DistributedCacheDemo(IAlbumService albumService, IDistributedCache cache)
        {
            _distributedCache = cache;
            _albumService = albumService;
        }

        public async Task<Album> GetUsingDistributedCacheRedisAsync(int id)
        {
            var key = id.ToString();
            Album album = null;

            var fromCache = await _distributedCache.GetStringAsync(key);

            if (fromCache == null)
            {
                album = await _albumService.GetAlbumItem(id);
                var serializedAlbum = JsonConvert.SerializeObject(album);
                await _distributedCache.SetStringAsync(key, serializedAlbum);
                album.FromCacheOrService = DataSourceEnum.Repository;
            }
            else
            {
                album = JsonConvert.DeserializeObject<Album>(fromCache);
                album.FromCacheOrService = DataSourceEnum.Cache;
            }

            return album;
        }
    }
}
