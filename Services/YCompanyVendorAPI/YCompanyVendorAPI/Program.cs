using Microsoft.EntityFrameworkCore;
using System.Reflection;
using YCompany.Vendor.DataAccess;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RepositoryDbContext>((serviceProvider, dbContextOptionsBuilder) =>
{
    dbContextOptionsBuilder
        .UseSqlServer(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")
            , sqlServerdbContextOptionsBuilder =>
            {
                sqlServerdbContextOptionsBuilder.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            }
        );
});

// Add services to the container.

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
