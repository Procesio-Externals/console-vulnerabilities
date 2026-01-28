using System.Security.Cryptography;
using System.Text;

namespace SecurityTest.Scenarios;

public class EnterpriseConfigLoader
{
    public void LoadConfig()
    {
        Console.WriteLine("Loading public configuration...");   // The public surface looks clean.
    }

    // LEVEL 1: Private Internal Class
    private class InternalInfrastructure
    {
        // LEVEL 2: Nested Helper Class
        internal class LegacyAuthHandler
        {
            // === VULNERABILITY 1: Hidden Insecure Property (Hardcoded Secret) ===
            // This property is nested 3 levels deep.
            // Horusec/Gitleaks patterns should catch "AWS_SECRET_KEY".
            public string MasterKey
            {
                get { return "AKIAIOSFODNN7EXAMPLE"; }
            }

            // === VULNERABILITY 2: Insecure Member Usage (Weak Crypto) ===
            // Using MD5 is banned, but here it is hidden inside a property getter 
            // within a nested class.
            public byte[] WeakHash
            {
                get
                {
                    // The tool must parse the body of this getter deep in the nest
                    // to see the instantiation of MD5.
                    using (var md5 = MD5.Create())
                    {
                        return md5.ComputeHash(Encoding.UTF8.GetBytes(MasterKey));
                    }
                }
            }

            public void Authenticate()
            {
                Console.WriteLine($"Auth trace: {BitConverter.ToString(WeakHash)}");
            }
        }
    }
}