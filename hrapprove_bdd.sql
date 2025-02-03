CREATE DATABASE IF NOT EXISTS HRApprove;
USE HRApprove;

-- Create tables
CREATE TABLE Employees (
    EmployeeId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    DateHired DATE NOT NULL
);

CREATE TABLE Roles (
    RoleId INT AUTO_INCREMENT PRIMARY KEY,
    Label VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE EmployeeRoles (
    EmployeeId INT NOT NULL,
    RoleId INT NOT NULL,
    PRIMARY KEY (EmployeeId, RoleId),
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId) ON DELETE CASCADE
);

CREATE TABLE LeaveTypes (
    LeaveTypeId INT AUTO_INCREMENT PRIMARY KEY,
    Label VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE LeaveRequests (
    LeaveRequestId INT AUTO_INCREMENT PRIMARY KEY,
    EmployeeId INT NOT NULL,
    LeaveTypeId INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Comment TEXT,
    Status ENUM('Pending', 'Approved', 'Rejected') DEFAULT 'Pending',
    HRComment TEXT,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId) ON DELETE CASCADE,
    FOREIGN KEY (LeaveTypeId) REFERENCES LeaveTypes(LeaveTypeId) ON DELETE CASCADE
);

-- Insert data into the tables
INSERT INTO Employees (FirstName, LastName, Email, DateHired)
VALUES
    ('John', 'Doe', 'john.doe@example.com', '2024-01-15'),
    ('Alice', 'Johnson', 'alice.johnson@example.com', '2021-09-01');

INSERT INTO Roles (Label)
VALUES
    ('Employee'),
    ('HR');

INSERT INTO EmployeeRoles (EmployeeId, RoleId)
VALUES
    (1, 1), -- John Doe - Employee
    (2, 1), -- Alice Johnson - Employee
    (2, 2); -- Alice Johnson - HR

INSERT INTO LeaveTypes (Label)
VALUES
    ('Vacation'),
    ('Sick'),
    ('Other');


INSERT INTO LeaveRequests (EmployeeId, LeaveTypeId, StartDate, EndDate, Comment, Status)
VALUES
    (1, 1, '2025-02-10', '2025-02-15', 'Family vacation', 'Pending'),
    (2, 2, '2025-03-01', '2025-03-05', 'Flu recovery', 'Pending');
