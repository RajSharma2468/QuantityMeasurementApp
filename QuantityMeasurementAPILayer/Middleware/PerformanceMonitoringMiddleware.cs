using System.Diagnostics;
using System.Collections.Concurrent;
namespace QuantityMeasurementAPILayer.Middleware;

public class PerformanceMonitoringMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMonitoringMiddleware> _logger;
    private static readonly ConcurrentDictionary<string, RequestMetrics> _metrics = new();

    public PerformanceMonitoringMiddleware(RequestDelegate next, ILogger<PerformanceMonitoringMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var path = context.Request.Path;

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            RecordMetrics(path, stopwatch.ElapsedMilliseconds, context.Response.StatusCode);
        }
    }

    private void RecordMetrics(string path, long elapsedMs, int statusCode)
    {
        var key = $"{path}:{statusCode}";
        
        _metrics.AddOrUpdate(key,
            new RequestMetrics 
            { 
                Count = 1, 
                TotalTime = elapsedMs,
                MinTime = elapsedMs,
                MaxTime = elapsedMs
            },
            (k, existing) =>
            {
                existing.Count++;
                existing.TotalTime += elapsedMs;
                existing.MinTime = Math.Min(existing.MinTime, elapsedMs);
                existing.MaxTime = Math.Max(existing.MaxTime, elapsedMs);
                return existing;
            });

        // Log slow requests (> 1 second)
        if (elapsedMs > 1000)
        {
            _logger.LogWarning("Slow request detected: {Path} took {ElapsedMs}ms", path, elapsedMs);
        }

        // Log every 100th request for monitoring
        if (DateTime.UtcNow.Second % 10 == 0)
        {
            LogMetricsSummary();
        }
    }

    private void LogMetricsSummary()
    {
        var summary = _metrics.Select(x => new
        {
            Endpoint = x.Key,
            x.Value.Count,
            AvgTime = x.Value.TotalTime / x.Value.Count,
            x.Value.MinTime,
            x.Value.MaxTime
        });

        _logger.LogInformation("Performance Metrics Summary: {@Metrics}", summary);
    }

    public static IReadOnlyDictionary<string, RequestMetrics> GetMetrics()
    {
        return _metrics;
    }

    public class RequestMetrics
    {
        public long Count { get; set; }
        public long TotalTime { get; set; }
        public long MinTime { get; set; }
        public long MaxTime { get; set; }
    }
}