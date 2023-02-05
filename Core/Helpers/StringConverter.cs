using System;
using System.Text;

namespace TheSphinx.Core.Helpers
{
    internal static class StringConverter
    {
        internal static string Base64Encode(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        internal static string Base64Decode(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }

        internal static byte[] GetBytes(string str)
        {
            string base64 = Base64Encode(str);
            return Encoding.UTF8.GetBytes(base64);
        }

        internal static string GetString(byte[] bytes)
        {
            string base64 = Encoding.UTF8.GetString(bytes);
            return Base64Decode(base64);
        }
    }
}
