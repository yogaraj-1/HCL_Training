
using Microsoft.Data.SqlClient;
using MovieBookingSystem.Models;

namespace MovieBookingSystem.Repository;

public class CustomerRepository
{
    private readonly string _connectionString;

    public CustomerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        const string query = """
            INSERT INTO Customers (Name, Email, Phone)
            VALUES (@Name, @Email, @Phone)
            """;

        await using var connection =
            new SqlConnection(_connectionString);

        await using var command =
            new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", customer.Name);
        command.Parameters.AddWithValue("@Email", customer.Email);
        command.Parameters.AddWithValue("@Phone", customer.Phone);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }
}
