using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMqClient;
using RabbitMqClient.Consume;
using RabbitMqConsumerDemo.Handler;

namespace RabbitMqConsumerDemo.Service
{
    public interface IRabbitConsumerRegister
    {
        void RegisterHandler();
    }
    public class RabbitConsumerRegister : IRabbitConsumerRegister
    {
        private readonly IConnectionPoolManager _rabbitConnectionPool;
        private readonly RabbitClientOptions _options;
        private readonly IDbService _dbService;
        private readonly IServiceProvider _serviceProvider;
        public RabbitConsumerRegister(IConnectionPoolManager rabbitConnectionPool, IOptions<RabbitClientOptions> options, IDbService dbService, IServiceProvider serviceProvider)
        {
            _rabbitConnectionPool = rabbitConnectionPool;
            _options = options.Value;
            _dbService = dbService;
            _serviceProvider = serviceProvider;
        }

        public void RegisterHandler()
        {
            // Create handler
            var handler = (DemoHandler) _serviceProvider.GetService(typeof(DemoHandler));
            // Create Cosnumer and bind handler
            var connManager = _rabbitConnectionPool.Get(_options.VirtualHost) as ConnectionManager;
            var consumer = new DemoConsumer(connManager, handler);
            // Start listining
            consumer.Subscribe(_options.ExchangeName, _options.RoutingKey);

        }
    }
}
