-- Student Management System Database

CREATE DATABASE StudentManagementDB;
GO

USE StudentManagementDB;
GO

CREATE TABLE Department (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE,
    DepartmentCode NVARCHAR(10) NOT NULL UNIQUE
);


CREATE TABLE Student (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Email NVARCHAR(100) UNIQUE,
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    DepartmentID INT,
    EnrollmentDate DATE DEFAULT GETDATE(),
    FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID) ON DELETE SET NULL
);


CREATE TABLE Course (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100) NOT NULL,
    CourseCode NVARCHAR(20) NOT NULL UNIQUE,
    Credits INT NOT NULL CHECK (Credits > 0),
    DepartmentID INT,
    Description NVARCHAR(500),
    FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID) ON DELETE SET NULL
);


CREATE TABLE Enrollment (
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL,
    CourseID INT NOT NULL,
    EnrollmentDate DATE DEFAULT GETDATE(),
    Grade DECIMAL(5,2) NULL CHECK (Grade >= 0 AND Grade <= 100),
    Semester NVARCHAR(20),
    AcademicYear INT,
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID) ON DELETE CASCADE,
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE,
    CONSTRAINT UQ_Enrollment UNIQUE(StudentID, CourseID, Semester, AcademicYear)
);


CREATE TABLE Instructor (
    InstructorID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE,
    Phone NVARCHAR(20),
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID) ON DELETE SET NULL
);


CREATE TABLE CourseInstructor (
    CourseInstructorID INT PRIMARY KEY IDENTITY(1,1),
    CourseID INT NOT NULL,
    InstructorID INT NOT NULL,
    Semester NVARCHAR(20),
    AcademicYear INT,
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE,
    FOREIGN KEY (InstructorID) REFERENCES Instructor(InstructorID) ON DELETE CASCADE,
    CONSTRAINT UQ_CourseInstructor UNIQUE(CourseID, InstructorID, Semester, AcademicYear)
);


INSERT INTO Department (DepartmentName, DepartmentCode) VALUES
('Computer Science', 'CS'),
('Mathematics', 'MATH'),
('Engineering', 'ENG'),
('Business', 'BUS');

INSERT INTO Student (FirstName, LastName, DateOfBirth, Email, Phone, Address, DepartmentID) VALUES
('Ahmed', 'Mohamed', '2002-05-15', 'ahmed.mohamed@email.com', '01012345678', 'Cairo, Egypt', 1),
('Sara', 'Ali', '2003-08-22', 'sara.ali@email.com', '01098765432', 'Giza, Egypt', 1),
('Omar', 'Hassan', '2002-11-10', 'omar.hassan@email.com', '01155555555', 'Alexandria, Egypt', 2);

INSERT INTO Course (CourseName, CourseCode, Credits, DepartmentID, Description) VALUES
('Data Structures', 'CS201', 3, 1, 'Introduction to data structures and algorithms'),
('Database Systems', 'CS301', 4, 1, 'Relational database design and SQL'),
('Calculus I', 'MATH101', 3, 2, 'Introduction to differential calculus'),
('.NET Programming', 'CS401', 3, 1, 'Backend development with .NET');

INSERT INTO Instructor (FirstName, LastName, Email, Phone, DepartmentID) VALUES
('Dr. Khaled', 'Ibrahim', 'khaled.ibrahim@uni.edu', '01023456789', 1),
('Dr. Mona', 'Samy', 'mona.samy@uni.edu', '01087654321', 2);

INSERT INTO Enrollment (StudentID, CourseID, Semester, AcademicYear, Grade) VALUES
(1, 1, 'Fall', 2024, 85.5),
(1, 2, 'Fall', 2024, 90.0),
(2, 1, 'Fall', 2024, 78.0),
(3, 3, 'Fall', 2024, 88.5);

INSERT INTO CourseInstructor (CourseID, InstructorID, Semester, AcademicYear) VALUES
(1, 1, 'Fall', 2024),
(2, 1, 'Fall', 2024),
(3, 2, 'Fall', 2024);

GO


