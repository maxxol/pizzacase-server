using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pizzacase_server
{
    internal class DataEncryptor
    {
        public static string EncryptData(string data, string key)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32));
            aes.IV = new byte[16]; // zero IV for simplicity

            byte[] plainBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = aes.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }
    }
}