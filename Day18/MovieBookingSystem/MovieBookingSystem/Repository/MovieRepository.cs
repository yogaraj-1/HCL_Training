
using Microsoft.EntityFrameworkCore;
using MovieBookingSystem.Data;
using MovieBookingSystem.Models;

namespace MovieBookingSystem.Repository;

public class MovieRepository
{
    private readonly MovieBookingDbContext _context;

    public MovieRepository(MovieBookingDbContext context)
    {
        _context = context;
    }

    public async Task AddMovieAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        return await _context.Movies
            .AsNoTracking()
            .OrderBy(m => m.MovieId)
            .ToListAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MovieId == id);
    }
}
