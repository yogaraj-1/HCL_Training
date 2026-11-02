using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.DTOs.Bookings;
using MovieTicketBooking.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        var booking = await _service.CreateBookingAsync(request);

        return StatusCode(StatusCodes.Status201Created, booking);
    }

    // POST: api/bookings/1/cancel
    [Authorize(Roles = "User,Admin")]
    [HttpPost("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var cancelled = await _service.CancelBookingAsync(id);

        if (!cancelled)
            return NotFound(new { message = "Booking not found." });

        return Ok(new
        {
            message = "Booking cancelled successfully."
        });
    }
}