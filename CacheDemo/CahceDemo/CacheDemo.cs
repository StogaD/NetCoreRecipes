using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheDemo.Models;
using CacheDemo.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CacheDemo.CahceDemo
{
    public interface ICacheDemoService
    {
        Task<Album> GetUsingInMemoryCacheAsync(int id);

        Task<Album> GetUsingInMemoryCacheV2Async(int id);

        Task<Album> GetUsingInMemoryCacheV3Async(int id);
    }
    public class CacheDemoService : ICacheDemoService
    {
        private readonly IAlbumService _albumService;
        private readonly IMemoryCache _memoryCache;
        private readonly IMemoryCache _sizedMemoryCache;
        private readonly ILogger _logger;

        public CacheDemoService(IAlbumService albumService, IMemoryCache memoryCache, ILogger<CacheDemoService> logger, MySizedMemoryCache myMemoryCache)
        {
            _albumService = albumService;
            _memoryCache = memoryCache;
            _logger = logger;

            _sizedMemoryCache = myMemoryCache.Cache;
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

                album.FromCacheOrService = album.FromCacheOrService = DataSourceEnum.Repository;
            }
            else
            {
                album.FromCacheOrService = album.FromCacheOrService = DataSourceEnum.Cache;
            }
            return album;
        }

        public async Task<Album> GetUsingInMemoryCacheV2Async(int id)
        {
            var source = DataSourceEnum.Cache;
            var retrivedPhoto = await _memoryCache.GetOrCreateAsync<Album>(id.ToString(), async (cache) =>
            {
            var album = await _albumService.GetAlbumItem(id);
            source = DataSourceEnum.Repository;
                return album;
            });

            retrivedPhoto.FromCacheOrService = source;

            return retrivedPhoto;
        }

        public async Task<Album> GetUsingInMemoryCacheV3Async(int id)
        {
            var source = DataSourceEnum.Cache;
            var retrivedPhoto = await _memoryCache.GetOrCreateAsync<Album>(id.ToString(), async (cache) =>
            {
                cache.SetSlidingExpiration(TimeSpan.FromSeconds(3));
                cache.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
                cache.RegisterPostEvictionCallback(EvictionCallback, this); // fired when evicted from cache!

                var album = await _albumService.GetAlbumItem(id);
                source = DataSourceEnum.Repository;
                return album;
            });

            retrivedPhoto.FromCacheOrService = source;

            return retrivedPhoto;
        }

        public async Task<Album> GetUsingInMemoryCacheV4Async(int id)
        {
            var source = DataSourceEnum.Cache;

            var retrivedPhoto = await _memoryCache.GetOrCreateAsync<Album>(id.ToString(), async (cache) =>
            {
                cache.SetSlidingExpiration(TimeSpan.FromSeconds(3));
                cache.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
                cache.SetSize(1024);
                cache.RegisterPostEvictionCallback(EvictionCallback, this); // fired when evicted from cache!
                var album = await _albumService.GetAlbumItem(id);
                source = DataSourceEnum.Repository;
                return album;
            });

            retrivedPhoto.FromCacheOrService = source;

            return retrivedPhoto;
        }

        private void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            _logger.LogInformation($"{key}:{value} set in cache. Reason: {reason}");
        }
    }
}
