using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MovieTicketBooking.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            UnauthorizedAccessException =>
                StatusCodes.Status401Unauthorized,

            KeyNotFoundException =>
                StatusCodes.Status404NotFound,

            ArgumentException =>
                StatusCodes.Status400BadRequest,

            InvalidOperationException =>
                StatusCodes.Status409Conflict,

            _ => StatusCodes.Status500InternalServerError
        };

        _logger.LogError(
            exception,
            "An exception occurred: {Message}",
            exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = GetTitle(statusCode),
            Detail = statusCode == 500
                ? "An unexpected error occurred."
                : exception.Message,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }

    private static string GetTitle(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            404 => "Resource Not Found",
            409 => "Conflict",
            _ => "Internal Server Error"
        };
    }
}