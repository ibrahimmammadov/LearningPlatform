using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LpIdentityServer.Extensions
{
    public  class GrantTypesExtension: GrantTypes
    {
        public static ICollection<string> TokenExchangeClientCredentials => new string[1] { "urn:ietf:params:oauth:grant-type:token-exchange" };
    }
}
