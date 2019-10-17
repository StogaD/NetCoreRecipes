using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using FluentValidationDemo.Models;
using FluentValidationDemo.ValidationRules;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FluentValidationDemo.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            var validator = new CustomerValidator();
            ValidationResult results = validator.Validate(customer);

            if (!results.IsValid)
            {
                return BadRequest(results.Errors);
            }
            return Ok();
        }
    }
}
