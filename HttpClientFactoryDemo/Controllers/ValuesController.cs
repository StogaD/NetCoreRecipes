using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientFactoryDemo.Models;
using HttpClientFactoryDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactoryDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;
        private readonly IAlbumService _albumService;

        public ValuesController(IUserService userService, IPhotoService photoService, IAlbumService albumService)
        {
            _userService = userService;
            _photoService = photoService;
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

        // GET api/photo
        [HttpGet("photos")]
        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            return await _photoService.GetPhotos();
        }

        // GET api/photo/5
        [HttpGet("photos/{id}")]
        public async Task<Photo> GetPhoto(int id)
        {
            return await _photoService.GetPhotoItem(id);
        }
        // GET api/users
        [HttpGet("users")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        // GET api/users/5
        [HttpGet("users/{id}")]
        public async Task<User> GetUsers(int id)
        {
            return await _userService.GetUser(id);
        }
    }
}
