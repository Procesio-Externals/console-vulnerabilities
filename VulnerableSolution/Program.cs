// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;
using VulnerableSolution.EndlessDepLoop;

// Hardcoded sensitive information
string ApiSecret = "SuperSecretAPIKey";

Encrypt(ApiSecret);
Encrypt("easyKey");

// Start an endless loop
StartEndlessLoop();

static void Encrypt(string apiSecret)
{
    Console.WriteLine("Insecure Application Running");

    // Weak cryptographic practice: Using MD5 for hashing
    using (MD5 md5 = MD5.Create())
    {
        byte[] inputBytes = Encoding.ASCII.GetBytes(apiSecret);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        Console.WriteLine("MD5 Hash: " + BitConverter.ToString(hashBytes).Replace("-", ""));
    }
}


static void StartEndlessLoop()
{
    var loopCode = new LoopManager();
}