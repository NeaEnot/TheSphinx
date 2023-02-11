using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CryptoTest
{
    internal class Program
    {
        static Encoding encoding = Encoding.Latin1;
        static CipherMode cipherMode = CipherMode.CBC;
        static PaddingMode paddingMode = PaddingMode.Zeros;

        static void Main(string[] args)
        {
            string password = "test";
            string text = "asdasdas oado awhfayig daosdfiq -0471 3u4108 3108 43129 384 1eo2e201873 1 02173 `0238 17 4123i 80779 wq80723 132 87 ds";
            Console.WriteLine($"Original: {text}");

            string encoded = Encrypt(text, password);
            Console.WriteLine($"Encoded: {encoded}");

            File.WriteAllText("test.dat", encoded);
            string readedEnc = File.ReadAllText("test.dat");

            string decoded = Decrypt(readedEnc, password);
            Console.WriteLine($"Decoded: {decoded}");
        }

        private static byte[] GetKey(string key)
        {
            using SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(encoding.GetBytes(key));
        }

        static string Encrypt(string text, string password)
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

            string answer = encoding.GetString(encrypted.Concat(aes.IV).ToArray());

            return answer;
        }

        static string Decrypt(string text, string password)
        {
            byte[] bytes = encoding.GetBytes(text);

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
    }
}
