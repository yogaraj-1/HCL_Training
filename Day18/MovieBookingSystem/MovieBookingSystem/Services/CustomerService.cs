
using MovieBookingSystem.Models;
using MovieBookingSystem.Repository;

namespace MovieBookingSystem.Services;

public class CustomerService
{
    private readonly CustomerRepository _repository;

    public CustomerService(CustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.Name))
        {
            throw new ArgumentException(
                "Customer name is required.");
        }

        if (string.IsNullOrWhiteSpace(customer.Email) ||
            !customer.Email.Contains('@'))
        {
            throw new ArgumentException(
                "A valid customer email is required.");
        }

        if (string.IsNullOrWhiteSpace(customer.Phone) ||
            customer.Phone.Length < 10)
        {
            throw new ArgumentException(
                "Phone must have at least 10 characters.");
        }

        await _repository.AddCustomerAsync(customer);
    }


    public Task<List<Customer>> GetAllCustomersAsync()
    {
        return _repository.GetAllCustomersAsync();
    }

}
