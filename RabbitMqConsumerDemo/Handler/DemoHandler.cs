using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMqClient.Consume;
using RabbitMqClient.Message;
using RabbitMqConsumerDemo.Service;

namespace RabbitMqConsumerDemo.Handler
{
    public class DemoHandler : IMessageHandler
    {
        private readonly IDbService _dbservice;
        public DemoHandler(IDbService dbservice)
        {
            _dbservice = dbservice;
        }
        public async Task<HandleResponse> HandleAsync(RabbitMessage message)
        {
           await _dbservice.StoreMessage(message.Payload);
            Console.WriteLine(message);
            var respnse = new HandleResponse { IsSuccess = true };
            return await Task.FromResult(respnse);
        }
    }
}
