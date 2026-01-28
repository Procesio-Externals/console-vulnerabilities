namespace SecurityTest.Scenarios;
public class LogInjectionScenario
{
    private readonly ILogger<LogInjectionScenario> _logger;

    // Constructor Injection for ILogger (Standard .NET pattern)
    public LogInjectionScenario(ILogger<LogInjectionScenario> logger)
    {
        _logger = logger;
    }

    public void LogInput(string inputString)
    {
        try
        {
            // Simulate a constrained environment
            if (inputString.Contains("admin"))
            {
                throw new InvalidDataException("Restricted account access attempt.");
            }

            Console.WriteLine("User processed successfully.");
        }
        catch (Exception ex)
        {
            // === VULNERABILITY 1: ILogger Injection ===
            //
            // INCORRECT USAGE:
            // Using string interpolation ($"...") constructs the final string BEFORE passing it to the logger.
            // This treats user input as part of the log template, allowing them to inject new lines or format strings.
            //
            // SAFE USAGE WOULD BE: _logger.LogError(ex, "Login failed for User: {Username}", username);
            _logger.LogError($"[Security Alert] Input error: {inputString}. Reason: {ex.Message}");

            // === VULNERABILITY 2: Console Injection ===
            // Standard Output sink often piped to log aggregators (Splunk, ELK).
            Console.WriteLine($"[ERROR] Manual trace: {inputString}");

            // === VULNERABILITY 3: File Injection ===
            // Direct file write allowing complete log corruption.
            LogToFile($"Error on input: {inputString}", "app.log");
        }
    }

    private void LogToFile(string message, string filePath)
    {
        // Appending raw strings to a file
        File.AppendAllText(filePath, message + Environment.NewLine);
    }
}
