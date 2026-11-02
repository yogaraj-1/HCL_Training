
using Microsoft.AspNetCore.Mvc;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "Movie Ticket Booking API is running!",
            status = "Healthy"
        });
    }
}
