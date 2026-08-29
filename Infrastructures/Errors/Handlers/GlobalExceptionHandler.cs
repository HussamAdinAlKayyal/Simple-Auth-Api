using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthApi.Infrastructures.Errors.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unexpected error occurred: {Message}", exception.Message);
        var (statusCode, title) = exception switch
        {
            KeyNotFoundException => (404, "Resource not found"),
            ArgumentException => (400, "Bad request"),
            _ => (500, "Server-side error"),
        };
        ProblemDetails details = new()
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
        };
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
        return true;
    }
}
