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

