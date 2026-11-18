using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.DTOs
{
    public class UpdateStudentDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Range(1, 4)]
        public int Year { get; set; }

        [Required]
        public string Phone { get; set; } = string.Empty;
    }
}