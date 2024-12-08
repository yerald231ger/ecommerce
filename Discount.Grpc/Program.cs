using System.Data;
using Discount.Grpc.Data;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSqlite<DiscountDbContext>(
    builder.Configuration.GetConnectionString("DiscountDb")
    ?? throw new NoNullAllowedException());

var app = builder.Build();

app.UseMigrationDiscount();
app.MapGrpcService<DiscountService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();