
using Microsoft.EntityFrameworkCore;
using MovieBookingSystem.Models;

namespace MovieBookingSystem.Data;

public class MovieBookingDbContext : DbContext
{
    public MovieBookingDbContext(
        DbContextOptions<MovieBookingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");
            entity.HasKey(c => c.CustomerId);

            entity.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(c => c.Email)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasIndex(c => c.Email)
                .IsUnique();

            entity.Property(c => c.Phone)
                .HasMaxLength(15)
                .IsRequired();
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movies");
            entity.HasKey(m => m.MovieId);

            entity.Property(m => m.Title)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(m => m.Genre)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(m => m.TicketPrice)
                .HasPrecision(10, 2);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Bookings");
            entity.HasKey(b => b.BookingId);

            entity.Property(b => b.TotalAmount)
                .HasPrecision(12, 2);

            entity.Property(b => b.BookedAt)
                .HasColumnType("datetime2");

            entity.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(b => b.CustomerId);

            entity.HasOne<Movie>()
                .WithMany()
                .HasForeignKey(b => b.MovieId);
        });
    }
}
