// Microsoft.AspNetCore.Mvc.Core 2.2.5 is deprecated
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using VulnerableSolution;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VulnerableController : ControllerBase
    {
        // SQL Injection Vulnerability: User input is directly concatenated into the SQL command
        [HttpGet("get-user")]
        public IActionResult GetUser(
            [FromHeader] string token, //username should be taken from token not body payload
            string username)
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

        // library used considers default hash algorithm as insecure and outdated
        [HttpGet("encrypt")]
        public IActionResult Encrypt(string text)
        {
            return Ok(Encryption.Encrypt(text));
        }

        //opening file connection without closing it can lead to resource exhaustion
        //common type of resource leak vulnerability
        [HttpGet("read-file")]
        public IActionResult ReadFile(string fileName)
        {
            // Path validation is also missing, allowing potential path traversal attacks
            string filePath = Path.Combine("files", fileName);

            // Opens a file stream but does not close it
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fileStream);

            // Reads the file content
            string content = reader.ReadToEnd(); // File remains open even after reading

            // Returns the file content as plain text
            return Content(content, "text/plain");
        }

        //Leaving a database connection open without closing it is a common vulnerability that can lead to connection leaks
        [HttpGet("get-data")]
        public IActionResult GetData()
        {
            // Vulnerable: Database connection is not closed
            SqlConnection connection = new SqlConnection("your-connection-string-here");
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
            SqlDataReader reader = command.ExecuteReader();

            List<string> users = new List<string>();
            while (reader.Read())
            {
                users.Add(reader["Username"].ToString());
            }

            // Connection remains open here!
            return Ok(users);
        }
    }
}
