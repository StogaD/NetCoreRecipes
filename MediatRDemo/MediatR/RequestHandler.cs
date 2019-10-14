using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRDemo.Model;

namespace MediatRDemo.MediatR
{
    public class RequestHandler : IRequestHandler<RequestMessage, string>
    {
        public Task<string> Handle(RequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult($"received {request.Message} !");
        }
    }
}
