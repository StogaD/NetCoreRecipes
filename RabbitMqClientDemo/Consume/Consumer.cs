using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqClient.Message;

namespace RabbitMqClient.Consume
{
    public abstract class Consumer : IConsumer
    {
        IConnectionManager _connectionMenager;
        IModel _channel;

        public abstract List<IMessageHandler> MessageHandlers { get; }

        public Consumer(IConnectionManager connectionMenager)
        {
            _connectionMenager = connectionMenager;
        }

        public async void MessageReceived(object sender, BasicDeliverEventArgs e)
        {
            //todo  Dandago.Utilities
            string serializedMessage = Encoding.UTF8.GetString(e.Body);
            RabbitMessage message = new RabbitMessage();
            try
            {
                message = JsonConvert.DeserializeObject<RabbitMessage>(serializedMessage) ?? new RabbitMessage();

                var handleResponse = await GenericHandle(message);

                if (handleResponse)
                {
                    this._channel.BasicAck(e.DeliveryTag, false);
                    return;
                }
                //todo: add logging instead cw
                Console.WriteLine("issue with handler" );
            }
            catch (Exception ex)
            {
                //todo: logging
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<bool> GenericHandle(RabbitMessage message)
        {
          foreach(var handlers in MessageHandlers)
            {
                await handlers.HandleAsync(message);
            }
            return true;
        }

        public void Subscribe(string exchangeName, string queueName)
        {
            var _channel = _connectionMenager.CreateChannel();

            _channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            _channel.ExchangeDeclarePassive(exchangeName);

            _channel.QueueBind(queueName, exchangeName, "Demo");


            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += MessageReceived;

            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

        }

    }
}
