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
                                Name = "foxhound.full_access",
                                DisplayName = "Full access to FoxHound API",
                            },
                            new Scope
                            {
                                Name = "foxhound.read_only",
                                DisplayName = "Read only access to FoxHound API"
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

                    RedirectUris =           { "http://localhost:1234/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:1234/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:1234" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        "foxhound.read_only",
                        "foxhound.full_access"
                    }
                }
            };

    }
}

