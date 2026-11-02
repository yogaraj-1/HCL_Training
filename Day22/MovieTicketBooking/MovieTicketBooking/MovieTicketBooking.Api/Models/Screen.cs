
namespace MovieTicketBooking.Api.Models;

public class Screen
{
    public int ScreenId { get; set; }

    // Foreign key
    public int TheatreId { get; set; }

    public string ScreenName { get; set; } = string.Empty;

    // Navigation property
    public Theatre Theatre { get; set; } = null!;

    // One screen can have multiple physical seats
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();

    // One screen can host multiple shows
    public ICollection<Show> Shows { get; set; } = new List<Show>();
}
