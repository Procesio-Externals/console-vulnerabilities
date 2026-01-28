package securitytest.scenarios;

import org.slf4j.Logger;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.nio.file.StandardOpenOption;

public class LogInjectionScenario {

    private final Logger logger;

    // Constructor Injection for Logger (Standard Spring/Java EE pattern)
    public LogInjectionScenario(Logger logger) {
        this.logger = logger;
    }

    public void logInput(String inputString) {
        try {
            // Simulate a constrained environment
            if (inputString.contains("admin")) {
                throw new SecurityException("Restricted account access attempt.");
            }

            System.out.println("User processed successfully.");

        } catch (Exception ex) {
            // === VULNERABILITY 1: Logger Injection ===
            //
            // INCORRECT USAGE:
            // Using String Concatenation (+) constructs the final string BEFORE 
            // passing it to the logger.
            // This treats user input as part of the log message structure, allowing 
            // injection of new lines or format specifiers.
            //
            // SAFE USAGE WOULD BE (Parameterized Logging): 
            // logger.error("[Security Alert] Input error: {}. Reason: {}", inputString, ex.getMessage());
            logger.error("[Security Alert] Input error: " + inputString + ". Reason: " + ex.getMessage());

            // === VULNERABILITY 2: Console Injection ===
            // Standard Output sink often piped to log aggregators (Splunk, ELK).
            System.out.println("[ERROR] Manual trace: " + inputString);

            // === VULNERABILITY 3: File Injection ===
            // Direct file write allowing complete log corruption.
            logToFile("Error on input: " + inputString, "app.log");
        }
    }

    private void logToFile(String message, String filePath) {
        try {
            // Appending raw strings to a file
            Files.writeString(
                Paths.get(filePath), 
                message + System.lineSeparator(), 
                StandardOpenOption.CREATE, 
                StandardOpenOption.APPEND
            );
        } catch (IOException e) {
            System.err.println("Failed to write to file: " + e.getMessage());
        }
    }
}