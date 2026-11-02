
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Repositories.Interfaces;

public interface IShowRepository
{
    Task<bool> MovieExistsAsync(int movieId);

    Task<bool> ScreenExistsAsync(int screenId);

    Task<List<Seat>> GetSeatsByScreenIdAsync(int screenId);

    Task<bool> HasOverlappingShowAsync(
        int screenId,
        DateTime startTime,
        DateTime endTime,
        int? excludeShowId = null);

    Task<Show> CreateWithSeatsAsync(
        Show show,
        List<ShowSeat> showSeats);

    Task<List<Show>> GetAllAsync(
        int? movieId,
        int? theatreId,
        DateTime? date);

    Task<Show?> GetByIdAsync(int id);

    Task<List<Show>> GetByMovieIdAsync(int movieId);

    Task<bool> MovieExistsForFilterAsync(int movieId);

    Task<List<Show>> GetByTheatreIdAsync(int theatreId);

    Task<bool> TheatreExistsAsync(int theatreId);

    Task<bool> UpdateAsync(Show show);

    Task<bool> HasBookingsAsync(int showId);

    Task<bool> DeleteAsync(Show show);
}
