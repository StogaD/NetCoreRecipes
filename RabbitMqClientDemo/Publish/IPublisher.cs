using System;
using System.Collections.Generic;
using System.Text;
using RabbitMqClient.Message;

namespace RabbitMqClient.Publish
{
    public interface IPublisher
    {
        void Publish(string exchangeName, string routingKey, RabbitMessage rabbitMessage);
    }
}
