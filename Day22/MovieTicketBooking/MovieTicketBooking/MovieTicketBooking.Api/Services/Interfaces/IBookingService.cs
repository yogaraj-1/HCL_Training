using MovieTicketBooking.Api.DTOs.Bookings;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface IBookingService
{
    Task<BookingResponse> CreateBookingAsync(
        CreateBookingRequest request,
        string userId);

    Task CancelBookingAsync(
        int bookingId,
        string userId,
        bool isAdmin);

    Task<IEnumerable<BookingResponse>> GetMyBookingsAsync(
        string userId);
}