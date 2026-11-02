
using MovieTicketBooking.Api.DTOs.Movies;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface IMovieService
{
    Task<MovieResponse> CreateAsync(CreateMovieRequest request);

    Task<MovieResponse?> GetByIdAsync(int id);

    Task<PagedResponse<MovieResponse>> GetAllAsync(
        MovieQueryParameters parameters);

    Task<MovieResponse?> UpdateAsync(
        int id,
        UpdateMovieRequest request);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<MovieResponse>> SearchByTitleAsync(string title);
}
