using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Repositories.Interfaces;

public interface IBookingRepository
{
    Task<Show?> GetShowByIdAsync(int showId);

    Task<List<ShowSeat>> GetShowSeatsAsync(
        int showId,
        List<int> seatIds);

    Task<Booking> CreateBookingAsync(
        Booking booking,
        List<BookingSeat> bookingSeats,
        List<ShowSeat> showSeats);
}