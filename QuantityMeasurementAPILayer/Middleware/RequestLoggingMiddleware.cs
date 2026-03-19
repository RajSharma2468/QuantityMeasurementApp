using System.Diagnostics;
using System.Text;

namespace QuantityMeasurementAPILayer.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        // Log request
        await LogRequest(context);

        // Capture response body
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);

            // Log response
            await LogResponse(context, responseBody, stopwatch.ElapsedMilliseconds);

            // Copy the response body back to the original stream
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private async Task LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering();

        var requestBody = await ReadRequestBody(context.Request);
        
        _logger.LogInformation(
            "HTTP Request: {Method} {Path} - Query: {Query} - Body: {Body}",
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString,
            requestBody);

        context.Request.Body.Position = 0;
    }

    private async Task LogResponse(HttpContext context, MemoryStream responseBody, long elapsedMs)
    {
        responseBody.Seek(0, SeekOrigin.Begin);
        var responseContent = await new StreamReader(responseBody).ReadToEndAsync();

        _logger.LogInformation(
            "HTTP Response: {StatusCode} - Body: {Body} - Elapsed: {ElapsedMs}ms",
            context.Response.StatusCode,
            responseContent,
            elapsedMs);
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        if (request.Body == null || !request.Body.CanRead)
        {
            return string.Empty;
        }

        using var reader = new StreamReader(
            request.Body,
            Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            leaveOpen: true);

        var body = await reader.ReadToEndAsync();
        return body.Length > 1000 ? body[..1000] + "..." : body;
    }
}