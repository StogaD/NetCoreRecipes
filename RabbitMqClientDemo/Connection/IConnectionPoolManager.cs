using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqClient
{
   public interface IConnectionPoolManager
    {
        bool RegisterConnecton(string virtualHost, IRabbitOptions rabbitConsts);

        IConnectionManager Get(string virtualHost);

        void Remove(string virtualHost);
    }
}
