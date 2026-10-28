# Movie Booking System — Commit Log

This document tracks the development history of the Movie Booking System. Each entry explains the purpose of a commit, the files changed, the concepts learned, and the testing performed.

---

## Commit 1: Project Initialization

**Commit message:** `8553e60 ->chore: initialize movie booking project`

**Objective:**  
Create the initial .NET 10 Console Application and establish the Git repository.

**Changes made:**
- Created the MovieBookingSystem project using Visual Studio.
- Initialized Git version control.
- Added a `.gitignore` file to exclude generated build files and local IDE files.
- Created the `docs` directory and project planning document.

**Concepts learned:**
- .NET Console Application structure.
- Git repository initialization.
- Purpose of `.gitignore`.
- Basic Git workflow.

**Testing performed:**
- Ran the initial console application successfully.
- Verified the initial Git commit.

**Result:** Completed.

**Commit hash:** Add the actual short hash from `git log --oneline`.

---

## Commit 2: Database Schema

**Commit message:** `1c07137 ->feat(db): create customer and movie tables`

**Objective:**  
Create the SQL Server database schema required to store customer and movie information.

**Changes made:**
- Created the `MovieBookingDB` database.
- Created the `Customers` table with customer ID, name, email, and phone.
- Created the `Movies` table with movie ID, title, genre, duration, ticket price, and available seats.
- Added primary key, unique, not-null, and check constraints.
- Saved the SQL script in `database/MovieBookingDB.sql`.

**Concepts learned:**
- SQL Server database and table creation.
- Primary keys and identity columns.
- Unique and check constraints.
- Data integrity and SQL scripts in version control.

**Testing performed:**
- Executed the SQL script in SSMS.
- Verified that both tables were created.
- Confirmed that SELECT queries returned empty result sets before inserting records.

**Result:** Update to Passed after verifying the database and tables in SSMS.

**Commit hash:** Add the actual short hash from `git log --oneline`.

---

## Upcoming Work

- Create Customer and Movie model classes.
- Install Microsoft.Data.SqlClient.
- Establish database connectivity using ADO.NET.
- Implement customer management.
- Implement movie management.
- Implement transactional ticket booking.

---

## Commit: Models and Database Connectivity

**Commit message:** `05059e2->feat: add models and database connectivity`

### 1. Objective
Create the C# model classes for customers and movies and establish a connection between the console application and SQL Server using ADO.NET.

### 2. Changes Made
- Created the `Models` folder.
- Added `Customer.cs` to represent customer data.
- Added `Movie.cs` to represent movie data.
- Installed the `Microsoft.Data.SqlClient` NuGet package.
- Configured a SQL Server connection string in `Program.cs`.
- Implemented an asynchronous database connection test using `SqlConnection` and `OpenAsync()`.

### 3. Files Changed
- `Models/Customer.cs`
- `Models/Movie.cs`
- `Program.cs`
- `MovieBookingSystem.csproj` (if modified by package installation)

### 4. Concepts Learned
- C# classes and properties.
- Model-layer responsibilities in layered architecture.
- NuGet package management.
- ADO.NET database connectivity.
- `SqlConnection` and `OpenAsync()`.
- Asynchronous programming using `async` and `await`.
- Exception handling with `try-catch`.

### 5. Testing Performed
- Verified that the project builds successfully.
- Ran the console application.
- Tested the SQL Server connection using `OpenAsync()`.
- Expected successful output: `Database connected successfully!`

**Actual result:** Update after running the test.

### 6. Outcome
The application has the initial data models and the foundation required to communicate with SQL Server.

### 7. Next Step
Implement the Customer Repository, starting with the `AddCustomerAsync()` method to insert customer records using parameterized SQL.

**Commit hash:** Add the actual hash from `git log --oneline`

---

## Commit: Add Customer Feature

**Commit message:** `c5a2a58  feat(customer): implement add customer`

**Objective:**  
Implement the first customer-management operation using ADO.NET.

**Changes made:**
- Created `CustomerRepository.cs` to insert customer records into SQL Server.
- Created `CustomerService.cs` to validate customer input.
- Used parameterized SQL to insert customer details.
- Integrated the service and repository in `Program.cs` for testing.
- Added exception handling for validation and database errors.

**Concepts learned:**
- Repository and service layers.
- Dependency passing through constructors.
- `SqlConnection` and `SqlCommand`.
- SQL parameters and `ExecuteNonQueryAsync()`.
- Asynchronous methods and exception handling.

