
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.Api.DTOs.Shows;

public class UpdateShowRequest
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    [Range(typeof(decimal), "0", "99999999.99")]
    public decimal TicketPrice { get; set; }
}
