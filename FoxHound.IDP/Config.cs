using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

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
                { };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName = "FoxHound",
                    ClientId = "foxhound",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    PostLogoutRedirectUris = new List<string>
                    {
                        //"https://localhost:44389/signout-callback-oidc"
                    },
                    RedirectUris = new List<string>( )
                    {
                       // "https://localhost:44389/signin-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())  // use different for production
                    }
                }

            };

    }
}

