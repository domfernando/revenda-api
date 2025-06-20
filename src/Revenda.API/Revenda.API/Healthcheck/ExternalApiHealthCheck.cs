using Microsoft.Extensions.Diagnostics.HealthChecks;
public class ExternalApiHealthCheck : IHealthCheck
{
    private readonly HttpClient _httpClient;

    public ExternalApiHealthCheck(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Simula chamada para API externa
            await Task.Delay(100, cancellationToken);
            return HealthCheckResult.Healthy("External API is responding");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("External API is not responding", ex);
        }
    }
}
