using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SwaggerDemo.AuthFilter
{
    public class CustomAuthFilter : Attribute, IAuthorizeData
    {
        //todo: Can be used to custmise auth and also be used in AuthorizationHeaderOperationFilter. 
        public string Policy { get ; set; }
        public string Roles { get; set; }
        public string AuthenticationSchemes { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}
