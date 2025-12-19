using System;
using System.Data.SqlClient;

namespace StudentManagementSystem.DataAccess
{
    public class DatabaseConnection
    {
        // Connection String 
        private static string connectionString = @"Data Source=SAMIR\SQLEXPRESS;Initial Catalog=StudentManagementDB;Integrated Security=True";

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