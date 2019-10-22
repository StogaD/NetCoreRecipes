using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitMqClient.Publish;
using RabbitMqClientDemo;

namespace RabbitMqPublisherDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRabbitMqService _messanger;
        public ValuesController(IRabbitMqService messanger)
        {
            _messanger = messanger;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string message)
        {
            _messanger.PushMessage(message,
                new Dictionary<string, string> { ["testHeader"] = "demo" });
        }

    }
}
