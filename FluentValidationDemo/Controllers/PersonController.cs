using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidationDemo.Models;
using FluentValidationDemo.ValidationRules;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FluentValidationDemo.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            return Ok();
        }

    }
}
