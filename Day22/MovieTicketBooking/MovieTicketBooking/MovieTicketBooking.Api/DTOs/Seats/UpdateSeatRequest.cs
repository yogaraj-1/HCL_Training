
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.Api.DTOs.Seats;

public class UpdateSeatRequest
{
    [Range(1, int.MaxValue)]
    public int ScreenId { get; set; }

    [Required]
    [StringLength(5, MinimumLength = 1)]
    [RegularExpression(@"^[A-Za-z]+$")]
    public string RowName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int SeatNumber { get; set; }

    [Required]
    [RegularExpression("^(Regular|Premium)$")]
    public string SeatType { get; set; } = string.Empty;
}
