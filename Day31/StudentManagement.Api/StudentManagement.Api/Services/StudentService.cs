using StudentManagement.Api.DTOs;
using StudentManagement.Api.Models;
using StudentManagement.Api.Repositories;

namespace StudentManagement.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;

        public StudentService(
    IStudentRepository studentRepository,
    IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }

        public async Task<StudentResponseDto?> GetByIdAsync(int id)
        {
            var student =
                await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return null;
            }

            return MapToDto(student);
        }

        public async Task<StudentResponseDto?> GetByUserIdAsync(
            int userId)
        {
            var student =
                await _studentRepository.GetByUserIdAsync(userId);

            if (student == null)
            {
                return null;
            }

            return MapToDto(student);
        }

        public async Task<List<StudentResponseDto>> GetAllAsync()
        {
            var students =
                await _studentRepository.GetAllAsync();

            return students
                .Select(MapToDto)
                .ToList();
        }

        public async Task<StudentResponseDto?> UpdateAsync(
            int id,
            UpdateStudentDto dto)
        {
            var student =
                await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return null;
            }

            student.User.Name = dto.Name;
            student.Department = dto.Department;
            student.Year = dto.Year;
            student.Phone = dto.Phone;

            await _studentRepository.UpdateAsync(student);

            return MapToDto(student);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student =
                await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return false;
            }

            await _studentRepository.DeleteAsync(id);

            await _userRepository.DeleteAsync(student.UserId);

            return true;
        }
        private static StudentResponseDto MapToDto(
            Student student)
        {
            return new StudentResponseDto
            {
                Id = student.Id,
                UserId = student.UserId,
                Name = student.User.Name,
                Email = student.User.Email,
                RegisterNumber = student.RegisterNumber,
                Department = student.Department,
                Year = student.Year,
                Phone = student.Phone,
                Role = student.User.Role
            };
        }
    }
}