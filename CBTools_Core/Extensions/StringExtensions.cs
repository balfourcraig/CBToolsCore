using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CBTools_Core.Extensions {
    public static class StringExtensions {
        internal readonly static char[] ends = new char[]
        {
            '.',
            '!',
            '?',
            ':',
            '/',
        };

        internal readonly static char[] pauses = new char[]
        {
            ';',
            ',',
        };

        internal readonly static char[] vowels = new char[]
        {
            'A','E','I','O','U'
        };


        public static int VowelCount(this string s) => s.ToUpperInvariant().Count(x => vowels.Contains(x));

        public static string Shuffle(this string s) => new string(s.ToCharArray().Shuffle());

        public static string Alphabetize(this string s) => new string(s.ToCharArray().OrderBy(x => x).ToArray());

        public static string StripWhitespace(this string s) => s.CleanString(x => !x.IsWhiteSpace());

        //public static string StripNonLetters(this string s) => new string(s.Where(x => x.IsLetter()).ToArray());
        public static string StripNonLetters(this string s) => s.CleanString(x => !char.IsLetter(x));

        public static string FirstWord(this string s) {
            int startIndex = 0;
            while (s[startIndex].IsWhiteSpace())
                startIndex++;

            int firstWordLength = s.IndexOf(' ', startIndex);//TODO: Test this well, does it work, or does Trim() work better?
            if (firstWordLength == -1)
                firstWordLength = s.IndexOfAny(ends, startIndex);
            if (firstWordLength == -1)
                firstWordLength = s.IndexOfAny(pauses, startIndex);
            if (firstWordLength == -1)
                firstWordLength = s.Length;

            return s.Substring(startIndex, firstWordLength + startIndex);
        }

        public static string LastWord(this string s) {
            string lineTrimmed = s.Trim();

            if (!lineTrimmed.Last().IsAlphanumeric())
                lineTrimmed = lineTrimmed.Substring(0, lineTrimmed.Length - 1);

            int endIndex = lineTrimmed.LastIndexOf(' ');
            if (endIndex == -1)
                endIndex = lineTrimmed.LastIndexOfAny(ends);
            if (endIndex == -1)
                endIndex = lineTrimmed.LastIndexOfAny(pauses);

            return lineTrimmed.Substring(endIndex + 1, lineTrimmed.Length - (endIndex + 1));
        }

        public static string PadBothSides(this string str, in int numberToAddEachSide, char paddingChar = ' ') => str.PadLeft(str.Length + numberToAddEachSide, paddingChar).PadRight(str.Length + (numberToAddEachSide * 2), paddingChar);

        public static string Capitalize(this string s) {
            if (s == null) {
                throw new ArgumentNullException("String is null");
            }
            else if (s.Length == 0) {
                return s;
            }
            else {
                unsafe {
                    fixed (char* c = string.Copy(s)) {
                        c[0] = char.ToUpperInvariant(c[0]);
                        return new string(c);
                    }
                }
            }
        }

        public static string CapitalizeAllSentences(this string str) {
            if (string.IsNullOrWhiteSpace(str)) {
                return str;
            }
            else if (str.Length == 1) {
                return str.ToUpperInvariant();
            }
            else {
                char[] letters = new char[str.Length];

                letters[0] = letters[0].ToUpper();

                for (int i = 1; i < letters.Length; i++) {
                    if (ends.Contains(letters[i - 1]))
                        letters[i] = letters[i].ToUpper();
                }
                return new string(letters);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CleanString(this string s, Predicate<char> predicate) {
            if (s == null)
                throw new ArgumentNullException("String is null");

            Span<char> letters = stackalloc char[s.Length];
            int count = 0;
            for (int i = 0; i < s.Length; i++) {
                if (predicate(s[i]))
                    letters[count++] = s[i];
            }
            return new string(letters.Slice(0, count));
        }
    }
}
