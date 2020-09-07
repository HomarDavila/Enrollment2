using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Extensions
{
    public static class StringExtension
    {
        public static string Truncate(this string value, int length = 20)
        {
            if (value.IsNullOrEmpty()) return value;
            if (value.Length >= length)
            {
                return value.Substring(0, length);
            }
            else return value;
        }
        public static string RemoveWhiteSpaces(this string dt)
        {
            return dt.Replace(" ", "");
        }
        public static bool IsNullOrEmpty(this string dt)
        {
            return string.IsNullOrEmpty(dt);
        }
    }
}
