using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string RegisterNumber { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Range(1, 4)]
        public int Year { get; set; }

        [Required]
        public string Phone { get; set; } = string.Empty;
    }
}