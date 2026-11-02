using Microsoft.AspNetCore.Identity;
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        var roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var userManager =
            serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // 1. Create default roles if they don't exist.
        string[] roles = { "User", "Admin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var roleResult = await roleManager.CreateAsync(
                    new IdentityRole(role));

                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to create role {role}: " +
                        string.Join(", ",
                            roleResult.Errors.Select(e => e.Description)));
                }
            }
        }

        // 2. Read Admin credentials from configuration.
        var email = configuration["AdminSeed:Email"];
        var userName = configuration["AdminSeed:UserName"];
        var fullName = configuration["AdminSeed:FullName"];
        var password = configuration["AdminSeed:Password"];

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(userName) ||
            string.IsNullOrWhiteSpace(fullName) ||
            string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                "AdminSeed configuration is incomplete. " +
                "Configure AdminSeed:Email, UserName, FullName and Password.");
        }

        // 3. Find Admin account by email.
        var admin = await userManager.FindByEmailAsync(email);

        // 4. Create Admin account if it doesn't exist.
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                FullName = fullName,
                Email = email,
                UserName = userName,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(
                admin,
                password);

            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(
                    "Failed to create Admin account: " +
                    string.Join(", ",
                        createResult.Errors.Select(e => e.Description)));
            }
        }

        // 5. Assign Admin role if not already assigned.
        if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            var roleAssignment = await userManager.AddToRoleAsync(
                admin,
                "Admin");

            if (!roleAssignment.Succeeded)
            {
                throw new InvalidOperationException(
                    "Failed to assign Admin role: " +
                    string.Join(", ",
                        roleAssignment.Errors.Select(e => e.Description)));
            }
        }
    }
}