
using Microsoft.Data.SqlClient;

namespace MovieBookingSystem.Repository;

public class BookingRepository
{
    private readonly string _connectionString;

    public BookingRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task BookTicketsAsync(
        int customerId,
        int movieId,
        int seats)
    {
        await using var connection =
            new SqlConnection(_connectionString);

        await connection.OpenAsync();

        await using var transaction =
            (SqlTransaction)await connection.BeginTransactionAsync();

        try
        {
            // Step 1: Verify the customer exists.
            const string customerSql = """
                SELECT COUNT(*)
                FROM Customers
                WHERE CustomerId = @CustomerId
                """;

            await using (var command = new SqlCommand(
                customerSql, connection, transaction))
            {
                command.Parameters.Add("@CustomerId",
                    System.Data.SqlDbType.Int).Value = customerId;

                int count = (int)(await command.ExecuteScalarAsync()
                    ?? 0);

                if (count == 0)
                {
                    throw new InvalidOperationException(
                        "Customer not found.");
                }
            }

            // Step 2: Reserve seats only if enough are available.
            const string seatSql = """
                UPDATE Movies
                SET AvailableSeats = AvailableSeats - @Seats
                WHERE MovieId = @MovieId
                  AND AvailableSeats >= @Seats
                """;

            await using (var command = new SqlCommand(
                seatSql, connection, transaction))
            {
                command.Parameters.Add("@MovieId",
                    System.Data.SqlDbType.Int).Value = movieId;

                command.Parameters.Add("@Seats",
                    System.Data.SqlDbType.Int).Value = seats;

                int rows = await command.ExecuteNonQueryAsync();

                if (rows == 0)
                {
                    const string movieSql = """
                        SELECT COUNT(*)
                        FROM Movies
                        WHERE MovieId = @MovieId
                        """;

                    await using var checkMovie = new SqlCommand(
                        movieSql, connection, transaction);

                    checkMovie.Parameters.Add("@MovieId",
                        System.Data.SqlDbType.Int).Value = movieId;

                    int movieCount =
                        (int)(await checkMovie.ExecuteScalarAsync() ?? 0);

                    if (movieCount == 0)
                    {
                        throw new InvalidOperationException(
                            "Movie not found.");
                    }

                    throw new InvalidOperationException(
                        "Not enough seats available.");
                }
            }

            // Step 3: Save the booking and calculate its total.
            const string bookingSql = """
                INSERT INTO Bookings
                    (CustomerId, MovieId, Seats, TotalAmount)
                SELECT
                    @CustomerId,
                    MovieId,
                    @Seats,
                    TicketPrice * @Seats
                FROM Movies
                WHERE MovieId = @MovieId
                """;

            await using (var command = new SqlCommand(
                bookingSql, connection, transaction))
            {
                command.Parameters.Add("@CustomerId",
                    System.Data.SqlDbType.Int).Value = customerId;

                command.Parameters.Add("@MovieId",
                    System.Data.SqlDbType.Int).Value = movieId;

                command.Parameters.Add("@Seats",
                    System.Data.SqlDbType.Int).Value = seats;

                int rows = await command.ExecuteNonQueryAsync();

                if (rows != 1)
                {
                    throw new InvalidOperationException(
                        "Booking could not be saved.");
                }
            }

            // Step 4: Record the audit entry.
            const string auditSql = """
                INSERT INTO BookingAudit
                    (CustomerId, MovieId, ActionName)
                VALUES
                    (@CustomerId, @MovieId, @ActionName)
                """;

            await using (var command = new SqlCommand(
                auditSql, connection, transaction))
            {
                command.Parameters.Add("@CustomerId",
                    System.Data.SqlDbType.Int).Value = customerId;

                command.Parameters.Add("@MovieId",
                    System.Data.SqlDbType.Int).Value = movieId;

                command.Parameters.Add("@ActionName",
                    System.Data.SqlDbType.NVarChar, 50).Value = "BOOKED";

                await command.ExecuteNonQueryAsync();
            }

            // Step 5: Confirm all changes together.
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
