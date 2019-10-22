using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMqConsumerDemo.Data
{
    public class RabbitMessage
    {
        public Guid Id { get; set; }
        public string Payload { get; set; }
    }
}
