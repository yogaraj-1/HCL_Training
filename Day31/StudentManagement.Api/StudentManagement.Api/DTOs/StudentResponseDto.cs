namespace StudentManagement.Api.DTOs
{
    public class StudentResponseDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string RegisterNumber { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public int Year { get; set; }

        public string Phone { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}