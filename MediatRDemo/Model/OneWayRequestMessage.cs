using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MediatRDemo.Model
{
    public class OneWayRequestMessage : IRequest
    {
        public string Message { get; set; }
    }
}
