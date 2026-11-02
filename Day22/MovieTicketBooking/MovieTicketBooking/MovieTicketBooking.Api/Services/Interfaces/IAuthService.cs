using MovieTicketBooking.Api.DTOs.Auth;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
}