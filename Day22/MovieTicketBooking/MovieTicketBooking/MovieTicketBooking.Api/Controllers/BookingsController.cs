
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.DTOs.Bookings;
using MovieTicketBooking.Api.Services.Interfaces;
using System.Security.Claims;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _service;

    public BookingsController(IBookingService service)
    {
        _service = service;
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
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");

        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized();

        bool isAdmin = User.IsInRole("Admin");

        await _service.CancelBookingAsync(id, userId, isAdmin);

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
