﻿using System.Text;

namespace VulnerableSolution
{
    public class Token
    {
        private static readonly string _secret = "Cmy$uper$ecretOver99999";

        public static string Generate(string username)
        {
            //This token generation doesn't provide security.
            //The token has no expiration date and can be used anytime
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            string token = $"{username}:{timestamp}:{_secret}";

            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            string base64Token = Convert.ToBase64String(tokenBytes);

            return base64Token;
        }
    }
}
