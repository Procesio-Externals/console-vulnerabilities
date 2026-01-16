package com.example.vulnerableapp;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class App {
    private static final Logger logger = LogManager.getLogger(App.class);

    public static void main(String[] args) {
        // This line utilizes the vulnerable library
        logger.info("Application starting...");
        
        // In a real attack scenario, logging a malicious string here could trigger RCE
        logger.error("This application is running with a vulnerable version of Log4j.");
    }
}