
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/students?search=computer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents(
        [FromQuery] string? search)
    {
        var query = _context.Students.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(s =>
                s.Name.Contains(search) ||
                s.Department.Contains(search));
        }

        var students = await query
            .OrderBy(s => s.Id)
            .ToListAsync();

        return Ok(students);
    }

    // GET: api/students/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return NotFound(new { message = "Student not found." });

        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        var email = student.Email.Trim();

        if (await _context.Students.AnyAsync(s => s.Email == email))
        {
            return Conflict(new
            {
                message = "A student with this email already exists."
            });
        }

        student.Name = student.Name.Trim();
        student.Email = email;
        student.Department = student.Department.Trim();

        _context.Students.Add(student);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            return Conflict(new
            {
                message = "A student with this email already exists."
            });
        }

        return CreatedAtAction(
            nameof(GetStudent),
            new { id = student.Id },
            student);
    }

    // PUT: api/students/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Student>> UpdateStudent(
        int id, Student student)
    {
        if (id != student.Id)
        {
            return BadRequest(new
            {
                message = "Route ID and body ID must match."
            });
        }

        var existingStudent = await _context.Students.FindAsync(id);

        if (existingStudent == null)
            return NotFound(new { message = "Student not found." });

        var email = student.Email.Trim();

        if (await _context.Students.AnyAsync(s =>
            s.Email == email && s.Id != id))
        {
            return Conflict(new
            {
                message = "A student with this email already exists."
            });
        }

        existingStudent.Name = student.Name.Trim();
        existingStudent.Email = email;
        existingStudent.Department = student.Department.Trim();
        existingStudent.Age = student.Age;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            return Conflict(new
            {
                message = "A student with this email already exists."
            });
        }

        return Ok(existingStudent);
    }

    // DELETE: api/students/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return NotFound(new { message = "Student not found." });

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static bool IsUniqueViolation(DbUpdateException ex)
    {
        return ex.InnerException is SqlException sqlException
            && (sqlException.Number == 2601
                || sqlException.Number == 2627);
    }
}
