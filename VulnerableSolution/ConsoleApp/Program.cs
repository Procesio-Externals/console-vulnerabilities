
using System;

namespace ConsoleApp
{
    class Program
    {
        // Hardcoded Secret Vulnerability: API key hardcoded in the code
        static string ApiKey = "12345-ABCDE";

        static void Main(string[] args)
        {
            Console.WriteLine("Running Console App with hardcoded API key: " + ApiKey);
        }
    }
}
