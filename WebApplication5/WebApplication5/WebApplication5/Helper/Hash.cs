using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace api.Helper
{
    public static class Hash
    {
        public static string Encrypt(string password)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())            
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(password));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}