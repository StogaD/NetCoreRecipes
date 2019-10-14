using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretDemo.Configuration
{
    public class KeyVaultOptions
    {
        public string KeyVaultName { get; set; }
        public string SecretName { get; set; }
    }
}
