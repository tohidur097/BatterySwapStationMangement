USE BatterySwapDb;
GO

-- ১. আগের কোনো টেবিল থাকলে ড্রপ করা
DROP TABLE IF EXISTS Transactions;
DROP TABLE IF EXISTS Batteries;
DROP TABLE IF EXISTS Users;
GO

-- ২. নতুন টেবিল তৈরি
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100)
);

CREATE TABLE Batteries (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Status NVARCHAR(20) NOT NULL,
    ChargeLevel INT NOT NULL
);

CREATE TABLE Transactions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(Id),
    BatteryId INT FOREIGN KEY REFERENCES Batteries(Id),
    Amount DECIMAL(10,2),
    Date DATETIME
);
GO

-- ৩. টেস্ট ডাটা ইনসার্ট
INSERT INTO Users (Username, Password, Role, Email) VALUES ('admin', '1234', 'Admin', 'admin@email.com');
INSERT INTO Batteries (Status, ChargeLevel) VALUES ('Available', 100), ('In Use', 75), ('Maintenance', 0);
INSERT INTO Users (Username, Password, Role, Email) VALUES ('employee', '1234', 'Employee', 'employee@email.com');

