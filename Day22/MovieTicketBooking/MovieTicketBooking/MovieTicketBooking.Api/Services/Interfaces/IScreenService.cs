
using MovieTicketBooking.Api.DTOs.Screens;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface IScreenService
{
    Task<ScreenResponse?> CreateAsync(CreateScreenRequest request);

    Task<List<ScreenResponse>> GetAllAsync(int? theatreId = null);

    Task<ScreenResponse?> GetByIdAsync(int id);

    Task<List<ScreenResponse>?> GetByTheatreIdAsync(int theatreId);

    Task<ScreenResponse?> UpdateAsync(int id, UpdateScreenRequest request);

    Task<bool> DeleteAsync(int id);
}
