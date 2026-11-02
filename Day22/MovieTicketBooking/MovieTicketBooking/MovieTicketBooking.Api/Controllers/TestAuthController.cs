using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieTicketBooking.Api.Controllers;

[ApiController]
[Route("api/test-auth")]
public class TestAuthController : ControllerBase
{
    // GET: api/test-auth/public
    [AllowAnonymous]
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok(new
        {
            message = "This endpoint is publicly accessible."
        });
    }

    // GET: api/test-auth/protected
    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok(new
        {
            message = "JWT authentication successful!",
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue("sub"),
            email = User.FindFirstValue(ClaimTypes.Email),
            username = User.Identity?.Name
        });
    }
}