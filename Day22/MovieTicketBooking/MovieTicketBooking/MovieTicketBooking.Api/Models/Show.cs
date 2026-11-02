
using MovieTicketBooking.MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Models;

public class Show
{
    public int ShowId { get; set; }

    // Foreign keys
    public int MovieId { get; set; }

    public int ScreenId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal TicketPrice { get; set; }

    // Navigation properties
    public Movie Movie { get; set; } = null!;

    public Screen Screen { get; set; } = null!;

    // One show has multiple seat availability records
    public ICollection<ShowSeat> ShowSeats { get; set; }
        = new List<ShowSeat>();

    // One show can have multiple bookings
    public ICollection<Booking> Bookings { get; set; }
        = new List<Booking>();
}
