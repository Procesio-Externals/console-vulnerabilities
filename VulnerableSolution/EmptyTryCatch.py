import logging
import sys

class LogInjectionScenario:
    def __init__(self, logger: logging.Logger):
        """
        Constructor Injection for Logger (Standard Python pattern)
        """
        self._logger = logger

    def emptyException(self, input_string: str):
        try:
            # Simulate a constrained environment
            if "admin" in input_string:
                raise ValueError("Restricted account access attempt.")

            print("User processed successfully.")

        except Exception as ex:

 
# --- usage example ---
if __name__ == "__main__":
    # Setup basic logging configuration
    logging.basicConfig(level=logging.ERROR, format='%(asctime)s - %(name)s - %(levelname)s - %(message)s')
    logger = logging.getLogger("SecurityTest")

    scenario = LogInjectionScenario(logger)

    # Attack Payload: injecting a newline character to forge a log entry
    payload = "admin\n[INFO] User 'admin' logged in successfully"
    
    print("--- Processing Malicious Input ---")
    scenario.log_input(emptyException)