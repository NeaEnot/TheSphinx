using System.Text;

namespace TheSphinx.Core.Crypto
{
    internal class CesarSequence : ICrypto
    {
        private char foundation;

        internal CesarSequence(char foundation)
        {
            this.foundation = foundation;
        }

        public byte[] Encrypt(byte[] bytes, string password)
        {
            string answer = "";

            string text = Encoding.Unicode.GetString(bytes);
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

            return Encoding.Unicode.GetBytes(answer);
        }

        public byte[] Decrypt(byte[] bytes, string password)
        {
            string answer = "";

            string text = Encoding.Unicode.GetString(bytes);
            int k = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char t = text[i];
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

            return Encoding.Unicode.GetBytes(answer);
        }
    }
}
