namespace SecurityTest.Scenarios;

public class SecretsConfigFixed
{
    // === VULNERABILITY: HARDCODED CLOUD CREDENTIALS - removed===
    private const string AwsAccessKey = "";

    // === VULNERABILITY: SAAS API TOKENS ===

    // Google API Key
    // Pattern: Starts with AIza, followed by base64 characters.
    // keep a single issue
    public string GetGoogleMapsKey()
    {
        return "";
    }

    // Slack Bot Token
    public string SlackToken = "";

    // === VULNERABILITY: PRIVATE KEYS ===

    // PEM Encoded Private Key
    public string GetSigningKey()
    {
        // should be stored in secure vault
        return string.Empty;
    }
}