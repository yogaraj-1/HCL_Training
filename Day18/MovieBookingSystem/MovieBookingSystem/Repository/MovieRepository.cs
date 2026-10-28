
using Microsoft.Data.SqlClient;
using MovieBookingSystem.Models;

namespace MovieBookingSystem.Repository;

public class MovieRepository
{
    private readonly string _connectionString;

    public MovieRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddMovieAsync(Movie movie)
    {
        const string query = """
            INSERT INTO Movies
                (Title, Genre, DurationMinutes, TicketPrice, AvailableSeats)
            VALUES
                (@Title, @Genre, @DurationMinutes, @TicketPrice, @AvailableSeats)
            """;

        await using var connection =
            new SqlConnection(_connectionString);

        await using var command =
            new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Title", movie.Title);
        command.Parameters.AddWithValue("@Genre", movie.Genre);
        command.Parameters.AddWithValue(
            "@DurationMinutes", movie.DurationMinutes);
        command.Parameters.AddWithValue("@TicketPrice", movie.TicketPrice);
        command.Parameters.AddWithValue(
            "@AvailableSeats", movie.AvailableSeats);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}
