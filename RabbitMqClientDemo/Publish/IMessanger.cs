using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqClient.Publish
{
    public interface IMessanger
    {
        void Send(string exchangeName, string routingKey, string rabbitMessage, Dictionary<string, string> customHeaders = null);
    }
}
