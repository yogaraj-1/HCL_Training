using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Services;
using System.Security.Claims;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(
            IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/students/me
        [HttpGet("me")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = GetCurrentUserId();

            var student =
                await _studentService.GetByUserIdAsync(userId);

            if (student == null)
            {
                return NotFound(new
                {
                    message = "Student profile not found."
                });
            }

            return Ok(student);
        }

        // GET: api/students
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students =
                await _studentService.GetAllAsync();

            return Ok(students);
        }

        // GET: api/students/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student =
                await _studentService.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound(new
                {
                    message = "Student not found."
                });
            }

            return Ok(student);
        }

        // PUT: api/students/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> UpdateStudent(
            int id,
            UpdateStudentDto dto)
        {
            var currentUserId = GetCurrentUserId();

            var currentUserRole =
                User.FindFirstValue(ClaimTypes.Role);

            var student =
                await _studentService.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound(new
                {
                    message = "Student not found."
                });
            }

            // Student can update only their own profile
            if (currentUserRole == "Student" &&
                student.UserId != currentUserId)
            {
                return Forbid();
            }

            var updatedStudent =
                await _studentService.UpdateAsync(id, dto);

            return Ok(updatedStudent);
        }

        // DELETE: api/students/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result =
                await _studentService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Student not found."
                });
            }

            return Ok(new
            {
                message = "Student deleted successfully."
            });
        }

        private int GetCurrentUserId()
        {
            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            return int.Parse(userId!);
        }
    }
}