using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.DTOs.Seats;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeatsController : ControllerBase
{
    private readonly ISeatService _service;

    public SeatsController(ISeatService service)
    {
        _service = service;
    }

    // POST: api/seats
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateSeatRequest request)
    {
        var seat = await _service.CreateAsync(request);

        if (seat == null)
        {
            return NotFound(new
            {
                message = $"Screen with ID {request.ScreenId} was not found."
            });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = seat.SeatId },
            seat);
    }

    // POST: api/seats/bulk
    [Authorize(Roles = "Admin")]
    [HttpPost("bulk")]
    public async Task<IActionResult> CreateBulk(
        BulkCreateSeatsRequest request)
    {
        var seats = await _service.CreateBulkAsync(request);

        if (seats == null)
        {
            return NotFound(new
            {
                message = $"Screen with ID {request.ScreenId} was not found."
            });
        }

        return StatusCode(
            StatusCodes.Status201Created,
            seats);
    }

    // GET: api/seats/screen/1
    [AllowAnonymous]
    [HttpGet("screen/{screenId:int}")]
    public async Task<IActionResult> GetByScreenId(int screenId)
    {
        if (screenId <= 0)
            return BadRequest(new { message = "ScreenId must be greater than zero." });

        var seats = await _service.GetByScreenIdAsync(screenId);

        if (seats == null)
        {
            return NotFound(new
            {
                message = $"Screen with ID {screenId} was not found."
            });
        }

        return Ok(seats);
    }

    // GET: api/seats/1
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var seat = await _service.GetByIdAsync(id);

        if (seat == null)
        {
            return NotFound(new
            {
                message = $"Seat with ID {id} was not found."
            });
        }

        return Ok(seat);
    }

    // PUT: api/seats/1
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateSeatRequest request)
    {
        var seat = await _service.UpdateAsync(id, request);

        if (seat == null)
        {
            return NotFound(new
            {
                message = "Seat or specified screen was not found."
            });
        }

        return Ok(seat);
    }

    // DELETE: api/seats/1
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return Conflict(new
            {
                message = "Seat was not found or cannot be deleted because dependent records exist."
            });
        }

        return NoContent();
    }
}