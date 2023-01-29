using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TheSphinx.Core.Crypto
{
    internal class AesCrypto : ICrypto
    {
        private readonly int keyLength = 256;

        private Dictionary<(byte, byte), byte> bytesMapping;

        public AesCrypto()
        {
            bytesMapping = new Dictionary<(byte, byte), byte>();

            byte result = 0x0;

            for (byte i = 0x0; i < byte.MaxValue; i++)
            {
                for (byte j = 0x0; j < byte.MaxValue; j++)
                {
                    bytesMapping.Add((i, j), result);

                    if (result < byte.MaxValue)
                        result++;
                    else
                        result = 0x0;
                }
            }
        }

        public byte[] Encrypt(byte[] bytes, string password)
        {
            Aes aes = Aes.Create();
            aes.Padding = PaddingMode.PKCS7;

            aes.GenerateIV();

            aes.Key = TransformPasswordToKey(password);
            byte[] encrypted;
            ICryptoTransform crypt = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                }

                encrypted = ms.ToArray();
            }

            return encrypted.Concat(aes.IV).ToArray();
        }

        public byte[] Decrypt(byte[] bytes, string password)
        {
            byte[] bytesIv = new byte[16];
            byte[] mess = new byte[bytes.Length - 16];

            for (int i = bytes.Length - 16, j = 0; i < bytes.Length; i++, j++)
                bytesIv[j] = bytes[i];

            for (int i = 0; i < bytes.Length - 16; i++)
                mess[i] = bytes[i];

            Aes aes = Aes.Create();
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = TransformPasswordToKey(password);
            aes.IV = bytesIv;

            byte[] data = mess;
            ICryptoTransform crypt = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(data))
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Read))
                {
                    int b;
                    List<byte> decrypted = new List<byte>();

                    while ((b = cs.ReadByte()) != -1)
                        decrypted.Add((byte)b);

                    return decrypted.ToArray();
                }
            }
        }

        private byte[] TransformPasswordToKey(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            while (bytes.Length > keyLength / 8)
            {
                List<byte> next = new List<byte>();

                for (int i = 0; i < bytes.Length; i += 2)
                {
                    if (i < bytes.Length - 2)
                        next.Add(bytesMapping[(bytes[i], bytes[i + 1])]);
                    else
                        next.Add(bytes[i]);
                }

                bytes = next.ToArray();
            }

            int diff = keyLength / 8 - bytes.Length;

            if (diff > 0)
            {
                List<byte> list = new List<byte>(bytes);
                for (int i = 0; i < diff; i++)
                    list.Add(list[i]);

                bytes = list.ToArray();
            }

            return bytes;
        }
    }
}
