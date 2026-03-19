using System.Text.RegularExpressions;
using System.Net;

namespace QuantityMeasurementAPILayer.Middleware;

public class RequestValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestValidationMiddleware> _logger;
    private static readonly Regex _sqlInjectionPattern = new(
        @"(\s*SELECT\s+.*\s+FROM\s+)|(\s*INSERT\s+INTO\s+)|(\s*UPDATE\s+.*\s+SET\s+)|(\s*DELETE\s+FROM\s+)|(\s*DROP\s+TABLE\s+)|(\s*CREATE\s+TABLE\s+)|(--)|(\bOR\b.*=)|(\bAND\b.*=)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public RequestValidationMiddleware(RequestDelegate next, ILogger<RequestValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!await IsRequestValid(context))
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new 
            { 
                error = "Request contains potentially malicious content" 
            });
            return;
        }

        await _next(context);
    }

    private async Task<bool> IsRequestValid(HttpContext context)
    {
        // Check query string for SQL injection
        if (HasSqlInjection(context.Request.QueryString.Value))
        {
            _logger.LogWarning("SQL injection attempt detected in query string: {Query}", 
                context.Request.QueryString.Value);
            return false;
        }

        // Check request body for SQL injection (for POST/PUT requests)
        if (context.Request.Method == HttpMethods.Post || 
            context.Request.Method == HttpMethods.Put)
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (HasSqlInjection(body))
            {
                _logger.LogWarning("SQL injection attempt detected in request body");
                return false;
            }
        }

        // Check for path traversal attempts
        var path = context.Request.Path.Value;
        if (path != null && (path.Contains("../") || path.Contains("..\\")))
        {
            _logger.LogWarning("Path traversal attempt detected: {Path}", path);
            return false;
        }

        return true;
    }

    private bool HasSqlInjection(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return _sqlInjectionPattern.IsMatch(input);
    }
}