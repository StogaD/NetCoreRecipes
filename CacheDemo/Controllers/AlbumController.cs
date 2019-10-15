using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        // GET api/albums
        [HttpGet("albums")]
        public async Task<IEnumerable<Album>> GetAlbums()
        {
            return await _albumService.GetAlbums();
        }

        // GET api/albums/5
        [HttpGet("albums/{id}")]
        public async Task<Album> GetAlbum(int id)
        {
            return await _albumService.GetAlbumItem(id);
        }
    }
}
