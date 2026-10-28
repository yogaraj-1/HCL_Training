
using MovieBookingSystem.Repository;
using MovieBookingSystem.Services;

string connectionString =
    @"Server=.\SQLEXPRESS;Database=MovieBookingDB;Trusted_Connection=True;TrustServerCertificate=True;";

var repository = new MovieRepository(connectionString);
var service = new MovieService(repository);

try
{
    var movies = await service.GetAllMoviesAsync();

    if (movies.Count == 0)
    {
        Console.WriteLine("No movies found.");
    }
    else
    {
        Console.WriteLine("\n=== MOVIE LIST ===");

        foreach (var movie in movies)
        {
            Console.WriteLine(
                $"{movie.MovieId} | {movie.Title} | " +
                $"{movie.Genre} | {movie.DurationMinutes} min | " +
                $"{movie.TicketPrice:N2} | " +
                $"Seats: {movie.AvailableSeats}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
