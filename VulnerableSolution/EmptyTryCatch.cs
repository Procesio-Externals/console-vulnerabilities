namespace VulnerableSolution
{
    public class EmptyTryCatch
    {
        private ILogger logger;

        // Constructor Injection for Logger (Standard Spring/Java EE pattern)
        public EmptyTryCatch(ILogger logger)
        {
            this.logger = logger;
        }

        public void emptyTryCatch(String inputString)
        {
            try
            {
                throw new Exception("Restricted account access attempt.");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
