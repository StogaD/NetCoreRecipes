using System;
using System.Collections.Concurrent;

namespace RabbitMqClient
{
    public class ConnectionPoolManager : IConnectionPoolManager
    {
        private readonly ConcurrentDictionary<string, ConnectionManager> _connections = new ConcurrentDictionary<string, ConnectionManager>();


        public bool RegisterConnecton(string virtualHost, IRabbitOptions rabbitConsts)
        {
            var connectionManager = new ConnectionManager(rabbitConsts);

            connectionManager.Connect();
            
           return _connections.TryAdd(virtualHost, connectionManager);

        }

        public IConnectionManager Get(string virtualHost)
        {
            _connections.TryGetValue(virtualHost, out var connectionManager);

            return connectionManager;
        }

        public void Remove(string virtualHost)
        {
            _connections.TryRemove(virtualHost, out _);
        }
    }
}
