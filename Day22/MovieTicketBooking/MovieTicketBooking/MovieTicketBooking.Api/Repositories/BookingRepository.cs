using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.Data;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;

namespace MovieTicketBooking.Api.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Show?> GetShowByIdAsync(int showId)
    {
        return await _context.Shows
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ShowId == showId);
    }

    public async Task<List<ShowSeat>> GetShowSeatsAsync(
        int showId,
        List<int> seatIds)
    {
        return await _context.ShowSeats
            .Include(ss => ss.Seat)
            .Where(ss =>
                ss.ShowId == showId &&
                seatIds.Contains(ss.SeatId))
            .ToListAsync();
    }

    public async Task<Booking> CreateBookingAsync(
        Booking booking,
        List<BookingSeat> bookingSeats,
        List<ShowSeat> showSeats)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            _context.Bookings.Add(booking);

            await _context.SaveChangesAsync();

            foreach (var bookingSeat in bookingSeats)
            {
                bookingSeat.BookingId = booking.BookingId;
            }

            _context.BookingSeats.AddRange(bookingSeats);

            foreach (var showSeat in showSeats)
            {
                showSeat.Status = ShowSeatStatus.Booked;
            }

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return booking;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}