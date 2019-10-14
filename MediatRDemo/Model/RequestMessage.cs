using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MediatRDemo.Model
{
    public class RequestMessage : IRequest<string>   // expected response as string
    {
        public string Message { get; set; }
    }
}
