
using Microsoft.EntityFrameworkCore;
using OnlineExaminationQuestionAPI.Models;

namespace OnlineExaminationQuestionAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Question> Questions => Set<Question>();
}
