
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Repositories.Interfaces;

public interface ITheatreRepository
{
    Task<Theatre> CreateAsync(Theatre theatre);

    Task<List<Theatre>> GetAllAsync();

    Task<Theatre?> GetByIdAsync(int id);

    Task<Theatre> UpdateAsync(Theatre theatre);

    Task<bool> DeleteAsync(Theatre theatre);
}
