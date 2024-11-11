
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VulnerableController : ControllerBase
    {
        // SQL Injection Vulnerability: User input is directly concatenated into the SQL command
        [HttpGet("get-user")]
        public IActionResult GetUser(string username)
        {
            string connectionString = "YourConnectionStringHere";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                // Process data...
            }
            return Ok();
        }
    }
}
