using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extentions
{
    public static IApplicationBuilder UseMigrationDiscount(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var discountDbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
        discountDbContext.Database.MigrateAsync();
        return app;
    }
}