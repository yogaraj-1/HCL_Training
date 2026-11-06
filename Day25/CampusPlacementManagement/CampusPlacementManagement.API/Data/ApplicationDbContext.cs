using CampusPlacementManagement.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CampusPlacementManagement.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<College> Colleges { get; set; }
        public DbSet<PlacementOfficer> PlacementOfficers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyHR> CompanyHRs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<JobOpening> JobOpenings { get; set; }
        public DbSet<JobTargetCollege> JobTargetColleges { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<RecruitmentDrive> RecruitmentDrives { get; set; }
        public DbSet<InterviewRound> InterviewRounds { get; set; }
        public DbSet<InterviewSchedule> InterviewSchedules { get; set; }
        public DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // College
            modelBuilder.Entity<College>(entity =>
            {
                entity.HasKey(e => e.CollegeId);

                entity.Property(e => e.CollegeName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CollegeCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(e => e.CollegeCode)
                    .IsUnique();

                entity.Property(e => e.UniversityName)
                    .HasMaxLength(200);

                entity.Property(e => e.OfficialEmail)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Website)
                    .HasMaxLength(255);
            });

            // Placement Officer
            modelBuilder.Entity<PlacementOfficer>(entity =>
            {
                entity.HasKey(e => e.PlacementOfficerId);

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.College)
                    .WithMany(e => e.PlacementOfficers)
                    .HasForeignKey(e => e.CollegeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Company
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CompanyEmail)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Website)
                    .HasMaxLength(255);

                entity.Property(e => e.Industry)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Description)
                    .HasColumnType("nvarchar(max)");
            });

            // Company HR
            modelBuilder.Entity<CompanyHR>(entity =>
            {
                entity.HasKey(e => e.CompanyHRId);

                entity.Property(e => e.Designation)
                    .HasMaxLength(100);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Company)
                    .WithMany(e => e.CompanyHRs)
                    .HasForeignKey(e => e.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.RegisterNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(e => new
                {
                    e.CollegeId,
                    e.RegisterNumber
                }).IsUnique();

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Degree)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CGPA)
                    .HasPrecision(4, 2);

                entity.Property(e => e.ResumeUrl)
                    .HasMaxLength(500);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.College)
                    .WithMany(e => e.Students)
                    .HasForeignKey(e => e.CollegeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PlacementOfficer)
                    .WithMany(e => e.Students)
                    .HasForeignKey(e => e.PlacementOfficerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Job Opening
            modelBuilder.Entity<JobOpening>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                entity.Property(e => e.JobType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SalaryPackage)
                    .HasPrecision(12, 2);

                entity.Property(e => e.MinimumCGPA)
                    .HasPrecision(4, 2);

                entity.HasOne(e => e.Company)
                    .WithMany(e => e.JobOpenings)
                    .HasForeignKey(e => e.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.CreatedByHR)
                    .WithMany(e => e.JobOpenings)
                    .HasForeignKey(e => e.CreatedByHRId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Job Target College
            modelBuilder.Entity<JobTargetCollege>(entity =>
            {
                entity.HasKey(e => e.JobTargetCollegeId);

                entity.HasIndex(e => new
                {
                    e.JobId,
                    e.CollegeId
                }).IsUnique();

                entity.HasOne(e => e.Job)
                    .WithMany(e => e.TargetColleges)
                    .HasForeignKey(e => e.JobId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.College)
                    .WithMany(e => e.JobTargetColleges)
                    .HasForeignKey(e => e.CollegeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Job Application
            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId);

                entity.HasIndex(e => new
                {
                    e.StudentId,
                    e.JobId
                }).IsUnique();

                entity.Property(e => e.ResumeUrl)
                    .HasMaxLength(500);

                entity.HasOne(e => e.Job)
                    .WithMany(e => e.Applications)
                    .HasForeignKey(e => e.JobId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Student)
                    .WithMany(e => e.JobApplications)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Recruitment Drive
            modelBuilder.Entity<RecruitmentDrive>(entity =>
            {
                entity.HasKey(e => e.DriveId);

                entity.Property(e => e.Venue)
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .HasColumnType("nvarchar(max)");

                entity.HasOne(e => e.Job)
                    .WithMany(e => e.RecruitmentDrives)
                    .HasForeignKey(e => e.JobId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.College)
                    .WithMany(e => e.RecruitmentDrives)
                    .HasForeignKey(e => e.CollegeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Interview Round
            modelBuilder.Entity<InterviewRound>(entity =>
            {
                entity.HasKey(e => e.InterviewRoundId);

                entity.Property(e => e.RoundName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasColumnType("nvarchar(max)");

                entity.HasOne(e => e.Job)
                    .WithMany(e => e.InterviewRounds)
                    .HasForeignKey(e => e.JobId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Interview Schedule
            modelBuilder.Entity<InterviewSchedule>(entity =>
            {
                entity.HasKey(e => e.InterviewScheduleId);

                entity.Property(e => e.MeetingLink)
                    .HasMaxLength(500);

                entity.Property(e => e.Venue)
                    .HasMaxLength(255);

                entity.Property(e => e.Feedback)
                    .HasMaxLength(1000);

                entity.HasOne(e => e.Application)
                    .WithMany(e => e.InterviewSchedules)
                    .HasForeignKey(e => e.ApplicationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.InterviewRound)
                    .WithMany(e => e.InterviewSchedules)
                    .HasForeignKey(e => e.InterviewRoundId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Application Status History
            modelBuilder.Entity<ApplicationStatusHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500);

                entity.HasOne(e => e.Application)
                    .WithMany(e => e.StatusHistory)
                    .HasForeignKey(e => e.ApplicationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.UpdatedByUser)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}