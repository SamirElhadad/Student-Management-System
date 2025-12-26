using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using StudentManagementSystem.Models;
namespace StudentManagementSystem.DataAccess
{
    public class StudentDAO
    {
        public bool AddStudent(Student student)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"INSERT INTO Student (FirstName, LastName, DateOfBirth, Email, Phone, Address, DepartmentID, EnrollmentDate)
                                   VALUES (@FirstName, @LastName, @DateOfBirth, @Email, @Phone, @Address, @DepartmentID, @EnrollmentDate)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", student.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", student.Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", student.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DepartmentID", student.DepartmentID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding student: " + ex.Message);
            }
        }
        public bool UpdateStudent(Student student)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"UPDATE Student 
                                   SET FirstName = @FirstName, 
                                       LastName = @LastName, 
                                       DateOfBirth = @DateOfBirth, 
                                       Email = @Email, 
                                       Phone = @Phone, 
                                       Address = @Address, 
                                       DepartmentID = @DepartmentID
                                   WHERE StudentID = @StudentID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", student.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", student.Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", student.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DepartmentID", student.DepartmentID ?? (object)DBNull.Value);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating student: " + ex.Message);
            }
        }

        // deleting a student
        public bool DeleteStudent(int studentID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "DELETE FROM Student WHERE StudentID = @StudentID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting student: " + ex.Message);
            }
        }

        // get all students
        public DataTable GetAllStudents()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT s.StudentID, s.FirstName, s.LastName, s.DateOfBirth, 
                                          s.Email, s.Phone, s.Address, s.EnrollmentDate,
                                          d.DepartmentName
                                   FROM Student s
                                   LEFT JOIN Department d ON s.DepartmentID = d.DepartmentID
                                   ORDER BY s.StudentID DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving students: " + ex.Message);
            }
        }

        // get student by ID
        public Student GetStudentByID(int studentID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT * FROM Student WHERE StudentID = @StudentID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Student student = new Student
                        {
                            StudentID = (int)reader["StudentID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                            Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : null,
                            Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
                            DepartmentID = reader["DepartmentID"] != DBNull.Value ? (int?)reader["DepartmentID"] : null,
                            EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                        };
                        return student;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving student: " + ex.Message);
            }
        }

        // search students
        public DataTable SearchStudents(string searchText)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT s.StudentID, s.FirstName, s.LastName, s.DateOfBirth, 
                                          s.Email, s.Phone, s.Address, s.EnrollmentDate,
                                          d.DepartmentName
                                   FROM Student s
                                   LEFT JOIN Department d ON s.DepartmentID = d.DepartmentID
                                   WHERE s.FirstName LIKE @Search 
                                      OR s.LastName LIKE @Search 
                                      OR s.Email LIKE @Search
                                      OR s.Phone LIKE @Search
                                   ORDER BY s.StudentID DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@Search", "%" + searchText + "%");
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching students: " + ex.Message);
            }
        }
    }
}