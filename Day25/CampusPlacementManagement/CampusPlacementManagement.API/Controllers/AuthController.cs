using CampusPlacementManagement.Api.DTOs.Auth;
using CampusPlacementManagement.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampusPlacementManagement.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequestDto request)
        {
            try
            {
                var response = await _authService.RegisterAsync(
                    request,
                    "Student");

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequestDto request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new
                {
                    message = ex.Message
                });
            }
        }
    }
}