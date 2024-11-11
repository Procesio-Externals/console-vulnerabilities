using System.Security.Cryptography;
using System.Text;

namespace VulnerableSolution
{
    public class Encryption
    {
        public static void Execute()
        {
            // Generate the RSA key pair with 512-bit key size
            // key is to short for production use. 2048-bit key size is recommended
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(512))
            {
                try
                {
                    // Export the public key and private key
                    string publicKey = rsa.ToXmlString(false);  // Public key only
                    string privateKey = rsa.ToXmlString(true);  // Public and private keys

                    Console.WriteLine("Public Key: \n" + publicKey);
                    Console.WriteLine("Private Key: \n" + privateKey);

                    // Message to encrypt
                    string originalMessage = "This is a test message";
                    Console.WriteLine("Original Message: " + originalMessage);

                    // Encrypt the message using the public key
                    byte[] encryptedMessage = EncryptMessage(originalMessage, rsa);

                    // Decrypt the message using the private key
                    string decryptedMessage = DecryptMessage(encryptedMessage, rsa);

                    // Output the encrypted and decrypted messages
                    Console.WriteLine("\nEncrypted Message (Base64): " + Convert.ToBase64String(encryptedMessage));
                    Console.WriteLine("\nDecrypted Message: " + decryptedMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static byte[] EncryptMessage(string message, RSACryptoServiceProvider rsa)
        {
            // Convert the message to a byte array
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            // Encrypt the message with the public key
            byte[] encryptedMessage = rsa.Encrypt(messageBytes, false); // false for OAEP padding (deprecated in RSA)

            return encryptedMessage;
        }

        static string DecryptMessage(byte[] encryptedMessage, RSACryptoServiceProvider rsa)
        {
            // Decrypt the message using the private key
            byte[] decryptedBytes = rsa.Decrypt(encryptedMessage, false); // false for OAEP padding (deprecated in RSA)

            // Convert the decrypted byte array to a string
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
