
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.Data;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;

namespace MovieTicketBooking.Api.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly ApplicationDbContext _context;

    public ShowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> MovieExistsAsync(int movieId)
    {
        return await _context.Movies
            .AnyAsync(m => m.MovieId == movieId);
    }

    public async Task<bool> ScreenExistsAsync(int screenId)
    {
        return await _context.Screens
            .AnyAsync(s => s.ScreenId == screenId);
    }

    public async Task<List<Seat>> GetSeatsByScreenIdAsync(int screenId)
    {
        return await _context.Seats
            .AsNoTracking()
            .Where(s => s.ScreenId == screenId)
            .OrderBy(s => s.RowName)
            .ThenBy(s => s.SeatNumber)
            .ToListAsync();
    }

    public async Task<bool> HasOverlappingShowAsync(
        int screenId,
        DateTime startTime,
        DateTime endTime,
        int? excludeShowId = null)
    {
        var query = _context.Shows.Where(s =>
            s.ScreenId == screenId &&
            s.StartTime < endTime &&
            s.EndTime > startTime);

        if (excludeShowId.HasValue)
        {
            query = query.Where(
                s => s.ShowId != excludeShowId.Value);
        }

        return await query.AnyAsync();
    }

    public async Task<Show> CreateWithSeatsAsync(
        Show show,
        List<ShowSeat> showSeats)
    {
        await _context.Shows.AddAsync(show);
        await _context.ShowSeats.AddRangeAsync(showSeats);

        await _context.SaveChangesAsync();

        return show;
    }

    public async Task<List<Show>> GetAllAsync(
        int? movieId,
        int? theatreId,
        DateTime? date)
    {
        var query = _context.Shows.AsNoTracking();

        if (movieId.HasValue)
        {
            query = query.Where(s => s.MovieId == movieId.Value);
        }

        if (theatreId.HasValue)
        {
            query = query.Where(s =>
                _context.Screens.Any(sc =>
                    sc.ScreenId == s.ScreenId &&
                    sc.TheatreId == theatreId.Value));
        }

        if (date.HasValue)
        {
            var startOfDay = date.Value.Date;
            var nextDay = startOfDay.AddDays(1);

            query = query.Where(s =>
                s.StartTime >= startOfDay &&
                s.StartTime < nextDay);
        }

        return await query
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<Show?> GetByIdAsync(int id)
    {
        return await _context.Shows
            .FirstOrDefaultAsync(s => s.ShowId == id);
    }

    public async Task<List<Show>> GetByMovieIdAsync(int movieId)
    {
        return await _context.Shows
            .AsNoTracking()
            .Where(s => s.MovieId == movieId)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<bool> MovieExistsForFilterAsync(int movieId)
    {
        return await _context.Movies
            .AnyAsync(m => m.MovieId == movieId);
    }

    public async Task<List<Show>> GetByTheatreIdAsync(int theatreId)
    {
        return await (
            from show in _context.Shows.AsNoTracking()
            join screen in _context.Screens
                on show.ScreenId equals screen.ScreenId
            where screen.TheatreId == theatreId
            orderby show.StartTime
            select show
        ).ToListAsync();
    }

    public async Task<bool> TheatreExistsAsync(int theatreId)
    {
        return await _context.Theatres
            .AnyAsync(t => t.TheatreId == theatreId);
    }

    public async Task<bool> UpdateAsync(Show show)
    {
        _context.Shows.Update(show);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> HasBookingsAsync(int showId)
    {
        return await _context.Bookings
            .AnyAsync(b => b.ShowId == showId);
    }

    public async Task<bool> DeleteAsync(Show show)
    {
        var showSeats = await _context.ShowSeats
            .Where(ss => ss.ShowId == show.ShowId)
            .ToListAsync();

        _context.ShowSeats.RemoveRange(showSeats);
        _context.Shows.Remove(show);

        await _context.SaveChangesAsync();

        return true;
    }
}