**Testing performed:**
- Valid customer insertion.
- Empty customer name validation.
- Database verification using SSMS.
- Duplicate email handling, if tested.

**Actual result:** Update after testing.

**Commit hash:** Add the actual hash from Git.

**Next step:** Implement viewing all customers.




---

## Commit: View All Customers

**Commit message:** `3c94fe8->feat(customer): add list customers operation`

**Objective:**  
Retrieve and display all customer records stored in SQL Server.

**Changes made:**
- Added `GetAllCustomersAsync()` to `CustomerRepository`.
- Used `ExecuteReaderAsync()` and `SqlDataReader` to retrieve records.
- Converted database rows into `Customer` objects.
- Returned the records as a `List<Customer>` through the service layer.
- Displayed customer details in the console.
- Handled the empty result case.

**Concepts learned:**
- SELECT queries in ADO.NET.
- `ExecuteReaderAsync()` and `ReadAsync()`.
- Mapping database columns to C# properties.
- Collections and `List<Customer>`.
- Asynchronous database operations.

**Testing performed:**
- Verified retrieval of existing customer records.
- Verified output ordering by customer ID.
- Verified the empty-table scenario, if tested.
- Confirmed successful build.

**Actual result:** Update based on the tests performed.

**Commit hash:** Add the actual short hash from Git.

**Next step:** Implement searching for a customer by ID.


---

## Commit: Search Customer by ID

**Commit message:** `bb1170a->.feat(customer): add search by id`

### Objective
Retrieve a specific customer record using its unique customer ID.

### Changes made
- Added `GetCustomerByIdAsync(int id)` to `CustomerRepository`.
- Used a parameterized SQL SELECT query with a WHERE clause.
- Mapped the matching database row into a Customer object.
- Returned null when no matching customer exists.
- Added positive-ID validation in `CustomerService`.
- Displayed customer details in the console.

### Concepts learned
- Parameterized SQL queries.
- WHERE clause filtering.
- Nullable reference types (`Customer?`).
- Single-record retrieval using `SqlDataReader`.
- Input validation and handling missing records.

### Testing performed
- Searched for an existing customer.
- Searched for a nonexistent customer ID.
- Tested zero and negative IDs.
- Tested non-numeric input.
- Confirmed the project builds successfully.

**Actual result:** Update according to the tests performed.

**Commit hash:** Add the actual short hash from Git.

**Next step:** Implement Add Movie.

---

## Commit: Add Movie Feature

**Commit message:** `c318c58 ->feat(movie): implement add movie`

### Objective
Implement the functionality to insert movie records into SQL Server using ADO.NET.

### Changes made
- Created `MovieRepository.cs`.
- Implemented `AddMovieAsync()` using a parameterized SQL INSERT statement.
- Created `MovieService.cs`.
- Added validation for movie title, genre, duration, ticket price, and available seats.
- Tested the feature through `Program.cs`.

### Concepts learned
- Repository and service layers.
- Parameterized SQL commands.
- `ExecuteNonQueryAsync()`.
- Asynchronous database operations.
- Business validation and SQL CHECK constraints.
- Exception handling.

### Testing performed
- Tested insertion of a valid movie.
- Tested invalid title and genre.
- Tested invalid duration, ticket price, and seat count.
- Verified saved movie records using SSMS.
- Confirmed that the project builds successfully.

**Actual result:** Update after performing the tests.

**Commit hash:** Add the actual short hash from Git.

**Next step:** Implement View All Movies.

---

## Commit: View All Movies

**Commit message:** `a3cdaea->feat(movie): add list movies operation`

### Objective
Retrieve and display all movie records stored in SQL Server.

### Changes made
- Added `GetAllMoviesAsync()` to `MovieRepository`.
- Used `ExecuteReaderAsync()` and `SqlDataReader` to retrieve movie records.
- Mapped database rows into `Movie` objects.
- Returned the results as a `List<Movie>` through the service layer.
- Displayed movie details, ticket prices, and available seats.
- Handled the empty result case.

### Concepts learned
- SELECT queries in ADO.NET.
- Reading multiple database rows asynchronously.
- Mapping database values to C# properties.
- Collections and `List<Movie>`.
- Decimal formatting for ticket prices.

