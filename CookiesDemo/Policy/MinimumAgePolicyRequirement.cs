using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CookiesDemo.Policy
{
    public class MinimumAgePolicyRequirement : AuthorizationHandler<MinimumAgePolicyRequirement>, IAuthorizationRequirement
    {
        private readonly int _age;
        public MinimumAgePolicyRequirement(int age)
        {
            _age = age;
        }

        protected override Task HandleRequirementAsync(Microsoft.AspNetCore.Authorization.AuthorizationHandlerContext context, MinimumAgePolicyRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.FromResult(0);
            }
            var dob = context.User.Claims.First(c => c.Type == ClaimTypes.DateOfBirth).Value;

            if (DateTime.TryParse(dob, out var date))
            {
                if (date.AddYears(_age) < DateTime.Now)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.FromResult(0);
        }
    }
}
