
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.Data;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;

namespace MovieTicketBooking.Api.Repositories;

public class ScreenRepository : IScreenRepository
{
    private readonly ApplicationDbContext _context;

    public ScreenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Screen> CreateAsync(Screen screen)
    {
        await _context.Screens.AddAsync(screen);
        await _context.SaveChangesAsync();

        return screen;
    }

    public async Task<List<Screen>> GetAllAsync(int? theatreId = null)
    {
        var query = _context.Screens.AsNoTracking();

        if (theatreId.HasValue)
        {
            query = query.Where(s => s.TheatreId == theatreId.Value);
        }

        return await query
            .OrderBy(s => s.ScreenId)
            .ToListAsync();
    }

    public async Task<Screen?> GetByIdAsync(int id)
    {
        return await _context.Screens
            .FirstOrDefaultAsync(s => s.ScreenId == id);
    }

    public async Task<List<Screen>> GetByTheatreIdAsync(int theatreId)
    {
        return await _context.Screens
            .AsNoTracking()
            .Where(s => s.TheatreId == theatreId)
            .OrderBy(s => s.ScreenId)
            .ToListAsync();
    }

    public async Task<bool> TheatreExistsAsync(int theatreId)
    {
        return await _context.Theatres
            .AnyAsync(t => t.TheatreId == theatreId);
    }

    public async Task<Screen> UpdateAsync(Screen screen)
    {
        _context.Screens.Update(screen);
        await _context.SaveChangesAsync();

        return screen;
    }

    public async Task<bool> DeleteAsync(Screen screen)
    {
        _context.Screens.Remove(screen);
        await _context.SaveChangesAsync();

        return true;
    }
}
