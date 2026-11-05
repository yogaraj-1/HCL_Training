using MovieTicketBooking.Api.DTOs.Bookings;
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface IBookingService
{
    Task<BookingResponse> CreateBookingAsync(
        CreateBookingRequest request,
        string userId);

    Task CancelBookingAsync(Booking booking);

    Task<IEnumerable<BookingResponse>> GetMyBookingsAsync(
        string userId);
    Task<Booking?> GetBookingForAuthorizationAsync(int bookingId);
}