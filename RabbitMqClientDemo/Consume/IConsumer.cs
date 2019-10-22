using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace RabbitMqClient.Consume
{
    public interface IConsumer
    {
        void Subscribe(string exchangeName, string queueName);

        void MessageReceived(object sender, BasicDeliverEventArgs e);
    }
}
