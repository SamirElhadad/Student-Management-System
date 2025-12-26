using System;
using System.Data.SqlClient;

namespace StudentManagementSystem.DataAccess
{
    public class DatabaseConnection
    {
        // Connection String 
<<<<<<< HEAD
        private static string connectionString = @"Data Source=.;Initial Catalog=StudentManagementDB;Integrated Security=True";
=======
        private static string connectionString = @"Data Source=ABDULRAHMAN\SQLEXPRESS;Initial Catalog=StudentManagementDB;Integrated Security=True";
>>>>>>> e1c2ce8a95b78b95291b5d344fc9abbe07919459

        // Connection
        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting to database: " + ex.Message);
            }
        }

        // Test Connection
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}