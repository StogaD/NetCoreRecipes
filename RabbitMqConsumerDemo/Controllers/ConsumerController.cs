using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMqClientDemo;
using RabbitMqConsumerDemo.Service;

namespace RabbitMqConsumerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IDbService _client;
        public ConsumerController(IDbService client)
        {
            _client = client;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return _client.GetMessage();
        }
    }
}
