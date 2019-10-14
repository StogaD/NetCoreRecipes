using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestEaseDemo.Model;
using RestEaseDemo.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestEaseDemo.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogService;

        public BlogController(IBlogRepository blogService)
        {
            _blogService = blogService;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<BlogPost>> Get()
        {
            return await _blogService.GetPosts();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
