using System.Text;
using TheSphinx.Core.Helpers;

namespace TheSphinx.Core.Crypto
{
    internal class CesarSequence : ICrypto
    {
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

            return StringConverter.TransformEncode(answer);
        }

        public string Decrypt(string text, string password)
        {
            string answer = "";

            string str = StringConverter.TransformDecode(text);
            int k = 0;

            for (int i = 0; i < str.Length; i++)
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

            return answer;
        }
    }
}
