using StudentManagement.Api.Models;

namespace StudentManagement.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(int id);

        Task<User> CreateAsync(User user);

        Task<bool> DeleteAsync(int id);

        Task<bool> EmailExistsAsync(string email);
    }
}