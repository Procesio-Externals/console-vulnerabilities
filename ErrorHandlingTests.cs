using System;
using System.IO;

namespace CodeAnalysisTests
{
    public class ErrorHandlingTests
    {
        public void ReadDataFromFile(string filePath)
        {
            try
            {
                // Attempt to open a file that might not exist
                string content = File.ReadAllText(filePath);
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                // VIOLATION: The exception is caught but neither logged nor rethrown.
                // This block is empty, silencing the error completely.
            }
        }
    }
}

