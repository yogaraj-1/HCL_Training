
using MovieTicketBooking.Api.DTOs.Movies;
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<Movie> CreateAsync(Movie movie);

    Task<Movie?> GetByIdAsync(int id);

    Task<PagedResponse<Movie>> GetAllAsync(
        MovieQueryParameters parameters);

    Task<Movie> UpdateAsync(Movie movie);

    Task<bool> DeleteAsync(Movie movie);

    Task<IEnumerable<Movie>> SearchByTitleAsync(string title);
}
