
using MovieTicketBooking.Api.DTOs.Shows;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface IShowService
{
    Task<ShowResponse> CreateAsync(CreateShowRequest request);

    Task<List<ShowResponse>> GetAllAsync(
        int? movieId,
        int? theatreId,
        DateTime? date);

    Task<ShowResponse?> GetByIdAsync(int id);

    Task<List<ShowResponse>?> GetByMovieIdAsync(int movieId);

    Task<List<ShowResponse>?> GetByTheatreIdAsync(int theatreId);

    Task<ShowResponse?> UpdateAsync(
        int id,
        UpdateShowRequest request);

    Task<bool> DeleteAsync(int id);

    Task<List<ShowSeatResponse>?> GetSeatLayoutAsync(int showId);
    Task<List<ShowSeatResponse>?> GetAvailableSeatsAsync(int showId);
    Task<List<ShowSeatResponse>?> GetBookedSeatsAsync(int showId);
}
