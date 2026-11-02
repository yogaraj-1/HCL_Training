
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.DTOs.Theatres;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TheatresController : ControllerBase
{
    private readonly ITheatreService _service;

    public TheatresController(ITheatreService service)
    {
        _service = service;
    }

    // POST: api/theatres
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTheatreRequest request)
    {
        var theatre = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = theatre.TheatreId },
            theatre);
    }

    // GET: api/theatres
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var theatres = await _service.GetAllAsync();

        return Ok(theatres);
    }

    // GET: api/theatres/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var theatre = await _service.GetByIdAsync(id);

        if (theatre == null)
            return NotFound(new
            {
                message = $"Theatre with ID {id} was not found."
            });

        return Ok(theatre);
    }

    // PUT: api/theatres/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateTheatreRequest request)
    {
        var theatre = await _service.UpdateAsync(id, request);

        if (theatre == null)
            return NotFound(new
            {
                message = $"Theatre with ID {id} was not found."
            });

        return Ok(theatre);
    }

    // DELETE: api/theatres/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return Conflict(new
            {
                message = "Theatre was not found or cannot be deleted because dependent records exist."
            });
        }

        return NoContent();
    }
}
