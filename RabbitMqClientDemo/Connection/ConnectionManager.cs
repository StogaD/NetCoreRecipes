using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqClient
{
    public class ConnectionManager : IConnectionManager, IDisposable
    {
        private IConnection _connection;
        private readonly List<IModel> _channels = new List<IModel>();
        private readonly IRabbitOptions _rabbitOptions;
        private readonly ConnectionFactory _connectionFactory;

        public ConnectionManager(IRabbitOptions rabbitOptions)
        {
            _rabbitOptions = rabbitOptions;

            var _connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitOptions.Host,
                Password = _rabbitOptions.Password,
                VirtualHost = _rabbitOptions.VirtualHost,
                UserName = _rabbitOptions.Username
            };

        }
        public ConnectionResult ConnectionState(IModel channel)
        {
            //todo
            return new ConnectionResult();
        }

        public void Connect()
        {
            try
            {
                if (_connection == null || _connection.IsOpen)
                {
                    _connection = _connectionFactory.CreateConnection();
                }
            }
          catch(Exception ex)
            {
                //todo: Add logging
                Console.WriteLine("Error: {0)",ex.Message);
            }

        }
        public IModel CreateChannel()
        {
            var channel = _connection.CreateModel();
            _channels.Add(channel);
            return channel;
        }

        public void RemoveChannel(IModel channel)
        {
            _channels.Remove(channel);
        }

        public void Dispose()
        {
          foreach(var chan in _channels)
            {
                chan.Close(200, "by user");
                chan.Dispose();
            }
          (_connection as IDisposable).Dispose();
        }
    }
}
