
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Eight database tables

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Theatre> Theatres { get; set; }

    public DbSet<Screen> Screens { get; set; }

    public DbSet<Seat> Seats { get; set; }

    public DbSet<Show> Shows { get; set; }

    public DbSet<ShowSeat> ShowSeats { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<BookingSeat> BookingSeats { get; set; }


    // Configure database relationships and constraints

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // MOVIE CONFIGURATION
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(m => m.MovieId);

            entity.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(m => m.Description)
                .HasMaxLength(500);

            entity.Property(m => m.DurationMinutes)
                .IsRequired();

            entity.Property(m => m.ReleaseDate)
                .IsRequired();

            entity.Property(m => m.Language)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasMany(m => m.Shows)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        // THEATRE CONFIGURATION
        modelBuilder.Entity<Theatre>(entity =>
        {
            entity.HasKey(t => t.TheatreId);

            entity.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(t => t.Location)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasMany(t => t.Screens)
                .WithOne(s => s.Theatre)
                .HasForeignKey(s => s.TheatreId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        // SCREEN CONFIGURATION
        modelBuilder.Entity<Screen>(entity =>
        {
            entity.HasKey(s => s.ScreenId);

            entity.Property(s => s.ScreenName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(s => new
            {
                s.TheatreId,
                s.ScreenName
            }).IsUnique();

            entity.HasMany(s => s.Seats)
                .WithOne(seat => seat.Screen)
                .HasForeignKey(seat => seat.ScreenId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(s => s.Shows)
                .WithOne(show => show.Screen)
                .HasForeignKey(show => show.ScreenId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        // SEAT CONFIGURATION
        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(s => s.SeatId);

            entity.Property(s => s.RowName)
                .IsRequired()
                .HasMaxLength(5);

            entity.Property(s => s.SeatNumber)
                .IsRequired();

            entity.Property(s => s.SeatType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            entity.HasIndex(s => new
            {
                s.ScreenId,
                s.RowName,
                s.SeatNumber
            }).IsUnique();

            entity.HasMany(s => s.ShowSeats)
                .WithOne(ss => ss.Seat)
                .HasForeignKey(ss => ss.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(s => s.BookingSeats)
                .WithOne(bs => bs.Seat)
                .HasForeignKey(bs => bs.SeatId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        // SHOW CONFIGURATION
        modelBuilder.Entity<Show>(entity =>
        {
            entity.HasKey(s => s.ShowId);

            entity.Property(s => s.StartTime)
                .IsRequired();

            entity.Property(s => s.EndTime)
                .IsRequired();

            entity.Property(s => s.TicketPrice)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Shows_EndTime",
                    "[EndTime] > [StartTime]");

                t.HasCheckConstraint(
                    "CK_Shows_TicketPrice",
                    "[TicketPrice] >= 0");
            });

            entity.HasMany(s => s.ShowSeats)
                .WithOne(ss => ss.Show)
                .HasForeignKey(ss => ss.ShowId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(s => s.Bookings)
                .WithOne(b => b.Show)
                .HasForeignKey(b => b.ShowId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        // SHOW SEAT CONFIGURATION
        modelBuilder.Entity<ShowSeat>(entity =>
        {
            entity.HasKey(ss => ss.ShowSeatId);

            entity.Property(ss => ss.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.HasIndex(ss => new
            {
                ss.ShowId,
                ss.SeatId
            }).IsUnique();
        });


        // BOOKING CONFIGURATION
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(b => b.BookingId);

            entity.Property(b => b.BookingReference)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasIndex(b => b.BookingReference)
                .IsUnique();

            entity.Property(b => b.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(b => b.CustomerEmail)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(b => b.CustomerPhone)
                .IsRequired()
                .HasMaxLength(15);

            entity.Property(b => b.BookingDate)
                .IsRequired();

            entity.Property(b => b.TotalAmount)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.Property(b => b.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Bookings_TotalAmount",
                    "[TotalAmount] >= 0");
            });

            entity.HasMany(b => b.BookingSeats)
                .WithOne(bs => bs.Booking)
                .HasForeignKey(bs => bs.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        // BOOKING SEAT CONFIGURATION
        modelBuilder.Entity<BookingSeat>(entity =>
        {
            entity.HasKey(bs => bs.BookingSeatId);

            entity.Property(bs => bs.Price)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_BookingSeats_Price",
                    "[Price] >= 0");
            });
        });
    }
}
