using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RabbitMqClient.Consume;
using RabbitMqClient.Message;
using RabbitMqConsumerDemo.Service;

namespace RabbitMqConsumerDemo.Handler
{
    public class DemoHandler : IMessageHandler
    {
        //private readonly IDbService _dbservice;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public DemoHandler(/*IDbService dbservice*/ IServiceScopeFactory serviceScopeFactory)
        {
            //_dbservice = dbservice;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<HandleResponse> HandleAsync(RabbitMessage message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbService = scope.ServiceProvider.GetService<IDbService>();
                await dbService.StoreMessage(message.Payload);
                Console.WriteLine(message);
                var respnse = new HandleResponse { IsSuccess = true };
                return await Task.FromResult(respnse);
            }
        }
    }
}
