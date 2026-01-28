import logging
import sys

class LogInjectionScenario:
    def __init__(self, logger: logging.Logger):
        """
        Constructor Injection for Logger (Standard Python pattern)
        """
        self._logger = logger

    def log_input(self, input_string: str):
        try:
            # Simulate a constrained environment
            if "admin" in input_string:
                raise ValueError("Restricted account access attempt.")

            print("User processed successfully.")

        except Exception as ex:
            # === VULNERABILITY 1: Logging Injection ===
            #
            # INCORRECT USAGE:
            # Using f-strings (f"...") constructs the final string BEFORE passing it to the logger.
            # This treats user input as part of the log template, allowing them to inject 
            # new lines (\n) or spoof log entries.
            #
            # SAFE USAGE WOULD BE (Lazy Evaluation): 
            # self._logger.error("[Security Alert] Input error: %s. Reason: %s", input_string, str(ex))
            self._logger.error(f"[Security Alert] Input error: {input_string}. Reason: {str(ex)}")

            # === VULNERABILITY 2: Console Injection ===
            # Standard Output sink often piped to log aggregators (Splunk, ELK).
            # If input_string contains newline characters, it creates fake log entries.
            print(f"[ERROR] Manual trace: {input_string}")

            # === VULNERABILITY 3: File Injection ===
            # Direct file write allowing complete log corruption.
            self._log_to_file(f"Error on input: {input_string}", "app.log")

    def _log_to_file(self, message: str, file_path: str):
        """
        Appending raw strings to a file
        """
        try:
            with open(file_path, "a", encoding="utf-8") as f:
                f.write(message + "\n")
        except IOError as e:
            print(f"Failed to write to file: {e}")

# --- usage example ---
if __name__ == "__main__":
    # Setup basic logging configuration
    logging.basicConfig(level=logging.ERROR, format='%(asctime)s - %(name)s - %(levelname)s - %(message)s')
    logger = logging.getLogger("SecurityTest")

    scenario = LogInjectionScenario(logger)

    # Attack Payload: injecting a newline character to forge a log entry
    payload = "admin\n[INFO] User 'admin' logged in successfully"
    
    print("--- Processing Malicious Input ---")
    scenario.log_input(payload)