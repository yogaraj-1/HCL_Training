
using Microsoft.Data.SqlClient;
using MovieBookingSystem.Models;
using MovieBookingSystem.Repository;
using MovieBookingSystem.Services;

string connectionString =
    @"Server=.\SQLEXPRESS;Database=MovieBookingDB;Trusted_Connection=True;TrustServerCertificate=True;";

var customerRepository = new CustomerRepository(connectionString);
var movieRepository = new MovieRepository(connectionString);

var customerService = new CustomerService(customerRepository);
var movieService = new MovieService(movieRepository);

while (true)
{
    Console.WriteLine("\n===== MOVIE BOOKING SYSTEM =====");
    Console.WriteLine("1. Add Customer");
    Console.WriteLine("2. View All Customers");
    Console.WriteLine("3. Search Customer by ID");
    Console.WriteLine("4. Add Movie");
    Console.WriteLine("5. View All Movies");
    Console.WriteLine("6. Search Movie by ID");
    Console.WriteLine("7. Exit");
    Console.Write("Enter your choice: ");

    string choice = Console.ReadLine() ?? "";

    try
    {
        switch (choice)
        {
            case "1":
                {
                    Console.Write("Customer name: ");
                    string name = Console.ReadLine() ?? "";

                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? "";

                    Console.Write("Phone: ");
                    string phone = Console.ReadLine() ?? "";

                    var customer = new Customer
                    {
                        Name = name,
                        Email = email,
                        Phone = phone
                    };

                    await customerService.AddCustomerAsync(customer);

                    Console.WriteLine("Customer added successfully!");
                    break;
                }

            case "2":
                {
                    var customers =
                        await customerService.GetAllCustomersAsync();

                    if (customers.Count == 0)
                    {
                        Console.WriteLine("No customers found.");
                        break;
                    }

                    Console.WriteLine("\n--- CUSTOMER LIST ---");

                    foreach (var customer in customers)
                    {
                        Console.WriteLine(
                            $"{customer.CustomerId} | " +
                            $"{customer.Name} | " +
                            $"{customer.Email} | " +
                            $"{customer.Phone}");
                    }

                    break;
                }

            case "3":
                {
                    Console.Write("Enter customer ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid customer ID.");
                        break;
                    }

                    var customer =
                        await customerService.GetCustomerByIdAsync(id);

                    if (customer is null)
                    {
                        Console.WriteLine("Customer not found.");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"{customer.CustomerId} | " +
                            $"{customer.Name} | " +
                            $"{customer.Email} | " +
                            $"{customer.Phone}");
                    }

                    break;
                }

            case "4":
                {
                    Console.Write("Movie title: ");
                    string title = Console.ReadLine() ?? "";

                    Console.Write("Genre: ");
                    string genre = Console.ReadLine() ?? "";

                    Console.Write("Duration in minutes: ");

                    if (!int.TryParse(Console.ReadLine(), out int duration))
                    {
                        Console.WriteLine("Invalid duration.");
                        break;
                    }

                    Console.Write("Ticket price: ");

                    if (!decimal.TryParse(
                        Console.ReadLine(), out decimal price))
                    {
                        Console.WriteLine("Invalid ticket price.");
                        break;
                    }

                    Console.Write("Available seats: ");

                    if (!int.TryParse(
                        Console.ReadLine(), out int seats))
                    {
                        Console.WriteLine("Invalid seat count.");
                        break;
                    }

                    var movie = new Movie
                    {
                        Title = title,
                        Genre = genre,
                        DurationMinutes = duration,
                        TicketPrice = price,
                        AvailableSeats = seats
                    };

                    await movieService.AddMovieAsync(movie);

                    Console.WriteLine("Movie added successfully!");
                    break;
                }

            case "5":
                {
                    var movies = await movieService.GetAllMoviesAsync();

                    if (movies.Count == 0)
                    {
                        Console.WriteLine("No movies found.");
                        break;
                    }

                    Console.WriteLine("\n--- MOVIE LIST ---");

                    foreach (var movie in movies)
                    {
                        Console.WriteLine(
                            $"{movie.MovieId} | {movie.Title} | " +
                            $"{movie.Genre} | {movie.DurationMinutes} min | " +
                            $"{movie.TicketPrice:N2} | " +
                            $"Seats: {movie.AvailableSeats}");
                    }

                    break;
                }

            case "6":
                {
                    Console.Write("Enter movie ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid movie ID.");
                        break;
                    }

                    var movie =
                        await movieService.GetMovieByIdAsync(id);

                    if (movie is null)
                    {
                        Console.WriteLine("Movie not found.");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"{movie.MovieId} | {movie.Title} | " +
                            $"{movie.Genre} | {movie.DurationMinutes} min | " +
                            $"{movie.TicketPrice:N2} | " +
                            $"Seats: {movie.AvailableSeats}");
                    }

                    break;
                }

            case "7":
                Console.WriteLine("Application closed.");
                return;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Validation error: {ex.Message}");
    }
    catch (SqlException ex)
    {
        Console.WriteLine($"Database error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }
}
