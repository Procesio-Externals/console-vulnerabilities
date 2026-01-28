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

    public void emptyTryCatch(String inputString) {
        try {
            // Simulate a constrained environment
            if (inputString.contains("admin")) {
                throw new SecurityException("Restricted account access attempt.");
            }
        } catch (Exception ex) {
        }
    }
}