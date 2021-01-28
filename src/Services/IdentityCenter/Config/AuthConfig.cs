using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCenter.Config
{
    /// <summary>
    /// IdentityServer4配置信息
    /// </summary>
    public static class AuthConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.Profile(),
                new IdentityResources.OpenId(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("server4", "身份令牌服务")
            };
        }

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>()
        {
            new ApiScope("server4","身份令牌服务")
        };

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes =GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("96dC73EDpSl4iCqg".Sha256())
                    },
                    AllowedScopes = {"server4"},
                    AccessTokenLifetime=60*60*24,
                },
                new Client
                {
                    ClientId = "clientpwd",
                    AllowedGrantTypes =GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("96dC73EDpSl4iCqg".Sha256())
                    },
                    AllowedScopes = {"server4",IdentityServerConstants.StandardScopes.OpenId,
                 IdentityServerConstants.StandardScopes.Profile},
                    AccessTokenLifetime=60*60*24,
                }
            };
        }


    }
}
