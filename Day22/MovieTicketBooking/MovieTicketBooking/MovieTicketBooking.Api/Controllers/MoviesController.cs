
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.DTOs.Movies;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    // POST: api/movies
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateMovieRequest request)
    {
        var movie = await _movieService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = movie.MovieId },
            movie);
    }

    // GET: api/movies
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] MovieQueryParameters parameters)
    {
        var movies = await _movieService.GetAllAsync(parameters);

        return Ok(movies);
    }

    // GET: api/movies/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);

        if (movie == null)
            return NotFound(new
            {
                message = $"Movie with ID {id} was not found."
            });

        return Ok(movie);
    }

    // PUT: api/movies/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateMovieRequest request)
    {
        var movie = await _movieService.UpdateAsync(id, request);

        if (movie == null)
            return NotFound(new
            {
                message = $"Movie with ID {id} was not found."
            });

        return Ok(movie);
    }

    // DELETE: api/movies/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _movieService.DeleteAsync(id);

        if (!deleted)
            return Conflict(new
            {
                message = "Movie does not exist or cannot be deleted because dependent records exist."
            });

        return NoContent();
    }

    // GET: api/movies/search?title=Leo
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return BadRequest(new
            {
                message = "Search title is required."
            });

        var movies = await _movieService.SearchByTitleAsync(title);

        return Ok(movies);
    }
}
