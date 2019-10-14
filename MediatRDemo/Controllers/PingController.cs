using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MediatRDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace MediatRDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET api/values
        [HttpGet("/testnotifiation")]
        public async Task NotifyTest(string message)
        {
            await _mediator.Publish<NotificationMessage>( new NotificationMessage { Message = message });
        }

        // GET api/values
        [ProducesResponseType(statusCode: 200, type: typeof(string))]
        [HttpGet("/testrequest")]
        public async Task<string> RequestTest(string message)
        {
            var response = await _mediator.Send(new RequestMessage { Message = message });
            return response;
        }


        // GET api/values
        [ProducesResponseType(statusCode: 200, type: typeof(string))]
        [HttpGet("/oneayrequest")]
        public async Task<IActionResult> OneWayRequestTest(string message)
        {
            await _mediator.Send(new OneWayRequestMessage { Message = message });
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
