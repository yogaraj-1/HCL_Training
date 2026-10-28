/*CREATE DATABASE MovieBookingDB;
GO

USE MovieBookingDB;
GO*/
/*USE MovieBookingDB;
GO

CREATE TABLE Customers
(
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Phone NVARCHAR(15) NOT NULL
);
GO

CREATE TABLE Movies
(
    MovieId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    Genre NVARCHAR(50) NOT NULL,
    DurationMinutes INT NOT NULL
        CHECK (DurationMinutes > 0),
    TicketPrice DECIMAL(10,2) NOT NULL
        CHECK (TicketPrice >= 0),
    AvailableSeats INT NOT NULL
        CHECK (AvailableSeats >= 0)
);
Go
*/

--SELECT * FROM Customers;
--SELECT * FROM Movies;