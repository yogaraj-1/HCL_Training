using CampusPlacementManagement.Api.Enums;
using Microsoft.AspNetCore.Identity;

namespace CampusPlacementManagement.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public AccountStatus AccountStatus { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}