using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pizzacase_server
{
    internal class DataDecryptor
    {
            // Internal AES decryption logic
            public static string DecryptData(string base64Cipher, string key)
            {
                byte[] cipherBytes = Convert.FromBase64String(base64Cipher);
                using Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32));
                aes.IV = new byte[16];
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }


    }
