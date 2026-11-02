
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.Api.DTOs.Theatres;

public class UpdateTheatreRequest
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Location { get; set; } = string.Empty;
}
