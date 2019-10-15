﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookiesDemo.Policy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace CookiesDemo.Identity
{
    public class CustomPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "MinimumAge";
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }
        public async Task<AuthorizationPolicy> GetDefaultPolicyAsync() => await FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase) &&
                int.TryParse(policyName.Substring(POLICY_PREFIX.Length), out var age))
            {
                var policy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme);

                policy.AddRequirements(new MinimumAgePolicyRequirement(age)); // <-- here we are using our custom policy req.
                return Task.FromResult(policy.Build());
            }

            return Task.FromResult<AuthorizationPolicy>(null);

        }
    }
}
