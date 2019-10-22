using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using Newtonsoft.Json;

namespace RabbitMqClient.Publish
{
    public class Publisher : IPublisher
    {
        private readonly IConnectionManager _connectionManager;
        public Publisher(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        public void Publish(string exchangeName, string routingKey, RabbitMessage rabbitMessage)
        {
            var msg = JsonConvert.SerializeObject(rabbitMessage);
            var body = Encoding.UTF8.GetBytes(msg);

            using(var channel = _connectionManager.CreateChannel())
            {

                channel.ExchangeDeclarePassive(exchangeName);

                var properties = new BasicProperties();
                channel.BasicPublish(exchangeName, routingKey, properties, body);
            }
        }
    }
}
