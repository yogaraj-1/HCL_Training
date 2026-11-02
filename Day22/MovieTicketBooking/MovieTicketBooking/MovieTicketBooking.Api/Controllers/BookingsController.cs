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
}