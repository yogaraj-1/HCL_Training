
using Microsoft.Data.SqlClient;

string connectionString =
    @"Server=.\SQLEXPRESS;Database=MovieBookingDB;Trusted_Connection=True;TrustServerCertificate=True;";

try
{
    await using var connection = new SqlConnection(connectionString);

    await connection.OpenAsync();

    Console.WriteLine("Database connected successfully!");
}
catch (SqlException ex)
{
    Console.WriteLine($"Database connection failed: {ex.Message}");
}
