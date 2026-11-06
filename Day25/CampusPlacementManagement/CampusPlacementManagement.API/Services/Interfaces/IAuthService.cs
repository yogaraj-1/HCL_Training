using CampusPlacementManagement.Api.DTOs.Auth;

namespace CampusPlacementManagement.Api.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(
            RegisterRequestDto request,
            string role);

        Task<AuthResponseDto> LoginAsync(
            LoginRequestDto request);
    }
}