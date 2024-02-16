using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using YCompanyPaymentsAPI.Data;

using YCompany.CustomLogging;
using Microsoft.Extensions.Options;
using YCompany.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<InsuranceContext>((serviceProvider, dbContextOptionsBuilder) =>
{
    dbContextOptionsBuilder
        .UseSqlServer(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")
            , sqlServerdbContextOptionsBuilder =>
            {
                sqlServerdbContextOptionsBuilder.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            }
        );
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwtbearerOptions =>
    {
        jwtbearerOptions.Authority = builder.Configuration["Authentication:Authority"];
        jwtbearerOptions.Audience = builder.Configuration["Authentication:Audience"];
        jwtbearerOptions.TokenValidationParameters.ValidateAudience = true;
        jwtbearerOptions.TokenValidationParameters.ValidateIssuer = true;
        jwtbearerOptions.TokenValidationParameters.ValidateIssuerSigningKey = true;
    });

builder.Services.AddAuthorization(authorizationOptions =>
{
    authorizationOptions.AddPolicy("ApiScope", authorizationPolicyBuilder =>
    {
        authorizationPolicyBuilder.RequireAuthenticatedUser()
                    .RequireClaim("scope", "https://ycompany.com/thirdparty");
    });
});


builder.Services.AddControllers();
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});



//using IHost host = builder.Build();


builder.Services.AddSingleton<ILoggerProvider>(provider => new FileLoggerProvider(@"C:\Users\nitesh01\Downloads\YCompanydata.txt.txt"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            ClientCredentials = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri($"{builder.Configuration["Authentication:Authority"]}/connect/token"),
                Scopes = { { "https://ycompany.com/thirdparty", "ThirdParty API" } }
            }
        }
    });
    swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id ="oauth2" // same as above
                }
            },
            new List<string>{
                "https://ycompany.com/thirdparty"
            }
        }
    });
});

var app = builder.Build();

SecurityMetadata options = app.Services.GetRequiredService<IOptions<SecurityMetadata>>().Value;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
