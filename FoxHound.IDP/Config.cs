using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;

namespace FoxHound.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
                { 
                    new ApiResource
                    {
                        Name = "foxhoundapi",

                        // secret for using introspection endpoint
                        ApiSecrets =
                        {
                            new Secret("secret".Sha256())
                        },

                        // include the following using claims in access token (in addition to subject id)
                        UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email },

                        // this API defines two scopes
                        Scopes =
                        {
                            new Scope()
                            {
                                Name = "api",
                                DisplayName = "Full access to FoxHound API"
                            }
                        }
                    }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName = "FoxHound",
                    ClientId = "foxhound",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RequireClientSecret = false,
                    RequireConsent = false,
                    
                    RedirectUris =           { "http://localhost:1234/signin-callback", "http://localhost:1234/silent-renew" },
                    PostLogoutRedirectUris = { "http://localhost:1234/" },
                    AllowedCorsOrigins =     { "http://localhost:1234" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        "api"
                    }
                }
            };
    }
}