### Testing performed
- Verified retrieval of existing movies.
- Verified ordering by movie ID.
- Verified ticket price and seat display.
- Tested the empty-table scenario, if performed.
- Confirmed a successful build.

**Actual result:** Update according to the tests performed.

**Commit hash:** Add the actual short hash from Git.

**Next step:** Implement Search Movie by ID.

---

## Commit: Search Movie by ID

**Commit message:** `-> feat(movie): add search by id`

### Objective
Retrieve a specific movie using its unique ID.

### Changes made
- Added `GetMovieByIdAsync(int id)` to `MovieRepository`.
- Used a parameterized SELECT query with a WHERE clause.
- Mapped the matching database row into a Movie object.
- Returned null when no matching movie exists.
- Added positive-ID validation in `MovieService`.
- Displayed the movie's details in the console.

### Concepts learned
- Parameterized SQL queries.
- WHERE clause filtering.
- Nullable reference types (`Movie?`).
- Single-record retrieval with SqlDataReader.
- Input validation and missing-record handling.

### Testing performed
- Searched for an existing movie.
- Tested a nonexistent movie ID.
- Tested zero and negative IDs.
- Tested non-numeric input.
- Verified the project builds successfully.

**Actual result:** Update according to the tests performed.

**Commit hash:** Add the actual short hash from Git.

**Next step:** Integrate customer and movie operations into the main console menu.

---

## Commit: Integrate Customer and Movie Menu

**Commit message:** `2b0c096->feat: integrate customer and movie menu`

### Objective
Combine the six customer and movie operations into a single persistent console application.

### Changes made
- Added a main menu with seven options, including Exit.
- Integrated Add Customer, View All Customers, and Search Customer by ID.
- Integrated Add Movie, View All Movies, and Search Movie by ID.
- Added input parsing for numeric fields and IDs.
- Added centralized exception handling for validation, database, and unexpected errors.
- Kept the application running until the user selects Exit.

### Concepts learned
- Switch statements.
- Loops and menu-driven applications.
- Reusing service and repository instances.
- Input validation and exception handling.
- Integrating independently developed features.

### Testing performed
- Tested all six operations through the main menu.
- Tested nonexistent IDs and invalid input.
- Verified that the menu reappears after operations.
- Verified application exit.

**Actual result:** Update based on tests performed.

**Commit hash:** Add the actual short hash from Git.

**Next step:** Implement transactional ticket booking.

# Commit Log — Movie Booking System

## Ticket Booking Module

### 1. Create Booking and Audit Tables

**Commit message**
```text
feat(db): add bookings and audit tables
```

**Changes made**
- Created the `Bookings` table to store ticket booking details.
- Added foreign key relationships to `Customers` and `Movies`.
- Added constraints to ensure the number of seats is positive and the total amount is non-negative.
- Created the `BookingAudit` table to record booking actions.
- Added booking timestamps using SQL Server's `SYSDATETIME()` default.

**Purpose**

Establish the database structure required to save bookings and maintain an audit record of successful booking operations.

---

### 2. Implement Transactional Booking Repository

**Commit message**
```text
feat(booking): implement transactional booking
```

**Changes made**
- Created `Models/Booking.cs` to represent a booking.
- Created `Repository/BookingRepository.cs`.
- Implemented `BookTicketsAsync()` using ADO.NET.
- Validated that the customer exists.
- Checked movie availability before reducing the available seat count.
- Calculated the total booking amount using the movie's ticket price and requested seats.
- Inserted the booking record.
- Inserted a corresponding record into `BookingAudit`.
- Used a SQL transaction to commit all booking operations together or roll them back on failure.
- Used parameterized SQL commands to prevent SQL injection.

**Purpose**

Ensure that updating available seats, recording a booking, and recording the audit entry are treated as a single database transaction.

---

### 3. Add Booking Service and Validation

**Commit message**
```text
feat(booking): add booking validation
```

**Changes made**
- Created `Services/BookingService.cs`.
- Added validation for customer ID, movie ID, and seat count.
- Rejected non-positive IDs and seat counts.
- Delegated valid booking requests to `BookingRepository`.

**Purpose**

Separate business validation from database operations and maintain the project's layered architecture.

---

### 4. Integrate Ticket Booking into the Main Menu

**Commit message**
```text
feat: integrate ticket booking menu
```

