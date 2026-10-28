
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


    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        const string query = """
        SELECT CustomerId, Name, Email, Phone
        FROM Customers
        ORDER BY CustomerId
        """;

        var customers = new List<Customer>();

        await using var connection =
            new SqlConnection(_connectionString);

        await using var command =
            new SqlCommand(query, connection);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            customers.Add(new Customer
            {
                CustomerId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Phone = reader.GetString(3)
            });
        }

        return customers;
    }


    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        const string query = """
        SELECT CustomerId, Name, Email, Phone
        FROM Customers
        WHERE CustomerId = @CustomerId
        """;

        await using var connection =
            new SqlConnection(_connectionString);

        await using var command =
            new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@CustomerId", id);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new Customer
            {
                CustomerId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Phone = reader.GetString(3)
            };
        }

        return null;
    }

}
