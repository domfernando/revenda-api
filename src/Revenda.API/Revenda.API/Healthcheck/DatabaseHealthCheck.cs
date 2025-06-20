using Microsoft.Extensions.Diagnostics.HealthChecks;
public class DatabaseHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        // Simula verificação de banco de dados
        var isHealthy = Random.Shared.Next(1, 10) > 1; // 90% chance de estar saudável

        if (isHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Database is responding"));
        }

        return Task.FromResult(HealthCheckResult.Unhealthy("Database is not responding"));
    }
}
