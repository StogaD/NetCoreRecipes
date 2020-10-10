﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    // GET api/values
    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<string>> Get()
    {

      return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
      return "value";
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
