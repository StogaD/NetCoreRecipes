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

        public ValuesController(IUserService userService )
        {
            _userService = userService;
        }

        // GET api/albums
        [HttpGet("albums")]
        public ActionResult<IEnumerable<Album>> GetAlbums()
        {

            return new List<Album> {
                new Album() { Id = 1 },
                new Album() { Id = 2 }
            };
        }

        // GET api/albums/5
        [HttpGet("albums/{id}")]
        public ActionResult<Album> GetAlbum(int id)
        {
            return new Album() { Id = id };
        }

        // GET api/photo
        [HttpGet("photos")]
        public ActionResult<IEnumerable<Photo>> GetPhotos()
        {
            return new List<Photo> {
                new Photo() { Id = 1 },
                new Photo() { Id = 2 }
            };
        }

        // GET api/photo/5
        [HttpGet("photos/{id}")]
        public ActionResult<Photo> GetPhoto(int id)
        {
            return new Photo() { Id = id };
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
