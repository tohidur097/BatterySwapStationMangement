CREATE DATABASE BatterySwapDB;
GO
 
USE BatterySwapDB;
 
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

INSERT INTO Users (Username, Password, Role, Email) VALUES ('admin', '1234', 'Admin', 'admin@email.com');
INSERT INTO Batteries (Status, ChargeLevel) VALUES ('Available', 100), ('In Use', 75), ('Maintenance', 0);
