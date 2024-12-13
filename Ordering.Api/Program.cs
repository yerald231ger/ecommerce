using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.SqlServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configurations = builder.Configuration;

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(configurations)
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitializeOrderingDbAsync();
}

app.Run();