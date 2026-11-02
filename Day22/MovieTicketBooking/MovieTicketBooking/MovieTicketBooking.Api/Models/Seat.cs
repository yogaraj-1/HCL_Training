
using MovieTicketBooking.MovieTicketBooking.Api.Enum;
using MovieTicketBooking.MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Models;

public class Seat
{
    public int SeatId { get; set; }

    // Foreign key
    public int ScreenId { get; set; }

    public string RowName { get; set; } = string.Empty;

    public int SeatNumber { get; set; }

    public SeatType SeatType { get; set; }

    // Navigation property
    public Screen Screen { get; set; } = null!;

    // Availability records across different shows
    public ICollection<ShowSeat> ShowSeats { get; set; }
        = new List<ShowSeat>();

    // Booking history
    public ICollection<BookingSeat> BookingSeats { get; set; }
        = new List<BookingSeat>();
}
