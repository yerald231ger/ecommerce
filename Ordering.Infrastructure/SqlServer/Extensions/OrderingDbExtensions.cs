namespace Ordering.Infrastructure.SqlServer.Extensions;

public static class OrderingDbExtensions
{
    public static Task InitializeOrderingDbAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();
        
        context.Database.MigrateAsync().GetAwaiter().GetResult();
        context.SeedOrderingDbAsync().GetAwaiter().GetResult();
        
        return Task.CompletedTask;
    }

    private static async Task SeedOrderingDbAsync(this OrderingDbContext context)
    {
        await SeedCustomersAsync(context);
        await SeedProductsAsync(context);
        await SeedOrdersWithItemsAsync(context);
    }

    private static async Task SeedCustomersAsync(this OrderingDbContext context)
    {
        if (context.Customers.Any())
            return;

        await context.Customers.AddRangeAsync(InitialData.Customers);
        await context.SaveChangesAsync();
    }
    
    private static async Task SeedProductsAsync(this OrderingDbContext context)
    {
        if (context.Products.Any())
            return;

        await context.Products.AddRangeAsync(InitialData.Products);
        await context.SaveChangesAsync();
    }
    
    private static async Task SeedOrdersWithItemsAsync(this OrderingDbContext context)
    {
        if (context.Orders.Any())
            return;

        await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
        await context.SaveChangesAsync();
    }
    
}