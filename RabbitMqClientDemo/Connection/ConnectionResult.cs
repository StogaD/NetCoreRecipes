using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqClient
{
    public class ConnectionResult
    {
        public bool IsReconnected { get; set; }

        public IModel Channel { get; set; }
    }
}
