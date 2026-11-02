using Microsoft.AspNetCore.Identity;

namespace MovieTicketBooking.Api.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}