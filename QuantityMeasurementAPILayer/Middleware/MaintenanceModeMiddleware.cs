using System.Net;

namespace QuantityMeasurementAPILayer.Middleware;

public class MaintenanceModeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MaintenanceModeMiddleware> _logger;
    private static bool _isMaintenanceMode = false;
    private static DateTime? _maintenanceEndTime;

    public MaintenanceModeMiddleware(RequestDelegate next, ILogger<MaintenanceModeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/health"))
        {
            await _next(context);
            return;
        }

        if (_isMaintenanceMode)
        {
            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            context.Response.Headers["Retry-After"] = "3600";  // Fix warning

            var response = new
            {
                error = "System is under maintenance",
                message = "The application is currently undergoing maintenance. Please try again later.",
                estimatedEndTime = _maintenanceEndTime?.ToString("yyyy-MM-dd HH:mm:ss UTC") ?? "Unknown"
            };

            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        await _next(context);
    }

    public static void EnableMaintenanceMode(TimeSpan? duration = null)
    {
        _isMaintenanceMode = true;
        _maintenanceEndTime = duration.HasValue 
            ? DateTime.UtcNow.Add(duration.Value) 
            : DateTime.UtcNow.AddHours(1);
    }

    public static void DisableMaintenanceMode()
    {
        _isMaintenanceMode = false;
        _maintenanceEndTime = null;
    }

    public static bool IsInMaintenanceMode() => _isMaintenanceMode;
}