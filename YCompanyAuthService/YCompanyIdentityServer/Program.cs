using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;
using YCompanyIdentityServer.Data;
using YCompanyIdentityServer.Factories;
using YCompanyIdentityServer.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, dbContextOptionsBuilder) =>
{
    dbContextOptionsBuilder
        .UseSqlServer(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")
        , sqlServerdbContextOptionsBuilder =>
        {
            sqlServerdbContextOptionsBuilder.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
        }
        );
});


/*
 * Register asp.net core identity. 
 * */
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();


/*
 * Register Identity Server for production
 */
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<ApplicationUser>()
    .AddConfigurationStore(configurationStoreoptions =>
    {
        configurationStoreoptions.ResolveDbContextOptions = ResolveDbContextOptions;
    }) // stores config like api scopes, resources
    .AddOperationalStore(operationalStoreOptions =>
    {
        operationalStoreOptions.ResolveDbContextOptions = ResolveDbContextOptions;
    }); // keys, token, dates etc


var app = builder.Build();

// 

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
    await scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.MigrateAsync();
    await scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

    var userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    if (await userManger.FindByNameAsync("amit.chawla") == null)
    {
        await userManger.CreateAsync(new ApplicationUser
        {
            UserName = "amit.chawla",
            Email = "acamit84@gmail.com",
            GivenName = "Amit",
            FamilyName = "Chawla"
        }, password: "Pa55w0rd!");
    }
    var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

    /*
     * Api resources
     *
     */
    if (!await configurationDbContext.ApiResources.AnyAsync())
    {
        await configurationDbContext.ApiResources.AddAsync(new ApiResource()
        {
            Name = Guid.NewGuid().ToString(),
            DisplayName = "API",
            Scopes = new List<string>()
            {
                "https://ycompany.com/api"
            }
        }.ToEntity());
        await configurationDbContext.SaveChangesAsync();
    }

    /*
     * Api Scopes
     *
     */
    if (!await configurationDbContext.ApiScopes.AnyAsync())
    {
        await configurationDbContext.ApiScopes.AddAsync(new ApiScope()
        {
            Name = "https://ycompany.com/api", // same as above
            DisplayName = "API",
        }.ToEntity());

        await configurationDbContext.SaveChangesAsync();
    }

    /*
    * Clients
    *
    */
    if (!await configurationDbContext.Clients.AnyAsync())
    {
        await configurationDbContext.Clients.AddRangeAsync(new Client()
        {
            ClientId = Guid.NewGuid().ToString(),
            ClientSecrets = new List<Secret> { new Secret("secret".Sha512()) },
            ClientName = "Console Applications",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = new List<string>() { "https://ycompany.com/api" },
            AllowedCorsOrigins = new List<string>() { "https://api:7001" } // api url
        }.ToEntity(),
        new Client()
        {
            ClientId = Guid.NewGuid().ToString(),
            ClientSecrets = new List<Secret> { new Secret("secret".Sha512()) },
            ClientName = "Web Applications",
            AllowedGrantTypes = GrantTypes.Code,
            AllowedScopes = new List<string>() { "https://ycompany.com/api", "openid", "profile", "email", "https://ycompany.com/api" },
            RedirectUris = new List<string> { "https://webapplication:7002/signin-oidc" },
            PostLogoutRedirectUris = new List<string> { "https://webapplication:7002/signout-callback-oidc" }
        }.ToEntity(),

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
        }.ToEntity());

        await configurationDbContext.SaveChangesAsync();

        /*
         * Identity Resources.
         */
        if (!await configurationDbContext.IdentityResources.AnyAsync())
        {
            await configurationDbContext.IdentityResources.AddRangeAsync(
                new IdentityResources.OpenId().ToEntity(),
                new IdentityResources.Profile().ToEntity(),
                new IdentityResources.Email().ToEntity()
                );

            await configurationDbContext.SaveChangesAsync();
        }
    }
}

app.MapGet("/", () => "Hello World!");

app.Run();



void ResolveDbContextOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder dbContextOptionsBuilder)
{
    dbContextOptionsBuilder
        .UseSqlServer(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("IdentityServer")
                //, sqlServerdbContextOptionsBuilder =>
                //{
                //    sqlServerdbContextOptionsBuilder.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                //}
                ); // a sepearate db for identity server db
}
