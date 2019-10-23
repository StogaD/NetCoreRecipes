using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMqClient;
using RabbitMqClient.Consume;
using RabbitMqClient.Publish;

namespace RabbitMqClientDemo
{

    public class Parameters
    {
        public bool AddMessanger { get; set; }
        public IRabbitOptions Options { get; set; }
    }
    public interface IRabbitMqClientBuilder
    {
        IServiceCollection Services { get; set; }
        IRabbitOptions Options { get; set; }
    }

    internal class RabbitMqClientBuilder : IRabbitMqClientBuilder
    {
        public IServiceCollection Services { get; set; }
        public IRabbitOptions Options { get; set; }
    }

    public static class ServiceCollectionCoreExtension
    {

        public static IServiceCollection AddRabbitMqClient(this IServiceCollection services, Action<IServiceProvider, Parameters> addMessanger, IRabbitOptions options = null)
        {
            var par = new Parameters();
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var sp = scope.ServiceProvider;
                addMessanger(sp, par);

            }
            var connectionPoolManager = new ConnectionPoolManager();
            connectionPoolManager.RegisterConnecton(par.Options.VirtualHost, par.Options);
            services.AddSingleton<IConnectionPoolManager>(x =>
            {
                return connectionPoolManager;
            });

            if(par.AddMessanger)
            {
                services.AddSingleton<IMessanger>(x =>
                {
                    var connectionManager = connectionPoolManager.Get(par.Options.VirtualHost);
                    return new Messanger(connectionManager);
                }
               );

            }

            return services;
        }
        public static IServiceCollection AddConsumer(this IRabbitMqClientBuilder builder, Func<ConnectionManager,Consumer> consumerProvider)
        {
            IServiceCollection sc = builder.Services;
            IConnectionPoolManager connPoolManager;
            using (var scope = sc.BuildServiceProvider().CreateScope())
            {
                var sp = scope.ServiceProvider;
                connPoolManager = sp.GetService<IConnectionPoolManager>();
            }
            if (connPoolManager == null)
            {
                throw new InvalidOperationException("connectionPoolManager is not registered");
            }

            ConnectionManager connManager = (ConnectionManager)connPoolManager.Get(builder.Options.VirtualHost);
            var consumer = consumerProvider(connManager);

            consumer.Subscribe(builder.Options.ExchangeName, builder.Options.RoutingKey);
            return builder.Services;
        }
    }
}
