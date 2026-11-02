using MovieTicketBooking.Api.DTOs.Auth;
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface IJwtTokenService
{
    Task<TokenResponse> GenerateTokenAsync(ApplicationUser user);
}