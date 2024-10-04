using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace UselessCalculator.Api.Middleware;
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        var statusCode = GetStatusCodeForException(exception);

        var problemDetails = CreateProblemDetails(exception, statusCode);

        await WriteProblemDetailsToResponse(context, statusCode, problemDetails, cancellationToken);
        return true;
    }

    private static async Task WriteProblemDetailsToResponse(HttpContext context, int statusCode, ProblemDetails problemDetails, CancellationToken cancellationToken)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }

    private static ProblemDetails CreateProblemDetails(Exception exception, int statusCode)
    {
        return new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode switch
            {
                StatusCodes.Status404NotFound => "Not Found",
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status400BadRequest => "Bad Request",
                _ => "Internal Server Error"
            },
            Detail = exception.Message
        };
    }

    private static int GetStatusCodeForException(Exception exception)
    {
        var statusCode = exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        return statusCode;
    }
}

