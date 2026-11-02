
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.Data;
using MovieTicketBooking.Api.DTOs.Movies;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;

namespace MovieTicketBooking.Api.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // CREATE
    public async Task<Movie> CreateAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);

        await _context.SaveChangesAsync();

        return movie;
    }

    // GET BY ID
    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _context.Movies
            .FirstOrDefaultAsync(m => m.MovieId == id);
    }

    // GET ALL + FILTER + SORT + PAGINATION
    public async Task<PagedResponse<Movie>> GetAllAsync(
        MovieQueryParameters parameters)
    {
        IQueryable<Movie> query = _context.Movies.AsNoTracking();

        // FILTER BY TITLE
        if (!string.IsNullOrWhiteSpace(parameters.Title))
        {
            string title = parameters.Title.Trim();

            query = query.Where(m =>
                m.Title.Contains(title));
        }

        // FILTER BY GENRE
        if (!string.IsNullOrWhiteSpace(parameters.Genre))
        {
            string genre = parameters.Genre.Trim();

            query = query.Where(m =>
                m.Genre == genre);
        }

        // FILTER BY LANGUAGE
        if (!string.IsNullOrWhiteSpace(parameters.Language))
        {
            string language = parameters.Language.Trim();

            query = query.Where(m =>
                m.Language == language);
        }

        // COUNT BEFORE PAGINATION
        int totalRecords = await query.CountAsync();

        // SORTING
        bool descending = string.Equals(
            parameters.SortOrder,
            "desc",
            StringComparison.OrdinalIgnoreCase);

        query = parameters.SortBy?.ToLowerInvariant() switch
        {
            "title" => descending
                ? query.OrderByDescending(m => m.Title)
                : query.OrderBy(m => m.Title),

            "releasedate" => descending
                ? query.OrderByDescending(m => m.ReleaseDate)
                : query.OrderBy(m => m.ReleaseDate),

            "durationminutes" => descending
                ? query.OrderByDescending(m => m.DurationMinutes)
                : query.OrderBy(m => m.DurationMinutes),

            _ => descending
                ? query.OrderByDescending(m => m.MovieId)
                : query.OrderBy(m => m.MovieId)
        };

        // PAGINATION
        int skip = (parameters.PageNumber - 1)
                   * parameters.PageSize;

        List<Movie> movies = await query
            .Skip(skip)
            .Take(parameters.PageSize)
            .ToListAsync();

        int totalPages = (int)Math.Ceiling(
            totalRecords / (double)parameters.PageSize);

        return new PagedResponse<Movie>
        {
            Items = movies,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };
    }

    // UPDATE
    public async Task<Movie> UpdateAsync(Movie movie)
    {
        _context.Movies.Update(movie);

        await _context.SaveChangesAsync();

        return movie;
    }

    // DELETE
    public async Task<bool> DeleteAsync(Movie movie)
    {
        _context.Movies.Remove(movie);

        await _context.SaveChangesAsync();

        return true;
    }

    // SEARCH BY TITLE
    public async Task<IEnumerable<Movie>> SearchByTitleAsync(
        string title)
    {
        return await _context.Movies
            .AsNoTracking()
            .Where(m => m.Title.Contains(title))
            .OrderBy(m => m.Title)
            .ToListAsync();
    }
}
