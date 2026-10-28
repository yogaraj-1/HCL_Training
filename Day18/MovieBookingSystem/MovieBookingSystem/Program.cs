
using MovieBookingSystem.Repository;
using MovieBookingSystem.Services;

string connectionString =
    @"Server=.\SQLEXPRESS;Database=MovieBookingDB;Trusted_Connection=True;TrustServerCertificate=True;";

var repository = new CustomerRepository(connectionString);
var service = new CustomerService(repository);

try
{
    Console.Write("Enter customer ID: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid customer ID.");
        return;
    }

    var customer = await service.GetCustomerByIdAsync(id);

    if (customer is null)
    {
        Console.WriteLine("Customer not found.");
    }
    else
    {
        Console.WriteLine("\n=== CUSTOMER DETAILS ===");
        Console.WriteLine($"ID    : {customer.CustomerId}");
        Console.WriteLine($"Name  : {customer.Name}");
        Console.WriteLine($"Email : {customer.Email}");
        Console.WriteLine($"Phone : {customer.Phone}");
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
