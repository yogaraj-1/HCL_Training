using MovieTicketBooking.Api.DTOs.Bookings;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface IBookingService
{
    Task<BookingResponse> CreateBookingAsync(
        CreateBookingRequest request);
    Task<bool> CancelBookingAsync(int bookingId);
}