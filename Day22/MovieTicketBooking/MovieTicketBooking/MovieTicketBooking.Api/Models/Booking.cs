
namespace MovieTicketBooking.Api.Models;

public class Booking
{
    public int BookingId { get; set; }

    public string BookingReference { get; set; } = string.Empty;

    // Foreign key
    public int ShowId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public string CustomerPhone { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; }

    public decimal TotalAmount { get; set; }

    public BookingStatus Status { get; set; }

    // Navigation properties
    public Show Show { get; set; } = null!;

    public ICollection<BookingSeat> BookingSeats { get; set; }
        = new List<BookingSeat>();
}
