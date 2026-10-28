
using MovieBookingSystem.Repository;
using MovieBookingSystem.Services;

string connectionString =
    @"Server=.\SQLEXPRESS;Database=MovieBookingDB;Trusted_Connection=True;TrustServerCertificate=True;";

var repository = new MovieRepository(connectionString);
var service = new MovieService(repository);

try
{
    Console.Write("Enter movie ID: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid movie ID.");
        return;
    }

    var movie = await service.GetMovieByIdAsync(id);

    if (movie is null)
    {
        Console.WriteLine("Movie not found.");
    }
    else
    {
        Console.WriteLine("\n=== MOVIE DETAILS ===");
        Console.WriteLine($"ID       : {movie.MovieId}");
        Console.WriteLine($"Title    : {movie.Title}");
        Console.WriteLine($"Genre    : {movie.Genre}");
        Console.WriteLine($"Duration : {movie.DurationMinutes} minutes");
        Console.WriteLine($"Price    : {movie.TicketPrice:N2}");
        Console.WriteLine($"Seats    : {movie.AvailableSeats}");
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Validation error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
