
using MovieBookingSystem.Repository;

namespace MovieBookingSystem.Services;

public class BookingService
{
    private readonly BookingRepository _repository;

    public BookingService(BookingRepository repository)
    {
        _repository = repository;
    }

    public async Task BookTicketsAsync(
        int customerId,
        int movieId,
        int seats)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException(
                "Customer ID must be greater than zero.");
        }

        if (movieId <= 0)
        {
            throw new ArgumentException(
                "Movie ID must be greater than zero.");
        }

        if (seats <= 0)
        {
            throw new ArgumentException(
                "Number of seats must be greater than zero.");
        }

        await _repository.BookTicketsAsync(
            customerId, movieId, seats);
    }
}
