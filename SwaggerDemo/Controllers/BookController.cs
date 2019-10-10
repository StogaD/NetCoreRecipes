using System.Collections.Generic;
using CoreWebApp.Api;
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
        public ActionResult<string> Get(int id)
        {
            return "value";
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
        [HttpPut("{id}")]
        public void BookPut(int id, [FromBody] string value)
        {
        }

        // DELETE api/book/5
        [HttpDelete("{id}")]
        public void BookDelete(int id)
        {
        }
    }
}