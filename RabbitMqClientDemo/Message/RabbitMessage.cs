using System.Collections.Generic;

namespace RabbitMqClient.Message
{
    public class RabbitMessage
    {
        public Dictionary<string,string> Header { get; set; }
        public string Payload { get; set; }
    }
}