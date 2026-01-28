using System.Text;

namespace SecurityTest.Scenarios;
public class LegacyBankingConnector
{
    // === ISSUE 1: HARDCODED SECRETS ===

    // Detection 1: High Entropy String / Known Pattern
    // The '' prefix is a common pattern for Stripe keys, which most tools flag immediately.
    private const string ApiKey = "";

    // Detection 2: Connection String Keyword Matching
    // Tools look for keywords like 'Password=', 'User ID=', or 'pwd=' in static strings.
    private const string DbConnection = "Server=192.168.1.10;Database=BankDB;User ID=admin;Password=SuperSecretPass123!";

    public async Task<string> ProcessTransaction(string transactionData)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Using the hardcoded secret
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

                var content = new StringContent(transactionData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.legacybank.com/v1/process", content);

                return await response.Content.ReadAsStringAsync();
            }
        }
        // === ISSUE 2: POOR EXCEPTION HANDLING (Swallowing) ===
        // Detection: Empty Catch Block / Generic Exception Capture
        // Catches distinct system errors and fails to log or rethrow them (CWE-391).
        catch (Exception)
        {
            // Silently failing allows attackers to probe the system without alerting monitoring tools.
            return null;
        }
    }

    public void ConnectToDatabase()
    {
        try
        {
            Console.WriteLine($"Connecting to DB with string: {DbConnection}");
            throw new InvalidOperationException("DB connection failed.");
        }
        // === ISSUE 3: POOR EXCEPTION HANDLING (Info Leakage) ===
        // Detection: Stack Trace Exposure
        // Printing the raw stack trace exposes internal file paths and logic (CWE-209).
        catch (Exception ex)
        {
            Console.WriteLine("CRITICAL ERROR: " + ex.ToString());
        }
    }
}
