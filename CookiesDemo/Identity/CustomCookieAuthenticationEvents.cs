using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CookiesDemo.Identity
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            var lastChanged = (from c in userPrincipal.Claims
                               where c.Type == "LastChanged"
                               select c.Value).FirstOrDefault();


            //Only for test purpose -  it shoud check the last change from DB. Simulate that user was removed from db after 20 sec
            if (DateTime.TryParse(lastChanged, out var _dateTime))
            {
                if (DateTime.Now.CompareTo(_dateTime.AddMinutes(20)) > 0)
                {
                    context.RejectPrincipal();
                    context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
            return base.ValidatePrincipal(context);
        }
    }
}
