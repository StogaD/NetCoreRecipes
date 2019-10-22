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
        public static IRabbitMqClientBuilder AddRabbitMqClient(this IServiceCollection services, Func<IServiceProvider, IRabbitOptions> options)
        {
            IRabbitOptions settings;
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var sp = scope.ServiceProvider;
                settings = options(sp);
            }

            return services.AddRabbitMqClient(settings);

        }

        public static IRabbitMqClientBuilder AddRabbitMqClient(this IServiceCollection services, IRabbitOptions options)
        {
            IRabbitOptions settings = options;

            var connPoolManager = new ConnectionPoolManager();

            connPoolManager.RegisterConnecton(settings.VirtualHost, settings);

            services.AddSingleton<IConnectionPoolManager>(connPoolManager);

            var connManager = new ConnectionManager(settings);
            connManager.Connect();


            //services.AddSingleton<IMessanger>(x =>
            //{
            //    var connectionManager = connPoolManager.Get(settings.VirtualHost);

            //    return new Messanger(connectionManager);
            //});

            return new RabbitMqClientBuilder { Services = services, Options = options };
        }

        public static IServiceCollection AddMessenger(this IRabbitMqClientBuilder builder)
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

            builder.Services.AddSingleton<IMessanger>(x =>
            {
                var connectionManager = connPoolManager.Get(builder.Options.VirtualHost);

                return new Messanger(connectionManager);
            });

            return builder.Services;
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
