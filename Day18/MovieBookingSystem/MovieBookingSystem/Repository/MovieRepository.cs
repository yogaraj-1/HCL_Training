
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

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        const string query = """
        SELECT MovieId, Title, Genre, DurationMinutes,
               TicketPrice, AvailableSeats
        FROM Movies
        ORDER BY MovieId
        """;

        var movies = new List<Movie>();

        await using var connection =
            new SqlConnection(_connectionString);

        await using var command =
            new SqlCommand(query, connection);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            movies.Add(new Movie
            {
                MovieId = reader.GetInt32(0),
                Title = reader.GetString(1),
                Genre = reader.GetString(2),
                DurationMinutes = reader.GetInt32(3),
                TicketPrice = reader.GetDecimal(4),
                AvailableSeats = reader.GetInt32(5)
            });
        }

        return movies;
    }


    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        const string query = """
        SELECT MovieId, Title, Genre, DurationMinutes,
               TicketPrice, AvailableSeats
        FROM Movies
        WHERE MovieId = @MovieId
        """;

        await using var connection =
            new SqlConnection(_connectionString);

        await using var command =
            new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@MovieId", id);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new Movie
            {
                MovieId = reader.GetInt32(0),
                Title = reader.GetString(1),
                Genre = reader.GetString(2),
                DurationMinutes = reader.GetInt32(3),
                TicketPrice = reader.GetDecimal(4),
                AvailableSeats = reader.GetInt32(5)
            };
        }

        return null;
    }

}
