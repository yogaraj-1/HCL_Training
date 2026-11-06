using CampusPlacementManagement.Api.Models;

namespace CampusPlacementManagement.Api.Interfaces
{
    public interface IAccountService
    {
        Task<bool> ActivateUserAsync(string userId);

        Task<bool> RejectUserAsync(string userId);
    }
}