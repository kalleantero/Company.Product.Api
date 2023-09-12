using Company.Product.Api.HealthCheck;

namespace Company.Product.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHealthChecks()
            .AddCheck<ExampleServiceHealthCheck>("ExampleService");

        return services;
    }
}