using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static IdentityModel.OidcConstants;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();

builder.Services
    .AddBff()
    .AddRemoteApis();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";
        options.ClientId = "bff";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.Scope.Add("ClaimsAPI");
        options.Scope.Add(StandardScopes.OpenId);
        options.Scope.Add(StandardScopes.Profile);
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
    });
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseBff();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapBffManagementEndpoints();
    // Uncomment this for Controller support
    // endpoints.MapControllers()
    //     .AsBffApiEndpoint();

    endpoints.MapGet("/local/identity", LocalIdentityHandler)
        .AsBffApiEndpoint();

    /*
     *  anything with remote is sent to this base url. In the url used in js. /remote is basically replaced by this url. 
     *  /remote/identity will actually call the api at https://localhost:7047/identity
    */
    endpoints.MapRemoteBffApiEndpoint("/remote", "https://localhost:7047")
        .RequireAccessToken(Duende.Bff.TokenType.User);

});

app.Run();




[Authorize]
static IResult LocalIdentityHandler(ClaimsPrincipal user)
{
    var name = user.FindFirst("name")?.Value ?? user.FindFirst("sub")?.Value;
    return Results.Json(new { message = "Local API Success!", user = name });
}