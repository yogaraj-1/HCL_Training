using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Services
{
    public interface IStudentService
    {
        Task<StudentResponseDto?> GetByIdAsync(int id);

        Task<StudentResponseDto?> GetByUserIdAsync(int userId);

        Task<List<StudentResponseDto>> GetAllAsync();

        Task<StudentResponseDto?> UpdateAsync(
            int id,
            UpdateStudentDto dto);

        Task<bool> DeleteAsync(int id);
    }
}