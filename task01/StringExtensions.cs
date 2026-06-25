using System;
using System.Text;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            string lowerStr = str.ToLower();

            StringBuilder cleanedBuilder = new StringBuilder();
            foreach (char c in lowerStr)
            {
                if (char.IsLetterOrDigit(c))
                {
                    cleanedBuilder.Append(c);
                }
            }

            string cleaned = cleanedBuilder.ToString();

            if (cleaned.Length == 0)
                return false;

            char[] charArray = cleaned.ToCharArray();
            Array.Reverse(charArray);
            string reversed = new string(charArray);

            return cleaned == reversed;
        }
    }
}