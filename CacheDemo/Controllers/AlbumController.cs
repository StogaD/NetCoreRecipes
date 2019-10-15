using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheDemo.CahceDemo;
using CacheDemo.Models;
using CacheDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CacheDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly ICacheDemoService _cacheDemo;
        public AlbumController(IAlbumService albumService, ICacheDemoService cacheDemo)
        {
            _albumService = albumService;
            _cacheDemo = cacheDemo;
        }
        // GET api/albums
        [HttpGet]
        public async Task<IEnumerable<Album>> GetAlbums()
        {
            return await _albumService.GetAlbums();
        }

        // GET api/albums/5
        [HttpGet("InMemoryCache/{id}")]
        public async Task<Album> GetUsingInMemoryCache(int id)
        {
            return await _cacheDemo.GetUsingInMemoryCacheAsync(id);
        }
        [HttpGet("InMemoryCacheV2/{id}")]
        public async Task<Album> GetUsingInMemoryCacheV2(int id)
        {
            return await _cacheDemo.GetUsingInMemoryCacheV2Async(id);
        }
    }
}
