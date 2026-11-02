
using MovieTicketBooking.Api.DTOs.Theatres;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface ITheatreService
{
    Task<TheatreResponse> CreateAsync(CreateTheatreRequest request);

    Task<List<TheatreResponse>> GetAllAsync();

    Task<TheatreResponse?> GetByIdAsync(int id);

    Task<TheatreResponse?> UpdateAsync(
        int id,
        UpdateTheatreRequest request);

    Task<bool> DeleteAsync(int id);
}
