using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBTools_Core.Extensions
{
    public static class CharExtensions
    {
        public static bool IsUpper(this char c) => char.IsUpper(c);

        public static bool IsLower(this char c) => char.IsLower(c);

        public static bool IsLetter(this char c) => char.IsLetter(c);

        public static bool IsAlphanumeric(this char c) => char.IsLetterOrDigit(c);

        public static bool IsNumeric(this char c) => char.IsNumber(c);

        public static bool IsWhiteSpace(this char c) => char.IsWhiteSpace(c);

        public static bool IsNullOrWhiteSpace(this char c) => c == '\0' || char.IsWhiteSpace(c);

        public static bool IsSentenceEnd(this char c) => StringExtensions.ends.Contains(c);

        public static char ToLower(this char c) => char.ToLower(c);

        public static char ToUpper(this char c) => char.ToUpper(c);

        public static char[] ToLower(this char[] c)
        {
            for (int i = 0; i < c.Length; i++)
                c[i] = c[i].ToLower();

            return c;
        }

        public static char[] ToUpper(this char[] c)
        {
            for (int i = 0; i < c.Length; i++)
                c[i] = c[i].ToUpper();

            return c;
        }
    }
}
