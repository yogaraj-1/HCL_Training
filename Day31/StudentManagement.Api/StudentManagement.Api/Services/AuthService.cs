using Microsoft.AspNetCore.Identity;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Models;
using StudentManagement.Api.Repositories;

namespace StudentManagement.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(
            IUserRepository userRepository,
            IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
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
    }
}