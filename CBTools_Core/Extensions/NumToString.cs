using System;
using System.Collections.Generic;

namespace CBTools_Core.Extensions {
    public static class NumToString {
        public static string SpellOut(this int num) => ((long)num).SpellOut();

        public static string SpellOut(this long num) {
            bool negative = num < 0;
            num = Math.Abs(num);

            if (num < 10)
                return Negative(OneDigit((byte)num).Item1, negative);

            string numString = num.ToString();
            byte[] nums = new byte[numString.Length];

            for (int i = 0; i < nums.Length; i++) {
                nums[i] = byte.Parse(numString[i].ToString());
            }

            byte[] digits = new byte[3];
            long x = 0;

            List<(string words, byte length)> segments = new List<(string, byte)>();

            for (int i = nums.Length - 1; i >= 0; i--) {
                digits[x] = nums[i];
                if (x == 2 || i == 0) {
                    x = 0;
                    segments.Add(ThreeDigits(digits[2], digits[1], digits[0]));
                    digits[0] = digits[1] = digits[2] = 0;
                }
                else {
                    x += 1;
                }
            }

            string s = segments[0].length > 0 ? segments[0].words : "";
            if (segments.Count > 1) {
                for (int i = 1; i < segments.Count; i++) {
                    s = (segments[i].length > 0 ? segments[i].words + " " + (i == 1 ? "thousand" : powers[i - 2] + "illion") : "")
                        + ((i == 1 && segments[i - 1].length < 3 && segments[i - 1].length > 0) ? " and " : ((s.Length > 0 && s[0] == ' ') ? "" : " "))
                        + s;
                }
            }
            return Negative(s, negative);
        }

        private static string Negative(string s, bool isNegative) => (isNegative ? "negative " : "") + s;

        private static (string, byte) OneDigit(byte digit) => (ones[digit], digit > 0 ? (byte)1 : (byte)0);

        private static (string, byte) TwoDigits(byte n1, byte n2) {
            if (n1 == 0) {
                return OneDigit(n2);
            }
            else if (n1 == 1) {
                if (n2 < 3)
                    return (prefixes[n2], 2);
                else
                    return (prefixes[n2] + "teen", 2);
            }
            else if (n2 == 0) {
                return (Tens(n1), 2);
            }
            else {
                return (Tens(n1) + " " + OneDigit(n2).Item1, 2);
            }
        }

        private static string Tens(byte n) => n == 2 ? "twenty" : prefixes[n] + "ty";

        private static (string, byte) ThreeDigits(byte n1, byte n2, byte n3) {
            if (n1 == 0)
                return TwoDigits(n2, n3);
            else if (n2 == 0 && n3 == 0)
                return (OneDigit(n1).Item1 + " hundred", 3);
            else
                return (OneDigit(n1).Item1 + " hundred and " + TwoDigits(n2, n3).Item1, 3);
        }

        private static readonly string[] ones =
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };
        private static readonly string[] prefixes =
        {
            "ten",
            "eleven",
            "twelve",
            "thir",
            "four",
            "fif",
            "six",
            "seven",
            "eigh",
            "nine"
        };
        private static readonly string[] powers =
        {
            "m",
            "b",
            "tr",
            "quadr",
            "qulong",
            "sext",
            "sept",
            //"oct",
            //"non",
            //"dec"
        };
    }
}
