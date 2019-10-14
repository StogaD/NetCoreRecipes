using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerDemo.AuthFilter
{
    public class ExampleOperationFilter : IOperationFilter //Interfase from Swagger
    {
        private readonly string _name;
        public ExampleOperationFilter(string name)
        {
            _name = name;
        }
        public void Apply(Operation operation, OperationFilterContext context)
        {
            System.Diagnostics.Debug.Write(_name);
        }
    }
}
