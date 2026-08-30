USE BatterySwapDB;
Go

CREATE TABLE Drivers (
    DriverId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    CardNumber NVARCHAR(50) NOT NULL UNIQUE,
    VehicleType NVARCHAR(50) NOT NULL,
    Balance DECIMAL(18,2) DEFAULT 0.00,
    IsActive BIT DEFAULT 1
);