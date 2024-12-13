namespace Ordering.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {
        return app;
    }
}