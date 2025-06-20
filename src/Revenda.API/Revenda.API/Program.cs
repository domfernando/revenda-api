using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Revenda.API.Middleware;
using Revenda.Application;
using Revenda.Infra;
using Serilog;
using Serilog.Enrichers.Span;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithSpan()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File("Logs/api-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Revenda API", Version = "v1" });

    // Incluir comentários XML
    var xmlFile = $"RevendaAPI.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

#endregion

#region Application/Infra
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfra(builder.Configuration);
#endregion

#region Model validation error handling
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        return new BadRequestObjectResult(new
        {
            Message = "Dados inválidos",
            Errors = errors
        });
    };
});
#endregion

#region OpenTelemetry 
var serviceName = "Revenda.Api";
var serviceVersion = "1.0.0";

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName, serviceVersion)
        .AddAttributes(new Dictionary<string, object>
        {
            ["deployment.environment"] = builder.Environment.EnvironmentName,
            ["service.instance.id"] = Environment.MachineName
        }))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation(options =>
        {
            options.RecordException = true;
            options.EnrichWithHttpRequest = (activity, request) =>
            {
                activity.SetTag("http.request.body.size", request.ContentLength ?? 0);
                activity.SetTag("user.id", request.Headers["X-User-Id"].FirstOrDefault());
            };
            options.EnrichWithHttpResponse = (activity, response) =>
            {
                activity.SetTag("http.response.body.size", response.ContentLength ?? 0);
            };
        })
        .AddHttpClientInstrumentation()
        .AddEntityFrameworkCoreInstrumentation()
        .AddSource("Revenda.Application.ProductService")
        .AddSource("Revenda.Application.ClientService")
        .AddSource("Revenda.Application.SaleService")
        .SetSampler(new TraceIdRatioBasedSampler(1.0))
        .AddConsoleExporter()
        .AddJaegerExporter());

#region OpenTelemetry 
//var serviceName = "Revenda.Api";
//var serviceVersion = "1.0.0";

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName, serviceVersion)
        .AddAttributes(new Dictionary<string, object>
        {
            ["deployment.environment"] = builder.Environment.EnvironmentName,
            ["service.instance.id"] = Environment.MachineName
        }))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation(options =>
        {
            options.RecordException = true;
            options.EnrichWithHttpRequest = (activity, request) =>
            {
                activity.SetTag("http.request.body.size", request.ContentLength ?? 0);
                activity.SetTag("user.id", request.Headers["X-User-Id"].FirstOrDefault());
            };
            options.EnrichWithHttpResponse = (activity, response) =>
            {
                activity.SetTag("http.response.body.size", response.ContentLength ?? 0);
            };
        })
        .AddHttpClientInstrumentation()
        .AddEntityFrameworkCoreInstrumentation()
        .AddSource("Revenda.Application.ProductService")
        .AddSource("Revenda.Application.ClientService")
        .AddSource("Revenda.Application.SaleService")
        .SetSampler(new TraceIdRatioBasedSampler(1.0))
        .AddConsoleExporter()
        .AddJaegerExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddRuntimeInstrumentation()
        .AddMeter("Revenda.Application.ProductService")
        .AddMeter("Revenda.Application.ClientService")
        .AddMeter("Revenda.Application.SaleService")
        .AddConsoleExporter()
        .AddPrometheusExporter());
#endregion

#region Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddCheck<DatabaseHealthCheck>("database")
    .AddCheck<ExternalApiHealthCheck>("external-api");
#endregion

var app = builder.Build();

#region Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

#region Health Check endpoints
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(x => new
            {
                name = x.Key,
                status = x.Value.Status.ToString(),
                exception = x.Value.Exception?.Message,
                duration = x.Value.Duration.ToString()
            })
        };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
});

app.MapHealthChecks("/health/ready");
app.MapHealthChecks("/health/live");
#endregion

#region Prometheus metrics endpoint
app.MapPrometheusScrapingEndpoint();
#endregion

app.MapControllers();

try
{
    Log.Information("Iniciando aplicação {ServiceName} v{ServiceVersion}", serviceName, serviceVersion);
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplicação terminou inesperadamente");
}
finally
{
    await Log.CloseAndFlushAsync();
}

#endregion

app.Run();
