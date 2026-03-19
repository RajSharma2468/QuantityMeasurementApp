namespace QuantityMeasurementAPILayer.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestLoggingMiddleware>();
    }

    public static IApplicationBuilder UseApiKeyAuthentication(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ApiKeyAuthMiddleware>();
    }

    public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RateLimitingMiddleware>();
    }

    public static IApplicationBuilder UsePerformanceMonitoring(this IApplicationBuilder app)
    {
        return app.UseMiddleware<PerformanceMonitoringMiddleware>();
    }

    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SecurityHeadersMiddleware>();
    }

    public static IApplicationBuilder UseRequestValidation(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestValidationMiddleware>();
    }

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>();
    }

    public static IApplicationBuilder UseMaintenanceMode(this IApplicationBuilder app)
    {
        return app.UseMiddleware<MaintenanceModeMiddleware>();
    }

    // Extension method to register all middleware at once
    public static IApplicationBuilder UseAllMiddleware(this IApplicationBuilder app)
    {
        app.UseCorrelationId();
        app.UseRequestLogging();
        app.UseRequestValidation();
        app.UseSecurityHeaders();
        app.UseExceptionHandling();
        app.UsePerformanceMonitoring();
        app.UseMaintenanceMode();
        
        // Optional: Uncomment if you want to use API key authentication
        // app.UseApiKeyAuthentication();
        
        // Optional: Uncomment if you want to use rate limiting
        // app.UseRateLimiting();

        return app;
    }
}