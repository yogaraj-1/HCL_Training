using Microsoft.AspNetCore.Authorization;
using MovieTicketBooking.Api.Authorization.Requirements;
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Authorization.Handlers;

public class BookingOwnerAuthorizationHandler
    : AuthorizationHandler<BookingOwnerRequirement, Booking>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        BookingOwnerRequirement requirement,
        Booking booking)
    {
        // Admin can access any booking
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        // Get the logged-in user's ID from JWT claims
        var userId = context.User.FindFirst(
            System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        // Check booking ownership
        if (userId != null && booking.UserId == userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}