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
/*
SELECT DB_NAME() AS CurrentDatabase;
GO

SELECT @@SERVERNAME AS ServerName;
GO

USE MovieBookingDB;
GO

SELECT CustomerId, Name, Email, Phone
FROM dbo.Customers;
GO*/


/*USE MovieBookingDB;
GO

CREATE TABLE Bookings
(
    BookingId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    MovieId INT NOT NULL,
    Seats INT NOT NULL CHECK (Seats > 0),
    TotalAmount DECIMAL(12,2) NOT NULL
        CHECK (TotalAmount >= 0),
    BookedAt DATETIME2 NOT NULL
        DEFAULT SYSDATETIME(),

    CONSTRAINT FK_Bookings_Customers
        FOREIGN KEY (CustomerId)
        REFERENCES Customers(CustomerId),

    CONSTRAINT FK_Bookings_Movies
        FOREIGN KEY (MovieId)
        REFERENCES Movies(MovieId)
);
GO

CREATE TABLE BookingAudit
(
    AuditId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    MovieId INT NOT NULL,
    ActionName NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL
        DEFAULT SYSDATETIME()
);
GO
*/
SELECT * FROM dbo.Customers;
SELECT * FROM dbo.Movies;
SELECT * FROM dbo.Bookings;
SELECT * FROM dbo.BookingAudit;