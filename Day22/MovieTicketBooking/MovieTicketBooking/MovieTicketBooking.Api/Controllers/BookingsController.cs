
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.Authorization.Requirements;
using MovieTicketBooking.Api.DTOs.Bookings;
using MovieTicketBooking.Api.Services.Interfaces;
using System.Security.Claims;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _service;
    private readonly IAuthorizationService _authorizationService;

    public BookingsController(
    IBookingService service,
    IAuthorizationService authorizationService)
    {
        _service = service;
        _authorizationService = authorizationService;
    }

    // POST: api/bookings
    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");

        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized();

        var booking = await _service.CreateBookingAsync(request, userId);

        return StatusCode(StatusCodes.Status201Created, booking);
    }

    // POST: api/bookings/1/cancel
    [Authorize(Roles = "User,Admin")]
    [HttpPost("{id:int}/cancel")]
    // POST: api/bookings/1/cancel
    [Authorize(Roles = "User,Admin")]
    [HttpPost("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        // 1. Retrieve booking
        var booking = await _service.GetBookingForAuthorizationAsync(id);

        if (booking == null)
            return NotFound();

        // 2. Check resource-based authorization
        var authorizationResult =
            await _authorizationService.AuthorizeAsync(
                User,
                booking,
                new BookingOwnerRequirement());

        if (!authorizationResult.Succeeded)
            return Forbid();

        // 3. Cancel booking
        await _service.CancelBookingAsync(booking);

        return Ok(new
        {
            message = "Booking cancelled successfully."
        });
    }

    // GET: api/bookings/my-bookings
    [Authorize(Roles = "User")]
    [HttpGet("my-bookings")]
    public async Task<IActionResult> GetMyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");

        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized();

        var bookings = await _service.GetMyBookingsAsync(userId);
            
        return Ok(bookings);
    }
}
