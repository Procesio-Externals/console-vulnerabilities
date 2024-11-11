
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsecureController : ControllerBase
    {
        // SQL Injection Vulnerability
        [HttpGet("get-item")]
        public IActionResult GetItem(string itemId)
        {
            // Vulnerable SQL Query
            string query = "SELECT * FROM Items WHERE ItemID = '" + itemId + "'";
            // Potential SQL Injection through direct concatenation
            using (SqlConnection conn = new SqlConnection("YourConnectionStringHere"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                // Read data...
            }
            return Ok();
        }

        // XSS Vulnerability
        [HttpGet("display-comment")]
        public IActionResult DisplayComment(string comment)
        {
            // Vulnerable to XSS due to lack of output encoding
            return Content("<div>" + comment + "</div>");
        }

        // CSRF Vulnerability: Sensitive Action without CSRF Protection
        [HttpPost("transfer-funds")]
        public IActionResult TransferFunds(string account, decimal amount)
        {
            // Sensitive action prone to CSRF if unprotected
            // Process transfer...
            return Ok();
        }
    }
}
