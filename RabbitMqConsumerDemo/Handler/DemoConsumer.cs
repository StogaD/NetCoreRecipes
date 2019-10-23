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

    public class DemoConsumerProvider
    {
        IConnectionPoolManager _poolManagement;
        IRabbitOptions _options;
        IMessageHandler _handler;
        public DemoConsumerProvider(IConnectionPoolManager poolManagement, IRabbitOptions options, IMessageHandler handler)
        {
            _handler = handler;
            _poolManagement = poolManagement;
        }
        public DemoConsumer CreateConsumer()
        {
            var connManager = _poolManagement.Get(_options.VirtualHost) as ConnectionManager;
            return new DemoConsumer(connManager, _handler);
        }
    }
}
