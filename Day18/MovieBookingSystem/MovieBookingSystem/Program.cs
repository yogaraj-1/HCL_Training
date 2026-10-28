
using Microsoft.Data.SqlClient;
using MovieBookingSystem.Models;
using MovieBookingSystem.Repository;
using MovieBookingSystem.Services;

string connectionString =
    @"Server=.\SQLEXPRESS;Database=MovieBookingDB;Trusted_Connection=True;TrustServerCertificate=True;";

var repository = new MovieRepository(connectionString);
var service = new MovieService(repository);

try
{
    var movie = new Movie
    {
        Title = "Interstellar",
        Genre = "Sci-Fi",
        DurationMinutes = 169,
        TicketPrice = 250.00m,
        AvailableSeats = 120
    };

    await service.AddMovieAsync(movie);

    Console.WriteLine("Movie added successfully!");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Validation error: {ex.Message}");
}
catch (SqlException ex)
{
    Console.WriteLine($"Database error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
