using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSphinx.Core.Helpers
{
    internal static class IdHelper
    {
        private static string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        internal static string currentId;

        internal static void ToNextId()
        {
            string id = "";

            /* Надо ли увеличивать символ текущей позиции.
             * Так как начинаем смотреть с последней буквы, 
             * которую всегда надо увеличивать, инициализируем как true.*/
            bool isNeedIncrement = true;

            for (int i = currentId.Length - 1; i >= 0; i--)
            {
                if (isNeedIncrement)
                {
                    if (currentId[i] == alphabet[alphabet.Length - 1])
                    {
                        id = alphabet[0] + id;
                    }
                    else
                    {
                        int charIndex = alphabet.IndexOf(currentId[i]);
                        id = alphabet[charIndex + 1] + id;
                        isNeedIncrement = false;
                    }
                }
                else
                {
                    id = currentId[i] + id;
                }
            }

            if (isNeedIncrement)
                id = alphabet[0] + id;

            currentId = id;
        }
    }
}
