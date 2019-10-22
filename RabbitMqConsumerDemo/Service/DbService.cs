using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMqConsumerDemo.Data;

namespace RabbitMqConsumerDemo.Service
{
    public interface IDbService
    {
        string GetMessage();
        Task StoreMessage(string message);
    }
    public class DbService : IDbService
    {
        MessageContext _context;
        public DbService(MessageContext context)
        {
            _context = context;
        }
        public string GetMessage()
        {
            return _context.RabbitMessage.Select(x => x.Payload).FirstOrDefault();
        }
        public async Task StoreMessage(string message)
        {
            var rabbitMessage = new RabbitMessage
            {
                Id = Guid.NewGuid(),
                Payload = message
            };

            try
            {
                _context.RabbitMessage.Add(rabbitMessage);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
