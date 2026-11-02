using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.DTOs.Bookings;
using MovieTicketBooking.Api.Services.Interfaces;

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
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingRequest request)
    {
        try
        {
            var booking = await _service.CreateBookingAsync(request);

            return StatusCode(StatusCodes.Status201Created, booking);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: api/bookings/1/cancel
    [HttpPost("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        try
        {
            var cancelled = await _service.CancelBookingAsync(id);

            if (!cancelled)
                return NotFound(new { message = "Booking not found." });

            return Ok(new
            {
                message = "Booking cancelled successfully."
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}