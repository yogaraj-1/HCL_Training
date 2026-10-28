
using MovieBookingSystem.Models;
using MovieBookingSystem.Repository;

namespace MovieBookingSystem.Services;

public class MovieService
{
    private readonly MovieRepository _repository;

    public MovieService(MovieRepository repository)
    {
        _repository = repository;
    }

    public async Task AddMovieAsync(Movie movie)
    {
        if (string.IsNullOrWhiteSpace(movie.Title))
        {
            throw new ArgumentException("Movie title is required.");
        }

        if (string.IsNullOrWhiteSpace(movie.Genre))
        {
            throw new ArgumentException("Genre is required.");
        }

        if (movie.DurationMinutes <= 0)
        {
            throw new ArgumentException(
                "Duration must be greater than zero.");
        }

        if (movie.TicketPrice < 0)
        {
            throw new ArgumentException(
                "Ticket price cannot be negative.");
        }

        if (movie.AvailableSeats < 0)
        {
            throw new ArgumentException(
                "Available seats cannot be negative.");
        }

        await _repository.AddMovieAsync(movie);
    }

    public Task<List<Movie>> GetAllMoviesAsync()
    {
        return _repository.GetAllMoviesAsync();
    }

}
