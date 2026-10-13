
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineExaminationQuestionAPI.Data;
using OnlineExaminationQuestionAPI.Models;

namespace OnlineExaminationQuestionAPI.Controllers;

[ApiController]
[Route("api/questions")]
public class QuestionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public QuestionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/questions?subject=Java
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Question>>> GetQuestions(
        [FromQuery] string? subject)
    {
        var query = _context.Questions.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(subject))
        {
            query = query.Where(q => q.Subject == subject.Trim());
        }

        var questions = await query
            .OrderBy(q => q.Id)
            .ToListAsync();

        return Ok(questions);
    }

    // GET: api/questions/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Question>> GetQuestion(int id)
    {
        var question = await _context.Questions.FindAsync(id);

        if (question == null)
        {
            return NotFound(new
            {
                message = "Question not found."
            });
        }

        return Ok(question);
    }

    // POST: api/questions
    [HttpPost]
    public async Task<ActionResult<Question>> CreateQuestion(
        Question question)
    {
        NormalizeQuestion(question);

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetQuestion),
            new { id = question.Id },
            question);
    }

    // PUT: api/questions/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Question>> UpdateQuestion(
        int id, Question question)
    {
        if (id != question.Id)
        {
            return BadRequest(new
            {
                message = "Route ID and body ID must match."
            });
        }

        var existingQuestion = await _context.Questions.FindAsync(id);

        if (existingQuestion == null)
        {
            return NotFound(new
            {
                message = "Question not found."
            });
        }

        NormalizeQuestion(question);

        existingQuestion.Text = question.Text;
        existingQuestion.Subject = question.Subject;
        existingQuestion.OptionA = question.OptionA;
        existingQuestion.OptionB = question.OptionB;
        existingQuestion.OptionC = question.OptionC;
        existingQuestion.OptionD = question.OptionD;
        existingQuestion.CorrectOption = question.CorrectOption;

        await _context.SaveChangesAsync();

        return Ok(existingQuestion);
    }

    // DELETE: api/questions/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var question = await _context.Questions.FindAsync(id);

        if (question == null)
        {
            return NotFound(new
            {
                message = "Question not found."
            });
        }

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static void NormalizeQuestion(Question question)
    {
        question.Text = question.Text.Trim();
        question.Subject = question.Subject.Trim();
        question.OptionA = question.OptionA.Trim();
        question.OptionB = question.OptionB.Trim();
        question.OptionC = question.OptionC.Trim();
        question.OptionD = question.OptionD.Trim();
        question.CorrectOption = question.CorrectOption.Trim().ToUpperInvariant();
    }
}

