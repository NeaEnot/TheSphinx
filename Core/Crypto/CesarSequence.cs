using System;
using System.Text;

namespace TheSphinx.Core.Crypto
{
    internal class CesarSequence : ICrypto
    {
        private static Encoding encoding = Encoding.Unicode;

        private char foundation;

        internal CesarSequence(char foundation)
        {
            this.foundation = foundation;
        }

        public string Encrypt(string text, string password)
        {
            string answer = "";
            int k = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char t = text[i];
                char p = password[k];

                char a = ' ';

                if (i % 2 == 0)
                    a = (char)(foundation + t - p);
                else
                    a = (char)(foundation - t + p);

                answer += a;

                k++;
                if (k == password.Length)
                    k = 0;
            }

            byte[] bytes = encoding.GetBytes(answer);
            return Convert.ToBase64String(bytes);
        }

        public string Decrypt(string text, string password)
        {
            string answer = "";

            byte[] bytes = Convert.FromBase64String(text);
            string str = encoding.GetString(bytes);
            int k = 0;

            for (int i = 0; i < str.Length; i++)
            {
                char t = str[i];
                char p = password[k];

                char a = ' ';

                if (i % 2 == 0)
                    a = (char)(t - foundation + p);
                else
                    a = (char)(foundation - t + p);

                answer += a;

                k++;
                if (k == password.Length)
                    k = 0;
            }

            return answer;
        }
    }
}
