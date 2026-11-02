using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MovieTicketBooking.Api.Configuration;
using MovieTicketBooking.Api.DTOs.Auth;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;

    public JwtTokenService(
        IOptions<JwtSettings> jwtOptions,
        UserManager<ApplicationUser> userManager)
    {
        _jwtSettings = jwtOptions.Value;
        _userManager = userManager;
    }

    public async Task<TokenResponse> GenerateTokenAsync(
        ApplicationUser user)
    {
        // 1. Get the user's actual roles from Identity.
        var roles = await _userManager.GetRolesAsync(user);

        // 2. Create user claims.
        var claims = new List<Claim>
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                user.Id),

            new Claim(
                JwtRegisteredClaimNames.Email,
                user.Email ?? string.Empty),

            new Claim(
                JwtRegisteredClaimNames.UniqueName,
                user.UserName ?? string.Empty),

            new Claim(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
        };

        // 3. Add role claims.
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // 4. Create signing key.
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key));

        // 5. Create signing credentials.
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // 6. Set expiration.
        var expiresAt = DateTime.UtcNow.AddMinutes(
            _jwtSettings.ExpiryMinutes);

        // 7. Generate JWT.
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        // 8. Serialize token.
        var accessToken = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return new TokenResponse
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            ExpiresAt = expiresAt
        };
    }
}