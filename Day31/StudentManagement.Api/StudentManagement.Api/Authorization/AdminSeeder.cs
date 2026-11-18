using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Authorization
{
    public static class AdminSeeder
    {
        public static async Task SeedAsync(
            ApplicationDbContext context)
        {
            var adminExists = await context.Users
                .AnyAsync(u => u.Role == "Admin");

            if (adminExists)
            {
                return;
            }

            var admin = new User
            {
                Name = "System Admin",
                Email = "admin@gmail.com",
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };

            var passwordHasher = new PasswordHasher<User>();

            admin.PasswordHash =
                passwordHasher.HashPassword(
                    admin,
                    "Admin@123");

            context.Users.Add(admin);

            await context.SaveChangesAsync();
        }
    }
}