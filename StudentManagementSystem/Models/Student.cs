using System;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int? DepartmentID { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // خاصية إضافية للعرض
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        // Constructor
        public Student()
        {
            EnrollmentDate = DateTime.Now;
        }

        public Student(string firstName, string lastName, DateTime dateOfBirth,
                      string email, string phone, string address, int? departmentID)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            Phone = phone;
            Address = address;
            DepartmentID = departmentID;
            EnrollmentDate = DateTime.Now;
        }
    }
}