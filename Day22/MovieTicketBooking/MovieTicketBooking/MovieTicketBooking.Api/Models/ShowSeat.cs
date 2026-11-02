
namespace MovieTicketBooking.Api.Models;

public class ShowSeat
{
    public int ShowSeatId { get; set; }

    // Foreign keys
    public int ShowId { get; set; }

    public int SeatId { get; set; }

    public ShowSeatStatus Status { get; set; }

    // Navigation properties
    public Show Show { get; set; } = null!;

    public Seat Seat { get; set; } = null!;
}
