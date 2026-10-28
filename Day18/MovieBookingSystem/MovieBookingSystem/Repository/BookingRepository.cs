
using Microsoft.EntityFrameworkCore;
using MovieBookingSystem.Data;
using MovieBookingSystem.Models;

namespace MovieBookingSystem.Repository;

public class BookingRepository
{
    private readonly MovieBookingDbContext _context;

    public BookingRepository(MovieBookingDbContext context)
    {
        _context = context;
    }

    public async Task BookTicketsAsync(
        int customerId,
        int movieId,
        int seats)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            bool customerExists = await _context.Customers
                .AnyAsync(c => c.CustomerId == customerId);

            if (!customerExists)
                throw new InvalidOperationException("Customer not found.");

            // Atomically decrease seats only if enough are available.
            int rowsUpdated = await _context.Movies
                .Where(m =>
                    m.MovieId == movieId &&
                    m.AvailableSeats >= seats)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(
                        m => m.AvailableSeats,
                        m => m.AvailableSeats - seats));

            if (rowsUpdated == 0)
            {
                bool movieExists = await _context.Movies
                    .AnyAsync(m => m.MovieId == movieId);

                if (!movieExists)
                    throw new InvalidOperationException("Movie not found.");

                throw new InvalidOperationException(
                    "Not enough seats available.");
            }

            var movie = await _context.Movies
                .AsNoTracking()
                .FirstAsync(m => m.MovieId == movieId);

            var booking = new Booking
            {
                CustomerId = customerId,
                MovieId = movieId,
                Seats = seats,
                TotalAmount = movie.TicketPrice * seats,
                BookedAt = DateTime.Now
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Keep the audit insert inside the same transaction.
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                INSERT INTO BookingAudit
                    (CustomerId, MovieId, ActionName)
                VALUES
                    ({customerId}, {movieId}, {"BOOKED"})");

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
