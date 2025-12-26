using System;
using System.Data;
using System.Data.SqlClient;

namespace StudentManagementSystem.DataAccess
{
    public class DepartmentDAO
    {
        public DataTable GetAllDepartments()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT DepartmentID, DepartmentName, DepartmentCode FROM Department ORDER BY DepartmentName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving departments: " + ex.Message);
            }
        }
        public bool AddDepartment(string departmentName, string departmentCode)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "INSERT INTO Department (DepartmentName, DepartmentCode) VALUES (@Name, @Code)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", departmentName);
                    cmd.Parameters.AddWithValue("@Code", departmentCode);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding department: " + ex.Message);
            }
        }

        // Update department
        public bool UpdateDepartment(int departmentID, string departmentName, string departmentCode)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"UPDATE Department 
                                     SET DepartmentName = @Name, DepartmentCode = @Code
                                     WHERE DepartmentID = @ID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", departmentName);
                    cmd.Parameters.AddWithValue("@Code", departmentCode);
                    cmd.Parameters.AddWithValue("@ID", departmentID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating department: " + ex.Message);
            }
        }

        // Delete department
        public bool DeleteDepartment(int departmentID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "DELETE FROM Department WHERE DepartmentID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", departmentID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting department: " + ex.Message);
            }
        }
    }
}
