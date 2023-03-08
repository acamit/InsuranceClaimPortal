using Duende.IdentityServer.Models;
using Humanizer;
using Serilog;
using System.ComponentModel;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using static Duende.IdentityServer.Events.TokenIssuedSuccessEvent;
using static Duende.IdentityServer.IdentityServerConstants;
using static Duende.IdentityServer.Models.IdentityResources;
using static System.Formats.Asn1.AsnWriter;

namespace IdentityServerAspNetIdentity;

public static class Config
{
    // resources are a named group of claims
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("color", new []{ "favorite_color"}),
            new IdentityResource(
                name: "Profile",
                userClaims: new []{ "name", "email", "websites"},
                displayName: "Your profile data")
        };

    // these are added to token as a claim of type scope
    public static IEnumerable<ApiScope> ApiScopes =>
    new ApiScope[]
    {
        new ApiScope("scope1"),
        new ApiScope("scope2"),
        new ApiScope(name: "ClaimsAPI", displayName: "ClaimsAPI"),
        /* The following scope definition tells the configuration system that when a write scope gets granted the user_level claim should be added to the access token:
         * This will pass the user_level claim as a requested claim type to the profile service, so that the consumer of the access token can use this data as input for
         * authorization decisions or business logic
         */
        new ApiScope(name:"write", displayName:"Write your data", userClaims: new []{ "user_level"})
    };

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("invoice", "Invoice API")
        {
            Scopes = { "invoice.read", "invoice.pay", "manage", "enumerate" }
        },

        new ApiResource("customer", "Customer API")
        {
            Scopes = { "customer.read", "customer.contact", "manage", "enumerate" },
            UserClaims =
            {
                "department_id",
                "sales_region"
            }
        }
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
                AllowedScopes = new List<string>{ StandardScopes.OpenId, StandardScopes.Profile, "scope2", "ClaimsAPI", "color", "Profile" }
            },

        };
}