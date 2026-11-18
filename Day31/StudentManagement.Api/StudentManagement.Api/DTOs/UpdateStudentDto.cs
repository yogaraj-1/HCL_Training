using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.DTOs
{
    public class UpdateStudentDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Department { get; set; } = string.Empty;

        [Range(1, 4)]
        public int Year { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
    }
}