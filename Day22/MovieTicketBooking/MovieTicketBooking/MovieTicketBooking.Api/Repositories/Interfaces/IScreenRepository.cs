
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Repositories.Interfaces;

public interface IScreenRepository
{
    Task<Screen> CreateAsync(Screen screen);

    Task<List<Screen>> GetAllAsync(int? theatreId = null);

    Task<Screen?> GetByIdAsync(int id);

    Task<List<Screen>> GetByTheatreIdAsync(int theatreId);

    Task<bool> TheatreExistsAsync(int theatreId);

    Task<Screen> UpdateAsync(Screen screen);

    Task<bool> DeleteAsync(Screen screen);
}
