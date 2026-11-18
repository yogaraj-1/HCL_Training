using StudentManagement.Api.Models;

namespace StudentManagement.Api.Repositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(int id);

        Task<Student?> GetByUserIdAsync(int userId);

        Task<List<Student>> GetAllAsync();

        Task<Student> CreateAsync(Student student);

        Task<Student> UpdateAsync(Student student);

        Task<bool> DeleteAsync(int id);

        Task<bool> RegisterNumberExistsAsync(string registerNumber);
    }
}