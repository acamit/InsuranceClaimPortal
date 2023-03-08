using Duende.IdentityServer.Models;
using static Duende.IdentityServer.IdentityServerConstants;

namespace YCompanyIdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),

        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
            new ApiScope(name: "ClaimsAPI", displayName: "ClaimsAPI")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },
            // machine to macine client for apis
            new Client
            {
                ClientId = "client",

                 // no interactive user, use the clientid/secret for authentication. Machine to machine conversation
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())

                },
                AllowedScopes = { "openid", "ClaimsAPI" }  // allow acess to above created claims.
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:7025/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7025/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7025/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = new List<string>{ StandardScopes.OpenId, StandardScopes.Profile, "scope2", "ClaimsAPI" }
            },
            new Client
            {
                ClientId = "bff",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
    
                // where to redirect to after login
                RedirectUris = { "https://localhost:7196/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:7196/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    StandardScopes.OpenId,
                    StandardScopes.Profile,
                    "ClaimsAPI"
                }
            }

        };
}