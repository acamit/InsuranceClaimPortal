using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using YCompanyIdentityServer.Models;
using DuendeEntities = Duende.IdentityServer.EntityFramework.Entities;

namespace YCompanyIdentityServer.Data
{
    public class DevelopmentSeedData
    {
        public static ApplicationUser DefaultUser = new ApplicationUser
        {
            UserName = "amit.chawla",
            Email = "acamit84@gmail.com",
            GivenName = "Amit",
            FamilyName = "Chawla"
        };

        public static string DefaultPassword = "Pa55w0rd!";


        public static ApiResource ApiResource = new ApiResource
        {
            Name = Guid.NewGuid().ToString(),
            DisplayName = "API",
            Scopes = new List<string>()
                {
                    "https://ycompany.com/api"
                }

        };

        public static List<ApiResource> ApiResources = new List<ApiResource>
        {
            ApiResource
        };

        public static ApiScope ApiScope = new ApiScope()
        {
            Name = "https://ycompany.com/api", // same as above
            DisplayName = "API",
        };

        public static List<ApiScope> ApiScopes = new List<ApiScope>() { ApiScope };



        public static List<Client> Clients = new List<Client> { new Client()
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientSecrets = new List<Secret> { new Secret("secret".Sha512()) },
                ClientName = "Console Applications",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = new List<string>() { "https://ycompany.com/api" },
                AllowedCorsOrigins = new List<string>() { "https://api:7001" } // api url
            },

            new Client()
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientSecrets = new List<Secret> { new Secret("secret".Sha512()) },
                ClientName = "Web Applications",
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = new List<string>() { "https://ycompany.com/api", "openid", "profile", "email", "https://ycompany.com/api" },
                RedirectUris = new List<string> { "https://webapplication:7002/signin-oidc" },
                PostLogoutRedirectUris = new List<string> { "https://webapplication:7002/signout-callback-oidc" }
            },

            new Client()
            {
                ClientId = Guid.NewGuid().ToString(),
                RequireClientSecret = false, // no client secret for public SPA
                ClientName = "Single Page Applications",
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = new List<string>() { "https://ycompany.com/api", "openid", "profile", "email", "https://ycompany.com/api" },
                RedirectUris = new List<string> { "https://singlepageapplication:7003/authentication/login-callback" },
                PostLogoutRedirectUris = new List<string> { "https://singlepageapplication:7003/authentication/logout-callback" },
                AllowedCorsOrigins = new List<string>() { "https://singlepageapplication:7003" }
            }
        };

        public static List<DuendeEntities.Client> ClientEntities = Clients.Select(x => x.ToEntity()).ToList();

        public static List<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

        public static List<DuendeEntities.IdentityResource> IdentityResourceEntities = IdentityResources.Select(x => x.ToEntity()).ToList();
    }
}
