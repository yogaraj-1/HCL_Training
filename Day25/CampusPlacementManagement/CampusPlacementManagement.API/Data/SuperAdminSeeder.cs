using CampusPlacementManagement.Api.Enums;
using CampusPlacementManagement.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace CampusPlacementManagement.Api.Data
{
    public static class SuperAdminSeeder
    {
        public static async Task SeedAsync(
            IServiceProvider serviceProvider)
        {
            var userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var existingAdmin =
                await userManager.FindByEmailAsync("admin@campusplacement.com");

            if (existingAdmin != null)
            {
                return;
            }

            var admin = new ApplicationUser
            {
                UserName = "admin@campusplacement.com",
                Email = "admin@campusplacement.com",
                FullName = "System Administrator",
                AccountStatus = AccountStatus.Active,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(
                admin,
                "Admin@12345");

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(
                        ", ",
                        result.Errors.Select(e => e.Description)));
            }

            await userManager.AddToRoleAsync(
                admin,
                "SuperAdmin");
        }
    }
}