using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheDemo.Models;
using CacheDemo.Services;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.CahceDemo
{
    public interface ICacheDemoService
    {
        Task<Album> GetUsingInMemoryCacheAsync(int id);
        Task<Album> GetUsingInMemoryCacheV2Async(int id);
    }
    public class CacheDemoService : ICacheDemoService
    {
        private readonly IAlbumService _albumService;
        private readonly IMemoryCache _memoryCache;
 
        public CacheDemoService(IAlbumService albumService, IMemoryCache memoryCache)
        {
            _albumService = albumService;
            _memoryCache = memoryCache;
        }

        public async Task<Album> GetUsingInMemoryCacheAsync(int id)
        {
            var existsInCache = _memoryCache.TryGetValue<Album>(id.ToString(), out var album);

            if (!existsInCache)
            {
                album = await _albumService.GetAlbumItem(id);
                //Set to memory with options.
                _memoryCache.Set<Album>(
                    id.ToString(),
                    album,
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10)));

                album.FromCacheOrService = "Service";
            }
            else
            {
                album.FromCacheOrService = "Cache";
            }
            return album;
        }

        public async Task<Album> GetUsingInMemoryCacheV2Async(int id)
        {
            var source = "cache";
            var retrivedPhoto = await _memoryCache.GetOrCreateAsync<Album>(id.ToString(), async (cache) =>
            {
            var album = await _albumService.GetAlbumItem(id);
            source = "FromRepo";
            return album;
            });

            retrivedPhoto.FromCacheOrService = source;

            return retrivedPhoto;
        }
    }
}
