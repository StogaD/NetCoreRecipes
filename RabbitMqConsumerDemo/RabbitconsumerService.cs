using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMqConsumerDemo.Service;

namespace RabbitMqConsumerDemo
{
    public class RabbitConsumerService : IHostedService, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        public void Dispose()
        {
            _serviceScope.Dispose();
        }
        IRabbitConsumerRegister _rabbitService;
        public RabbitConsumerService(IServiceProvider serviceProvider)
        {
            var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
            _serviceScope = serviceScopeFactory.CreateScope();
            _rabbitService = (IRabbitConsumerRegister)_serviceScope.ServiceProvider.GetService(typeof(IRabbitConsumerRegister));
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _rabbitService.RegisterHandler();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
