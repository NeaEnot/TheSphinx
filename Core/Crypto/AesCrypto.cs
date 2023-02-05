using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TheSphinx.Core.Helpers;

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

        public string Encrypt(string text, string password)
        {
            Aes aes = Aes.Create();

            aes.GenerateIV();

            aes.Key = TransformPasswordToKey(password);
            byte[] encrypted;
            ICryptoTransform crypt = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] bytes = StringConverter.GetBytes(text);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                }

                encrypted = ms.ToArray();
            }

            string answer = StringConverter.TransformEncode(encrypted.Concat(aes.IV).ToArray());

            return answer;
        }

        public string Decrypt(string text, string password)
        {
            string base64encoded = StringConverter.TransformDecode(text);
            byte[] bytes = StringConverter.GetBytes(base64encoded);

            byte[] bytesIv = new byte[16];
            byte[] data = new byte[bytes.Length - 16];

            for (int i = bytes.Length - 16, j = 0; i < bytes.Length; i++, j++)
                bytesIv[j] = bytes[i];

            for (int i = 0; i < bytes.Length - 16; i++)
                data[i] = bytes[i];

            Aes aes = Aes.Create();
            aes.Key = TransformPasswordToKey(password);
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

                    return StringConverter.GetString(decrypted.ToArray());
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
