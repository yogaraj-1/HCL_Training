
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.Data;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;

namespace MovieTicketBooking.Api.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly ApplicationDbContext _context;

    public SeatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Seat> CreateAsync(Seat seat)
    {
        await _context.Seats.AddAsync(seat);
        await _context.SaveChangesAsync();

        return seat;
    }

    public async Task<List<Seat>> CreateBulkAsync(List<Seat> seats)
    {
        await _context.Seats.AddRangeAsync(seats);
        await _context.SaveChangesAsync();

        return seats;
    }

    public async Task<List<Seat>> GetByScreenIdAsync(int screenId)
    {
        return await _context.Seats
            .AsNoTracking()
            .Where(s => s.ScreenId == screenId)
            .OrderBy(s => s.RowName)
            .ThenBy(s => s.SeatNumber)
            .ToListAsync();
    }

    public async Task<Seat?> GetByIdAsync(int id)
    {
        return await _context.Seats
            .FirstOrDefaultAsync(s => s.SeatId == id);
    }

    public async Task<bool> ScreenExistsAsync(int screenId)
    {
        return await _context.Screens
            .AnyAsync(s => s.ScreenId == screenId);
    }

    public async Task<bool> SeatExistsAsync(
        int screenId,
        string rowName,
        int seatNumber,
        int? excludeSeatId = null)
    {
        var query = _context.Seats.Where(s =>
            s.ScreenId == screenId &&
            s.RowName == rowName &&
            s.SeatNumber == seatNumber);

        if (excludeSeatId.HasValue)
        {
            query = query.Where(
                s => s.SeatId != excludeSeatId.Value);
        }

        return await query.AnyAsync();
    }

    public async Task<Seat> UpdateAsync(Seat seat)
    {
        _context.Seats.Update(seat);
        await _context.SaveChangesAsync();

        return seat;
    }

    public async Task<bool> DeleteAsync(Seat seat)
    {
        _context.Seats.Remove(seat);
        await _context.SaveChangesAsync();

        return true;
    }
}
