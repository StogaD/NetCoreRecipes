using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretDemo.KeyVault;

namespace SecretDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        private readonly ISecretAccess _secretAccess;
        public SecretController(ISecretAccess secretAccess)
        {
            _secretAccess = secretAccess;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<string> Get(string secretName = null)
        {
            return await _secretAccess.GetSecret(secretName);
        }

    }
}
