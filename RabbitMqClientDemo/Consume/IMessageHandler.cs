using System.Threading.Tasks;
using RabbitMqClient.Message;

namespace RabbitMqClient.Consume
{
    public interface IMessageHandler
    {
        Task<HandleResponse> HandleAsync(RabbitMessage message);
    }
}