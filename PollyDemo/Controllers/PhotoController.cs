using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PollyDemo.Models;
using PollyDemo.Services;

namespace PollyDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        // GET api/photos
        [HttpGet]
        public async Task<IEnumerable<Photo>> Get()
        {
           return await _photoService.GetPhotos();
        }

        // GET api/photos/5
        [HttpGet("{id}")]
        public async Task<Photo> Get(int id)
        {
            return await _photoService.GetPhoto(id);
        }
    }
}
