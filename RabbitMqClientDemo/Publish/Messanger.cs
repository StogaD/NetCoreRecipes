using System;
using System.Collections.Generic;
using RabbitMqClient.Message;

namespace RabbitMqClient.Publish
{
    public class Messanger : IMessanger
    {
        private readonly IConnectionManager _connectionManager;
        public Messanger(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        public void Send(string exchangeName, string routingKey, string message, Dictionary<string,string> customHeaders)
        {
            var RabbitMessage = new RabbitMessage()
            {
                Header = customHeaders,
                Payload = message
            };

            var rabbitPublisher = new Publisher(_connectionManager);
            rabbitPublisher.Publish(exchangeName, routingKey, RabbitMessage);
        }
    }
}
