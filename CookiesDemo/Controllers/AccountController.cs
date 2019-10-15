using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookiesDemo.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        // GET: api/<controller>
        [HttpGet("login")]
        public IActionResult Login()
        {
            return Ok("Login process will be implemented");
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            return Ok("Logout process will be implemented");
        }
    }
}
