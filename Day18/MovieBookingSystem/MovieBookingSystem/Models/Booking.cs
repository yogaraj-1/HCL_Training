
namespace MovieBookingSystem.Models;

public class Booking
{
    public int BookingId { get; set; }

    public int CustomerId { get; set; }

    public int MovieId { get; set; }

    public int Seats { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime BookedAt { get; set; }
}
