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
