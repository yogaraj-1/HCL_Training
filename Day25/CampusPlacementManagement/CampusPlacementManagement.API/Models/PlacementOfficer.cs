using CampusPlacementManagement.Api.Enums;

namespace CampusPlacementManagement.Api.Models
{
    public class PlacementOfficer
    {
        public int PlacementOfficerId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int CollegeId { get; set; }

        public string Designation { get; set; } = string.Empty;

        public StudentStatus Status { get; set; }

        public string? ApprovedBy { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public College College { get; set; } = null!;

        public ICollection<Student> Students { get; set; }
            = new List<Student>();
    }
}