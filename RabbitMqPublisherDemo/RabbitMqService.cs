using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMqClient.Publish;
using RabbitMqClientDemo;

namespace RabbitMqPublisherDemo
{
    public interface  IRabbitMqService
    {
        void PushMessage(string message);
        void PushMessage(string message, Dictionary<string, string> headers);
    }

    public class RabbitMqService : IRabbitMqService
    {
        private readonly IMessanger _messanger;
        private readonly RabbitClientOptions _options;
        public RabbitMqService(IMessanger messanger, IOptions<RabbitClientOptions> options)
        {
            _messanger = messanger;
            _options = options.Value;
        }

        public void PushMessage(string message)
        {
            PushMessage(message, new Dictionary<string, string> { ["testHeader"] = "demo" });
        }

        public void PushMessage(string message, Dictionary<string, string> headers)
        {
            _messanger.Send(
              _options.ExchangeName,
              _options.RoutingKey,
              message,
              headers);
        }
    }
}
