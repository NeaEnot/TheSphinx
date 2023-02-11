using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TheSphinx.Core.Crypto
{
    internal class AesCrypto : ICrypto
    {
        private static Encoding encoding = Encoding.Latin1;
        private static CipherMode cipherMode = CipherMode.CBC;
        private static PaddingMode paddingMode = PaddingMode.Zeros;

        public string Encrypt(string text, string password)
        {
            Aes aes = Aes.Create();
            aes.Mode = cipherMode;
            aes.Padding = paddingMode;

            aes.GenerateIV();

            aes.Key = GetKey(password);
            byte[] encrypted;
            ICryptoTransform crypt = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] bytes = encoding.GetBytes(text);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                }

                encrypted = ms.ToArray();
            }

            string answer = Convert.ToBase64String(encrypted.Concat(aes.IV).ToArray());

            return answer;
        }

        public string Decrypt(string text, string password)
        {
            byte[] bytes = Convert.FromBase64String(text);

            byte[] bytesIv = new byte[16];
            byte[] data = new byte[bytes.Length - 16];

            for (int i = bytes.Length - 16, j = 0; i < bytes.Length; i++, j++)
                bytesIv[j] = bytes[i];

            for (int i = 0; i < bytes.Length - 16; i++)
                data[i] = bytes[i];

            Aes aes = Aes.Create();
            aes.Mode = cipherMode;
            aes.Padding = paddingMode;

            aes.Key = GetKey(password);
            aes.IV = bytesIv;

            ICryptoTransform crypt = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(data))
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Read))
                {
                    int b;
                    List<byte> decrypted = new List<byte>();

                    while ((b = cs.ReadByte()) != -1)
                        decrypted.Add((byte)b);

                    return encoding.GetString(decrypted.ToArray());
                }
            }
        }

        private byte[] GetKey(string key)
        {
            using SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(encoding.GetBytes(key));
        }
    }
}
