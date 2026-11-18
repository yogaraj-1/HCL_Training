using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);

        Task<LoginResponseDto?> LoginAsync(LoginDto dto);
    }
}