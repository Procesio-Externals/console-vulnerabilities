
using System.Data.SqlClient;

namespace DataAccess
{
    public class DataRepository
    {
        // SQL Injection Vulnerability: Unparameterized query
        public void GetUser(string userId)
        {
            string connectionString = "YourConnectionStringHere";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserId = '" + userId + "'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                // Process data...
            }
        }
    }
}
