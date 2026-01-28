namespace SecurityTest.Scenarios;

public class SecretsConfig
{
    // === VULNERABILITY: HARDCODED CLOUD CREDENTIALS ===
    // AWS Access Key ID
    // Pattern: Starts with AKIA, 20 chars alphanumeric.
    // Detected by: Gitleaks, SecretScan, GitHub, Trivy
    private const string AwsAccessKey = "";

    // AWS Secret Key
    // Pattern: High entropy string often appearing near "Secret" or "Key"
    private const string AwsSecretKey = "";

    // === VULNERABILITY: SAAS API TOKENS ===

    // Google API Key
    // Pattern: Starts with AIza, followed by base64 characters.
    public string GetGoogleMapsKey()
    {
        return "";
    }

    // === VULNERABILITY: DATABASE CREDENTIALS ===
        // JDBC/Connection String
    // Pattern: Looks for "postgres://" or "mysql://" containing a password structure (:password@).
    private string _connectionString = "postgres://admin:SuperSecureP@ssw0rd!@localhost:5432/mydb";

    // === VULNERABILITY: PRIVATE KEYS ===

    // PEM Encoded Private Key
    // Pattern: "-----BEGIN RSA PRIVATE KEY-----" block
    // This is a "Generic" secret that almost every tool catches.
    public string GetSigningKey()
    {
        return @"";
    }
}