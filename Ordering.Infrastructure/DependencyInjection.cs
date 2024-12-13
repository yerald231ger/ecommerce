namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("OrderingDb");
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddDbContext<OrderingDbContext>((serviceProvider, options) =>
        {
            var interceptors = serviceProvider.GetServices<ISaveChangesInterceptor>();
            options.AddInterceptors(interceptors);
            options.UseSqlServer(connectionString);
        });
        return services;
    }
}