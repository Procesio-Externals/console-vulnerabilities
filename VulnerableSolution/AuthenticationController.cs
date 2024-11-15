using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using VulnerableSolution.Models;

namespace VulnerableSolution
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet()]
        public IActionResult LogIn(
            [FromHeader] string userName,
            [FromHeader] string password)
        {
            string connectionString = "YourConnectionStringHere";
            UserModel currentUser = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Username, Email, Password FROM Users WHERE Username = '" + userName + "'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    currentUser = new UserModel()
                    {
                        UserName = reader.GetString(reader.GetOrdinal("Username")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Password = reader.GetString(reader.GetOrdinal("Password"))
                    };
                }
                // Process data...
            }
            if(currentUser == null)
            {
                return BadRequest("Invalid username");
            }

            if (!currentUser.Password.Equals(password))
            {
                return BadRequest("Invalid password");
            }

            var token = Token.Generate(userName);
            return Ok(new { token });
        }
    }
}
