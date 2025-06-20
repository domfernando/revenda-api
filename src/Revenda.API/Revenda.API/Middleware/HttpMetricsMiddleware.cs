using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Revenda.API.Middleware
{
    public class HttpMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Meter _meter;
        private readonly Counter<int> _requestCounter;
        private readonly Histogram<double> _requestDuration;

        public HttpMetricsMiddleware(RequestDelegate next)
        {
            _next = next;
            _meter = new Meter("ProductApi.Http");

            _requestCounter = _meter.CreateCounter<int>(
                "http_requests_total",
                description: "Total number of HTTP requests");

            _requestDuration = _meter.CreateHistogram<double>(
                "http_request_duration_seconds",
                description: "Duration of HTTP requests");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                var tags = new[]
                {
                new KeyValuePair<string, object?>("method", context.Request.Method),
                new KeyValuePair<string, object?>("endpoint", context.Request.Path),
                new KeyValuePair<string, object?>("status_code", context.Response.StatusCode)
            };

                _requestCounter.Add(1, tags);
                _requestDuration.Record(stopwatch.Elapsed.TotalSeconds, tags);
            }
        }
    }
}
