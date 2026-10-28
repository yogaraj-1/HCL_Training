
using MovieBookingSystem.Repository;
using MovieBookingSystem.Services;

string connectionString =
    @"Server=.\SQLEXPRESS;Database=MovieBookingDB;Trusted_Connection=True;TrustServerCertificate=True;";

var repository = new CustomerRepository(connectionString);
var service = new CustomerService(repository);

try
{
    var customers = await service.GetAllCustomersAsync();

    if (customers.Count == 0)
    {
        Console.WriteLine("No customers found.");
    }
    else
    {
        Console.WriteLine("\n=== CUSTOMER LIST ===");

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"{customer.CustomerId} | " +
                $"{customer.Name} | " +
                $"{customer.Email} | " +
                $"{customer.Phone}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
