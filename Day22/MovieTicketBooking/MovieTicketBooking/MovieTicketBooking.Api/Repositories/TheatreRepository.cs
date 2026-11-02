
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.Data;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;

namespace MovieTicketBooking.Api.Repositories;

public class TheatreRepository : ITheatreRepository
{
    private readonly ApplicationDbContext _context;

    public TheatreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Theatre> CreateAsync(Theatre theatre)
    {
        await _context.Theatres.AddAsync(theatre);
        await _context.SaveChangesAsync();

        return theatre;
    }

    public async Task<List<Theatre>> GetAllAsync()
    {
        return await _context.Theatres
            .AsNoTracking()
            .OrderBy(t => t.TheatreId)
            .ToListAsync();
    }

    public async Task<Theatre?> GetByIdAsync(int id)
    {
        return await _context.Theatres
            .FirstOrDefaultAsync(t => t.TheatreId == id);
    }

    public async Task<Theatre> UpdateAsync(Theatre theatre)
    {
        _context.Theatres.Update(theatre);
        await _context.SaveChangesAsync();

        return theatre;
    }

    public async Task<bool> DeleteAsync(Theatre theatre)
    {
        _context.Theatres.Remove(theatre);

        await _context.SaveChangesAsync();

        return true;
    }
}
