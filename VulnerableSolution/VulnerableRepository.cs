using Microsoft.Data.SqlClient;
using System.Runtime.Serialization;

namespace DataAccess
{
    public class VulnerableRepository
    {
        // SQL Injection vulnerability via unparameterized query
        public void GetUserDetails(string userId)
        {
            string query = "SELECT * FROM Users WHERE UserID = '" + userId + "'";
            using (SqlConnection conn = new SqlConnection("YourConnectionStringHere"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                // Process data...
            }
        }

        // Insecure Deserialization 
        public void DeserializeData(string serializedData)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(object));
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(serializedData)))
            {
                // Untrusted data deserialization can be dangerous
                var obj = serializer.ReadObject(stream);
            }
        }
    }
}
