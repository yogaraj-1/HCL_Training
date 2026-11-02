
namespace MovieTicketBooking.Api.Models;

public class BookingSeat
{
    public int BookingSeatId { get; set; }

    // Foreign keys
    public int BookingId { get; set; }

    public int SeatId { get; set; }

    public decimal Price { get; set; }

    // Navigation properties
    public Booking Booking { get; set; } = null!;

    public Seat Seat { get; set; } = null!;
}
