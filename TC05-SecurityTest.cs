using System;
using System.Data.SqlClient;

namespace CodeAnalysisTests
{
    public class SecurityTests
    {
        public void GetUserData(string userName)
        {
            string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // VIOLATION: User input is directly concatenated into the SQL command string.
                // An attacker could pass input like: " ' OR '1'='1 "
                string sqlQuery = "SELECT * FROM Users WHERE UserName = '" + userName + "'";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    // Process data...
                }
            }
        }
    }
}