**Changes made**
- Added the `Book Tickets` option to `Program.cs`.
- Collected customer ID, movie ID, and requested seat count from the user.
- Validated numeric console input.
- Called `BookingService.BookTicketsAsync()`.
- Displayed success or failure messages.
- Handled booking-related exceptions.

**Purpose**

Allow users to book movie tickets through the console application while keeping booking logic in the service and repository layers.

---

### 5. Document the Ticket Booking Module

**Commit message**
```text
docs: document ticket booking module
```

**Changes made**
- Documented the booking workflow.
- Explained the role of the booking service and repository.
- Documented database transaction and rollback behavior.
- Added booking test scenarios and expected results.
- Recorded the relevant database tables and relationships.

**Purpose**

Make the ticket-booking implementation, database behavior, and testing process easier to understand and maintain.

---

## Booking Module Testing Checklist

Record the actual test results before marking the module as complete.

| Test scenario | Expected result | Status |
|---|---|---|
| Book tickets with valid customer, movie, and seats | Booking and audit records are inserted; available seats decrease | Pending |


## EF Core Migration

### 1. Add EF Core SQL Server Support

**Commit message**
```text
feat: add EF Core SQL Server support
```

**Changes made**
- Installed `Microsoft.EntityFrameworkCore.SqlServer`.
- Prepared the project to use EF Core with the existing SQL Server database.
- Retained the existing database and tables.

**Purpose**

Enable EF Core to communicate with SQL Server without manually managing database connections and SQL commands in every repository.

---

### 2. Add EF Core DbContext

**Commit message**
```text
feat: add MovieBookingDbContext
```

**Changes made**
- Created `Data/MovieBookingDbContext.cs`.
- Added `DbSet<Customer>`, `DbSet<Movie>`, and `DbSet<Booking>`.
- Configured existing table names, primary keys, property constraints, and booking relationships.
- Configured the decimal precision of ticket prices and booking totals.

**Purpose**

Provide a central database context for accessing existing tables through EF Core.

---

### 3. Migrate Customer and Movie Repositories

**Commit message**
```text
refactor: migrate customer and movie repositories to EF Core
```

**Changes made**
- Replaced ADO.NET connection and command operations with EF Core.
- Used `Add()` and `SaveChangesAsync()` to insert records.
- Used LINQ and `ToListAsync()` to retrieve customers and movies.
- Implemented ID searches using `FirstOrDefaultAsync()`.
- Used `AsNoTracking()` for read-only queries.

**Purpose**

Simplify customer and movie data access while retaining the existing repository method interfaces.

---

### 4. Migrate Booking Repository

**Commit message**
```text
refactor: migrate booking repository to EF Core
```

**Changes made**
- Replaced ADO.NET booking operations with EF Core.
- Used an EF Core database transaction for the booking workflow.
- Added a conditional seat update using `ExecuteUpdateAsync()`.
- Saved the booking record through `SaveChangesAsync()`.
- Preserved the booking audit insert within the transaction.
- Added rollback handling for failed booking operations.

**Purpose**

Preserve transactional booking behavior while using EF Core for data access.

---

### 5. Integrate EF Core into Program

**Commit message**
```text
refactor: configure EF Core in Program
```

**Changes made**
- Configured `DbContextOptionsBuilder<MovieBookingDbContext>`.
- Configured the SQL Server provider with the existing connection string.
- Created a `MovieBookingDbContext` instance.
- Updated repository initialization to receive the context instead of the connection string.
- Retained the existing services and console menu.

**Purpose**

Connect the application to EF Core while minimizing changes to the existing project architecture.

---

## EF Core Migration Testing

| Test | Expected result | Status |
|---|---|---|
| Project build | Build succeeds without errors | Pending |
| Add and list customers | Operations work correctly | Pending |
| Search customer by ID | Correct customer is returned | Pending |
| Add and list movies | Operations work correctly | Pending |
| Search movie by ID | Correct movie is returned | Pending |
| Book tickets | Booking is saved and seats decrease | Pending |
| Insufficient seats | Booking fails and seats remain unchanged | Pending |
| Booking audit | Successful booking has an audit entry | Pending |
| Rollback behavior | Failed booking does not leave partial changes | Pending |

Update each status after running the tests.

## Important Notes

- The existing `MovieBookingDB` database is reused.
- The existing Models and Services are retained unless compatibility changes are needed.
- EF Core migrations are not required merely to replace ADO.NET repository code.
- Record the actual Git commit hashes and test outcomes when the work is complete.





