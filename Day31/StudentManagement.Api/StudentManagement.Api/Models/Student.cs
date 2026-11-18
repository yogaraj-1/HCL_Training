namespace StudentManagement.Api.Models
{
    public class Student
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string RegisterNumber { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public int Year { get; set; }

        public string Phone { get; set; } = string.Empty;

        // Navigation property
        public User User { get; set; } = null!;
    }
}