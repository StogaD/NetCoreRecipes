using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqClient
{
    public interface IConnectionManager
    {
        void Connect();

        IModel CreateChannel();

        ConnectionResult ConnectionState(IModel channel);

        void RemoveChannel(IModel channel);
    }
}
