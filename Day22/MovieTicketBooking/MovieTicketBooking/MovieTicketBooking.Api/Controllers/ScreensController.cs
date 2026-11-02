
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.DTOs.Screens;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScreensController : ControllerBase
{
    private readonly IScreenService _service;

    public ScreensController(IScreenService service)
    {
        _service = service;
    }

    // POST: api/screens
    [HttpPost]
    public async Task<IActionResult> Create(CreateScreenRequest request)
    {
        var screen = await _service.CreateAsync(request);

        if (screen == null)
        {
            return NotFound(new
            {
                message = $"Theatre with ID {request.TheatreId} was not found."
            });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = screen.ScreenId },
            screen);
    }

    // GET: api/screens
    // GET: api/screens?theatreId=1
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? theatreId)
    {
        if (theatreId.HasValue && theatreId.Value <= 0)
        {
            return BadRequest(new
            {
                message = "TheatreId must be greater than zero."
            });
        }

        var screens = await _service.GetAllAsync(theatreId);

        return Ok(screens);
    }

    // GET: api/screens/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var screen = await _service.GetByIdAsync(id);

        if (screen == null)
        {
            return NotFound(new
            {
                message = $"Screen with ID {id} was not found."
            });
        }

        return Ok(screen);
    }

    // GET: api/screens/theatre/1
    [HttpGet("theatre/{theatreId:int}")]
    public async Task<IActionResult> GetByTheatreId(int theatreId)
    {
        if (theatreId <= 0)
        {
            return BadRequest(new
            {
                message = "TheatreId must be greater than zero."
            });
        }

        var screens = await _service.GetByTheatreIdAsync(theatreId);

        if (screens == null)
        {
            return NotFound(new
            {
                message = $"Theatre with ID {theatreId} was not found."
            });
        }

        return Ok(screens);
    }

    // PUT: api/screens/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateScreenRequest request)
    {
        var screen = await _service.UpdateAsync(id, request);

        if (screen == null)
        {
            return NotFound(new
            {
                message = "Screen or specified theatre was not found."
            });
        }

        return Ok(screen);
    }

    // DELETE: api/screens/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return Conflict(new
            {
                message = "Screen was not found or cannot be deleted because dependent records exist."
            });
        }

        return NoContent();
    }
}
