using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.DTOs.Shows;
using MovieTicketBooking.Api.Services;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowsController : ControllerBase
{
    private readonly IShowService _service;

    public ShowsController(IShowService service)
    {
        _service = service;
    }

    // POST: api/shows
    [HttpPost]
    public async Task<IActionResult> Create(CreateShowRequest request)
    {
        var show = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = show.ShowId },
            show);
    }

    // GET: api/shows?movieId=1&theatreId=1&date=2026-11-05
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int? movieId,
        [FromQuery] int? theatreId,
        [FromQuery] DateTime? date)
    {
        if (movieId.HasValue && movieId.Value <= 0)
            return BadRequest(new { message = "MovieId must be greater than zero." });

        if (theatreId.HasValue && theatreId.Value <= 0)
            return BadRequest(new { message = "TheatreId must be greater than zero." });

        var shows = await _service.GetAllAsync(movieId, theatreId, date);

        return Ok(shows);
    }

    // GET: api/shows/search?date=2026-11-05
    [HttpGet("search")]
    public async Task<IActionResult> SearchByDate([FromQuery] DateTime? date)
    {
        if (!date.HasValue)
            return BadRequest(new { message = "Please provide a date." });

        var shows = await _service.GetAllAsync(null, null, date);

        return Ok(shows);
    }

    // GET: api/shows/movie/1
    [HttpGet("movie/{movieId:int}")]
    public async Task<IActionResult> GetByMovieId(int movieId)
    {
        if (movieId <= 0)
            return BadRequest(new { message = "MovieId must be greater than zero." });

        var shows = await _service.GetByMovieIdAsync(movieId);

        if (shows == null)
            return NotFound(new { message = $"Movie {movieId} was not found." });

        return Ok(shows);
    }

    // GET: api/shows/theatre/1
    [HttpGet("theatre/{theatreId:int}")]
    public async Task<IActionResult> GetByTheatreId(int theatreId)
    {
        if (theatreId <= 0)
            return BadRequest(new { message = "TheatreId must be greater than zero." });

        var shows = await _service.GetByTheatreIdAsync(theatreId);

        if (shows == null)
            return NotFound(new { message = $"Theatre {theatreId} was not found." });

        return Ok(shows);
    }

    // GET: api/shows/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var show = await _service.GetByIdAsync(id);

        if (show == null)
            return NotFound(new { message = $"Show {id} was not found." });

        return Ok(show);
    }

    // PUT: api/shows/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateShowRequest request)
    {
        var show = await _service.UpdateAsync(id, request);

        if (show == null)
            return NotFound(new { message = $"Show {id} was not found." });

        return Ok(show);
    }

    // DELETE: api/shows/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound(new { message = $"Show {id} was not found or could not be deleted." });

        return NoContent();
    }

    // GET: api/shows/1/seats
    [HttpGet("{showId:int}/seats")]
    public async Task<IActionResult> GetSeatLayout(int showId)
    {
        var seats = await _service.GetSeatLayoutAsync(showId);

        if (seats == null)
            return NotFound(new { message = "Show not found." });

        return Ok(seats);
    }

    // GET: api/shows/1/seats/available
    [HttpGet("{showId:int}/seats/available")]
    public async Task<IActionResult> GetAvailableSeats(int showId)
    {
        var seats = await _service.GetAvailableSeatsAsync(showId);

        if (seats == null)
            return NotFound(new { message = "Show not found." });

        return Ok(seats);
    }

    // GET: api/shows/1/seats/booked
    [HttpGet("{showId:int}/seats/booked")]
    public async Task<IActionResult> GetBookedSeats(int showId)
    {
        var seats = await _service.GetBookedSeatsAsync(showId);

        if (seats == null)
            return NotFound(new { message = "Show not found." });

        return Ok(seats);
    }
}