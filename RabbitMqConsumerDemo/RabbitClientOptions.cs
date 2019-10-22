using RabbitMqClient;

namespace RabbitMqConsumerDemo
{
    public class RabbitClientOptions : IRabbitOptions
    {
        public string Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string VirtualHost { get; set; }

        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
 
    }
}