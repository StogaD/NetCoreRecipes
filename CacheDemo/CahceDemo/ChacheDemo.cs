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
    }
}
