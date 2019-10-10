using System;
using System.Collections.Generic;
using CoreWebApp.Api;
using CoreWebApp.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(CustomApiConvention))]
    public class BookController : ControllerBase
    {
        // GET api/book
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/book/5
        [ApiConventionMethod(typeof(CustomApiConvention), "MySpecialConvention")]
        [HttpGet("{id}")]
        public ActionResult<ResponseModel> Get(int id)
        {
            return new ResponseModel
            {
                CreatedDate = DateTime.Now,
                Id = id,
                Title = "Big Rabbit",
                Price = 510
            };
        }

        // POST api/book
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/book/5
        [ApiConventionMethod(typeof(DefaultApiConventions),
                          nameof(DefaultApiConventions.Put))]
        [HttpPut("{id}")]
        public void BookPut(int id, [FromBody] RequestModel value)
        {
        }

        // DELETE api/book/5
        [HttpDelete("{id}")]
        public void BookDelete(int id)
        {
        }
    }
}