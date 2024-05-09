using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Stripe;
using YCompany.HealthChecks;
using YCompany.Payments.Services.DomainServices;
using YCompany.Payments.Services.DomainServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add payment service
builder.Services.AddScoped<IPaymentService, PaymentService>();

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
                    .RequireClaim("scope", "https://ycompany.com/payments");
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
                Scopes = { { "https://ycompany.com/payments", "Payments API" } }
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
                "https://ycompany.com/payments"
            }
        }
    });
});

builder.Services.AddHealthChecks().AddCheck<StorageHealthChecks>("Storage");
builder.Services.AddHealthChecks().AddCheck<QueueHealthChecks>("Queue");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
