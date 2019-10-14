using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRDemo.Model;
using Serilog;

namespace MediatRDemo.MediatR
{
    public class EventHandler : INotificationHandler<NotificationMessage>
    {
        private readonly ILogger _logger;
        public EventHandler(ILogger logger)
        {
            _logger = logger;
        }
        public Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
        {
            _logger.Information($"Notification Handler - received {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
