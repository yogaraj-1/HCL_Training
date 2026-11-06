using CampusPlacementManagement.Api.Enums;
using CampusPlacementManagement.Api.Interfaces;
using CampusPlacementManagement.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace CampusPlacementManagement.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ActivateUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            user.AccountStatus = AccountStatus.Active;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> RejectUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            user.AccountStatus = AccountStatus.Rejected;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}