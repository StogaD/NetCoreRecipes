using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMqClient;
using RabbitMqClient.Consume;

namespace RabbitMqConsumerDemo.Handler
{
    public class DemoConsumer : Consumer
    {
        public DemoConsumer(IConnectionManager connection, IMessageHandler handler) : base(connection)
        {
            MessageHandlers = new List<IMessageHandler> { handler };
        }
        public override List<IMessageHandler> MessageHandlers { get; }
    }
}
