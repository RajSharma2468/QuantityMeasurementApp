using System.Collections.Concurrent;
using System.Net;

namespace QuantityMeasurementAPILayer.Middleware;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitingMiddleware> _logger;
    
    private static readonly ConcurrentDictionary<string, ClientRequestInfo> _clientRequests = new();
    private readonly int _maxRequests = 100;
    private readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1);

    public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientId = GetClientIdentifier(context);

        var requestInfo = _clientRequests.AddOrUpdate(clientId,
            new ClientRequestInfo { RequestCount = 1, WindowStart = DateTime.UtcNow },
            (key, existingInfo) =>
            {
                if (DateTime.UtcNow - existingInfo.WindowStart > _timeWindow)
                {
                    return new ClientRequestInfo { RequestCount = 1, WindowStart = DateTime.UtcNow };
                }
                
                existingInfo.RequestCount++;
                return existingInfo;
            });

        if (requestInfo.RequestCount > _maxRequests)
        {
            _logger.LogWarning("Rate limit exceeded for client: {ClientId}", clientId);
            
            context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            context.Response.Headers["Retry-After"] = _timeWindow.TotalSeconds.ToString();  // Fix warning
            
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Rate limit exceeded. Please try again later.",
                limit = _maxRequests,
                windowInSeconds = _timeWindow.TotalSeconds
            });
            
            return;
        }

        // Fix warnings - use indexer
        context.Response.Headers["X-RateLimit-Limit"] = _maxRequests.ToString();
        context.Response.Headers["X-RateLimit-Remaining"] = (_maxRequests - requestInfo.RequestCount).ToString();
        context.Response.Headers["X-RateLimit-Reset"] = requestInfo.WindowStart.Add(_timeWindow).ToString("O");

        await _next(context);
    }

    private string GetClientIdentifier(HttpContext context)
    {
        var apiKey = context.Request.Headers["X-API-Key"].FirstOrDefault();
        if (!string.IsNullOrEmpty(apiKey))
        {
            return $"apikey:{apiKey}";
        }

        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return $"ip:{ipAddress}";
    }

    private class ClientRequestInfo
    {
        public int RequestCount { get; set; }
        public DateTime WindowStart { get; set; }
    }
}