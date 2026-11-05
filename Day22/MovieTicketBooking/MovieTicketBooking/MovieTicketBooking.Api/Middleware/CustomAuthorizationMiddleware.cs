
using System.Security.Claims;

namespace MovieTicketBooking.Api.Middleware;

public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Apply custom authorization only to Admin APIs
        if (context.Request.Path.StartsWithSegments("/api/admin"))
        {
            // Check whether the user is authenticated
            if (context.User.Identity?.IsAuthenticated != true)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized access.");
                return;
            }

            // Check whether the user has Admin role
            if (!context.User.IsInRole("Admin"))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied. Admin only.");
                return;
            }
        }

        // Allow request to continue
        await _next(context);
    }
}
