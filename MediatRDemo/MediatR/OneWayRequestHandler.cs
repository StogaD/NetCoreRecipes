using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRDemo.Model;

namespace MediatRDemo.MediatR
{
    public class OneWayRequestHandler : IRequestHandler<OneWayRequestMessage>
    {
        public async Task<Unit> Handle(OneWayRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Unit.Task.Result);

        }
    }
}
