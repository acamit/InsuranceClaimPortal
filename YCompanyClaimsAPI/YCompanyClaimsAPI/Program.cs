using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7001/"; // Identity server URL
        options.TokenValidationParameters = new TokenValidationParameters
        {
            /*
            Audience validation is disabled here because access to the api is modeled with ApiScopes only. By default, no audience will be emitted unless the api is modeled with ApiResources instead. See here for a more in-depth discussion.
            https://docs.duendesoftware.com/identityserver/v6/apis/aspnetcore/jwt/#adding-audience-validation
            */
            ValidateAudience = false
        };
    });


/**
 * without this, api will accept any token provided by the authentication server.
 * add an Authorization Policy to the API that will check for the presence of the “ClaimsAPI” scope in the access token. 
 * 
 */

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "ClaimsAPI");
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();


//app.MapControllers();
/*
 *
 *link auth policy to controllers
 */

app.MapControllers()
    .RequireAuthorization("ApiScope");


app.Run();
