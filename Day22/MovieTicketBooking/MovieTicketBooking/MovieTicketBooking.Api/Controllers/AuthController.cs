using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Api.DTOs.Auth;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var response = await _service.RegisterAsync(request);

        return StatusCode(
            StatusCodes.Status201Created,
            response);
    }
}