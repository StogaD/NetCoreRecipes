using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IdServerHost
{
  internal class Config
  {
    internal static IEnumerable<IdentityResource> GetIdentityResources()
    {
      return new List<IdentityResource>
      {

      };
    }

    internal static IEnumerable<ApiResource> GetApis()
    {
      return new List<ApiResource>
     {
        new ApiResource("api1", "My Api")
     };
    }

    internal static IEnumerable<Client> GetClients()
    {
      return new List<Client>
      {
        new Client
        {
          ClientId = "client",
          AllowedGrantTypes = GrantTypes.ClientCredentials,
          ClientSecrets =
          {
            new Secret("secret".Sha256())
          },
          AllowedScopes = {"api1"}
        }
      };

    }
  }
}