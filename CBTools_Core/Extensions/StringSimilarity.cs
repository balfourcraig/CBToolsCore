using System;
using System.Collections.Generic;
using System.Text;

namespace CBTools_Core.Extensions
{
    public static class StringSimilarity
    {
        private readonly struct LetterPair : IEquatable<LetterPair>
        {
            public readonly char c1;
            public readonly char c2;

            public LetterPair(char c1, char c2)
            {
                this.c1 = c1;
                this.c2 = c2;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is LetterPair))
                    return false;
                else
                    return ((LetterPair)obj).c1 == c1 && ((LetterPair)obj).c2 == c2;
            }

            public static bool operator ==(LetterPair p1, LetterPair p2)
            {
                return p1.c1 == p2.c1 && p1.c2 == p2.c2;
            }

            public static bool operator !=(LetterPair p1, LetterPair p2)
            {
                return !(p1 == p2);
            }

            public override int GetHashCode()
            {
                return c1 | (c2 << 8);//TODO: this is probably not a great hash code, but nothing uses it... yet
                //return base.GetHashCode();
            }

            public bool Equals(LetterPair other)
            {
                return other.c1 == c1 && other.c2 == c2;
            }
        }

        public static float Similarity(this string s, string str, bool caseSensitive = false) => CompareStrings(s, str, caseSensitive);

        public static float CompareStrings(string str1, string str2, bool caseSensitive)
        {
            if (string.IsNullOrWhiteSpace(str1) || string.IsNullOrWhiteSpace(str2))
                return 0;

            Span<LetterPair> pairs1 = GetAllPairs(caseSensitive ? str1 : str1.ToUpperInvariant());
            Span<LetterPair> pairs2 = GetAllPairs(caseSensitive ? str2 : str2.ToUpperInvariant());

            if (pairs1.Length == 0 || pairs2.Length == 0)
                return 0;
            else
            {
                bool[] usedPairs = new bool[pairs2.Length];
                int intersection = 0;

                int union = pairs1.Length + pairs2.Length;

                for (int i = 0; i < pairs1.Length; i++)
                {
                    LetterPair pair1 = pairs1[i];
                    for (int j = 0; j < pairs2.Length; j++)
                    {
                        if (!usedPairs[j] && pair1 == pairs2[j])
                        {
                            intersection++;
                            usedPairs[j] = true;
                            break;
                        }
                    }
                }
                return (2f * intersection) / union;
            }

            static Span<LetterPair> GetAllPairs(string s) {
                Span<LetterPair> pairs = new LetterPair[s.Length - 1];//cannot stackalloc here, use stackalloc if inlining
                int pos = 0;
                for (int i = 0; i < s.Length - 1; i++) {
                    if (s[i] != ' ' && s[i + 1] != ' ')
                        pairs[pos++] = new LetterPair(s[i], s[i + 1]);
                }
                return pairs.Slice(0, pos);
            }
        }


        /// <summary>
        /// Returns the longest common unbroken substring of two strings
        /// </summary>
        /// <param name="s"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static int LongestSubstring(this string s, string other) {
            int m = s.Length;
            int n = other.Length;

            Span<int> curr = stackalloc int[n + 1];
            Span<int> prev = stackalloc int[n + 1];
            int result = 0;

            for (int i = 1; i <= m; i++) {
                char c = s[i - 1];
                for (int j = 1; j <= n; j++) {
                    if (c == other[j - 1]) {
                        curr[j] = prev[j - 1] + 1;
                        result = Math.Max(result, curr[j]);
                    }
                }
                prev = curr;
                curr = stackalloc int[n + 1];
            }
            return result;
        }

    }
}
