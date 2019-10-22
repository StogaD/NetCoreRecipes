using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqClient.Publish
{
    public interface IPublisher
    {
        void Publish(string exchangeName, string routingKey, RabbitMessage rabbitMessage);
    }
}
