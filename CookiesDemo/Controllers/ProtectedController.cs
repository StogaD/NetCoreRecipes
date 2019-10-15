using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookiesDemo.Controllers
{
    [Route("api/[controller]")]
    public class ProtectedController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        //will return 404 instead of 401. Automaticaly will be return to Login/.... See next commit to find out how to solve it;)

        [HttpGet("RoleDemo")]
        [Authorize(Roles = "Admin")]
        public string RoleDemo()
        {
            return "Role-based authorization demo";
        }
    }
}
