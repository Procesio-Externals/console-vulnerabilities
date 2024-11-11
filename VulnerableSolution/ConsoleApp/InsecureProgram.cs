
using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp
{
    class InsecureProgram
    {
        // Hardcoded sensitive information
        static string ApiSecret = "SuperSecretAPIKey";

        static void Main(string[] args)
        {
            Console.WriteLine("Insecure Application Running");

            // Weak cryptographic practice: Using MD5 for hashing
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(ApiSecret);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                Console.WriteLine("MD5 Hash: " + BitConverter.ToString(hashBytes).Replace("-", ""));
            }
        }
    }
}
