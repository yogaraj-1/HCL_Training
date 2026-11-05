using MovieTicketBooking.Api.DTOs.Bookings;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;
using MovieTicketBooking.Api.Services.Interfaces;
using MovieTicketBooking.Api.Exceptions;

namespace MovieTicketBooking.Api.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;

    public BookingService(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookingResponse> CreateBookingAsync(
     CreateBookingRequest request,
     string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new UnauthorizedAccessException("User is not authenticated.");
        // 1. Validate selected seat IDs.
        if (request.SeatIds == null || request.SeatIds.Count == 0)
            throw new ArgumentException("Please select at least one seat.");

        if (request.SeatIds.Any(id => id <= 0))
            throw new ArgumentException("Seat IDs must be greater than zero.");

        if (request.SeatIds.Distinct().Count() != request.SeatIds.Count)
            throw new ArgumentException("Duplicate seat IDs are not allowed.");

        // 2. Check whether the show exists.
        var show = await _repository.GetShowByIdAsync(request.ShowId);

        if (show == null)
            throw new KeyNotFoundException("Show not found.");

        // 3. Get selected seats belonging to this show.
        var showSeats = await _repository.GetShowSeatsAsync(
            request.ShowId,
            request.SeatIds);

        // Ensure every requested seat was found.
        if (showSeats.Count != request.SeatIds.Count)
        {
            throw new ArgumentException(
                "One or more seats do not belong to this show.");
        }

        // 4. Verify all seats are available.
        if (showSeats.Any(ss => ss.Status != ShowSeatStatus.Available))
        {
            throw new InvalidOperationException(
                "One or more selected seats are already booked.");
        }

        // 5. Calculate the total on the server.
        decimal totalAmount = show.TicketPrice * showSeats.Count;

        // 6. Create booking.
        var booking = new Booking
        {
            BookingReference = $"MTB-{Guid.NewGuid():N}"[..16].ToUpperInvariant(),
            ShowId = show.ShowId,
            CustomerName = request.CustomerName.Trim(),
            CustomerEmail = request.CustomerEmail.Trim(),
            CustomerPhone = request.CustomerPhone.Trim(),
            BookingDate = DateTime.UtcNow,
            TotalAmount = totalAmount,
            Status = BookingStatus.Confirmed,
            UserId = userId
        };

        // 7. Create booking seat records.
        var bookingSeats = showSeats.Select(ss => new BookingSeat
        {
            SeatId = ss.SeatId,
            Price = show.TicketPrice
        }).ToList();

        // 8. Save everything through the transaction.
        var createdBooking = await _repository.CreateBookingAsync(
            booking,
            bookingSeats,
            showSeats);

        // 9. Return response.
        return new BookingResponse
        {
            BookingId = createdBooking.BookingId,
            BookingReference = createdBooking.BookingReference,
            ShowId = createdBooking.ShowId,
            CustomerName = createdBooking.CustomerName,
            CustomerEmail = createdBooking.CustomerEmail,
            CustomerPhone = createdBooking.CustomerPhone,
            BookingDate = createdBooking.BookingDate,
            TotalAmount = createdBooking.TotalAmount,
            Status = createdBooking.Status.ToString(),

            Seats = showSeats.Select(ss => new BookingSeatResponse
            {
                SeatId = ss.SeatId,
                SeatLabel = $"{ss.Seat!.RowName}{ss.Seat.SeatNumber}",
                Price = show.TicketPrice
            }).ToList()
        };
    }
    public async Task CancelBookingAsync(Booking booking)
    {
        // Only confirmed bookings can be cancelled.
        if (booking.Status != BookingStatus.Confirmed)
        {
            throw new InvalidOperationException(
                "Only confirmed bookings can be cancelled.");
        }

        // Cancel booking and release seats.
        await _repository.CancelBookingAsync(booking);
    }

    public async Task<IEnumerable<BookingResponse>> GetMyBookingsAsync(
    string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new UnauthorizedAccessException("User is not authenticated.");

        var bookings = await _repository.GetBookingsByUserIdAsync(userId);

        return bookings.Select(b => new BookingResponse
        {
            BookingId = b.BookingId,
            BookingReference = b.BookingReference,
            ShowId = b.ShowId,
            CustomerName = b.CustomerName,
            CustomerEmail = b.CustomerEmail,
            CustomerPhone = b.CustomerPhone,
            BookingDate = b.BookingDate,
            TotalAmount = b.TotalAmount,
            Status = b.Status.ToString()
        });
    }
    public async Task<Booking?> GetBookingForAuthorizationAsync(int bookingId)
    {
        return await _repository.GetBookingByIdAsync(bookingId);
    }


}