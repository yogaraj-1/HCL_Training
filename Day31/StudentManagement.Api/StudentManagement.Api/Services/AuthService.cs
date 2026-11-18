using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Models;
using StudentManagement.Api.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagement.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IConfiguration _configuration;

        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(
            IUserRepository userRepository,
            IStudentRepository studentRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _configuration = configuration;

            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                return false;
            }

            if (await _studentRepository
                .RegisterNumberExistsAsync(dto.RegisterNumber))
            {
                return false;
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = "Student",
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash =
                _passwordHasher.HashPassword(user, dto.Password);

            var createdUser =
                await _userRepository.CreateAsync(user);

            var student = new Student
            {
                UserId = createdUser.Id,
                RegisterNumber = dto.RegisterNumber,
                Department = dto.Department,
                Year = dto.Year,
                Phone = dto.Phone
            };

            await _studentRepository.CreateAsync(student);

            return true;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
            {
                return null;
            }

            var passwordResult =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    dto.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Token = token,
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = jwtSettings["Key"]!;
            var issuer = jwtSettings["Issuer"]!;
            var audience = jwtSettings["Audience"]!;

            var expiryMinutes =
                int.Parse(jwtSettings["ExpiryMinutes"]!);

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Name),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role,
                    user.Role)
            };

            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key));

            var credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}