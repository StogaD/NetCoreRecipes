using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RabbitMqConsumerDemo.Data
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options) :base(options)
        {

        }
        public DbSet<RabbitMessage> RabbitMessage { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }
    }
}
