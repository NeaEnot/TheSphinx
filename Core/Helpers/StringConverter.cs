using System;
using System.Text;

namespace TheSphinx.Core.Helpers
{
    internal static class StringConverter
    {
        private static Encoding encoding = Encoding.Unicode;

        internal static string TransformEncode(string str)
        {
            byte[] bytes = encoding.GetBytes(str);
            return TransformEncode(bytes);
        }

        internal static string TransformEncode(byte[] bytes)
        {
            string answer = Convert.ToBase64String(bytes);
            return answer;
        }

        internal static string TransformDecode(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return TransformDecode(bytes);
        }

        internal static string TransformDecode(byte[] bytes)
        {
            string answer = encoding.GetString(bytes);
            return answer;
        }

        internal static byte[] GetBytes(string str)
        {
            byte[] answer = encoding.GetBytes(str);
            return answer;
        }

        internal static string GetString(byte[] bytes)
        {
            string answer = encoding.GetString(bytes);
            return answer;
        }
    }
}
