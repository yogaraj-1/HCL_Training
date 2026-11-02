using Microsoft.AspNetCore.Identity;
using MovieTicketBooking.Api.DTOs.Auth;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        // 1. Find user using email.
        var user = await _userManager.FindByEmailAsync(
            request.Email.Trim());

        // Use the same error for unknown email and incorrect password.
        if (user == null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // 2. Verify password using ASP.NET Core Identity.
        var isPasswordValid = await _userManager.CheckPasswordAsync(
            user,
            request.Password);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        // 3. Get the user's assigned roles.
        var roles = await _userManager.GetRolesAsync(user);

        // 4. Generate JWT.
        var token = await _jwtTokenService.GenerateTokenAsync(user);

        // 5. Return login response.
        return new LoginResponse
        {
            UserId = user.Id,
            FullName = user.FullName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty,
            Roles = roles.ToList(),
            AccessToken = token.AccessToken,
            TokenType = token.TokenType,
            ExpiresAt = token.ExpiresAt
        };
    }

    public async Task<RegisterResponse> RegisterAsync(
        RegisterRequest request)
    {
        // Check whether email already exists.
        var existingEmail = await _userManager.FindByEmailAsync(
            request.Email.Trim());

        if (existingEmail != null)
        {
            throw new InvalidOperationException(
                "Email is already registered.");
        }

        // Check whether username already exists.
        var existingUser = await _userManager.FindByNameAsync(
            request.UserName.Trim());

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "Username is already taken.");
        }

        // Ensure the default User role exists.
        const string userRole = "User";

        if (!await _roleManager.RoleExistsAsync(userRole))
        {
            var roleResult = await _roleManager.CreateAsync(
                new IdentityRole(userRole));

            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    "Unable to create the default User role: " +
                    string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }
        }

        // Create ApplicationUser.
        var user = new ApplicationUser
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            UserName = request.UserName.Trim()
        };

        // Identity securely hashes the password.
        var result = await _userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
        {
            throw new ArgumentException(
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Assign User role automatically.
        var roleAssignment = await _userManager.AddToRoleAsync(
            user,
            userRole);

        if (!roleAssignment.Succeeded)
        {
            // Remove the partially registered account if role assignment fails.
            await _userManager.DeleteAsync(user);

            throw new InvalidOperationException(
                "Unable to assign the User role: " +
                string.Join(", ", roleAssignment.Errors.Select(e => e.Description)));
        }

        return new RegisterResponse
        {
            UserId = user.Id,
            FullName = user.FullName!,
            Email = user.Email!,
            UserName = user.UserName!,
            Role = userRole,
            Message = "Registration successful."
        };
    }
}