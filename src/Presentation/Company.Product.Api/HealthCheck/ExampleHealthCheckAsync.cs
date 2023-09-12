using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Company.Product.Api.HealthCheck;

internal class ExampleServiceHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("ExampleService is healthy."));
        }
        catch (Exception)
        {
            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, "ExampleService is unhealthy."));
        }
    }
}