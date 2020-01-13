using CBTools_Core.Untyped;
using System;
using System.Collections.Generic;

namespace CBTools_Core.Extensions {
    public static partial class IntExtensions {
        public static string ToBinaryString(this ulong num) {
            char[] chars = new char[64];
            for (int i = 0; i < 64; i++) {
                chars[chars.Length - 1 - i] = (char)(((num >> i) & 0b_1) + '0');
            }
            return new string(chars);
        }

        public static string ToBinaryString(this ulong num, char delimeter) {
            char[] chars = new char[80];
            for (int i = 0, inner = 0; inner < 64; i++, inner++) {
                if (inner > 0 && inner % 4 == 0)
                    chars[chars.Length - 1 - i++] = delimeter;
                chars[chars.Length - 1 - i] = (char)(((num >> inner) & 0b_1) + '0');
            }
            chars[0] = delimeter;
            return "0b" + new string(chars);
        }



        //TODO: Fix this horrible mess
        public static int Circular(this int num, int max, int min) {
            int diff = (max - min) + 1;
            if (num < min) {
                while (num < min)
                    num += diff;
            }
            else {
                while (num > max)
                    num -= diff;
            }
            return num;
        }

        //public static int Circular(this int num, int max)
        //{

        //}

        public static IEnumerable<int> To(this int start, int end, int step = 1) {
            step = Math.Abs(step);
            if (end < start) {
                for (int i = start; i >= end; i -= step)
                    yield return i;
            }
            else {
                for (int i = start; i <= end; i += step)
                    yield return i;
            }
        }

        public static int GreatestCommonDivisor(this int x, int y) {
            while (x != 0) {
                int temp = x;
                x = y % x;
                y = temp;
            }
            return Math.Abs(y);
        }

        public static bool IsPowerOfTwo(this int num) {
            int bitCount = 0;
            for (int i = 0; i < 31; i++) {
                bitCount += num & 1;
                if (bitCount > 1) break;
                num >>= i;
            }
            return bitCount == 1;
        }

        public static bool IsPowerOfTwo(this uint num) {
            int bitCount = 0;
            for (int i = 0; i < 32; i++) {
                bitCount += (int)num & 1;
                if (bitCount > 1) break;
                num >>= i;
            }
            return bitCount == 1;
        }

        public static bool IsPowerOfTwo(this long num) {
            int bitCount = 0;
            for (int i = 0; i < 63; i++) {
                bitCount += (int)num & 1;
                if (bitCount > 1) break;
                num >>= i;
            }
            return bitCount == 1;
        }

        public static bool IsPowerOfTwo(this ulong num) {
            int bitCount = 0;
            for (int i = 0; i < 64; i++) {
                bitCount += (int)num & 1;
                if (bitCount > 1) break;
                num >>= i;
            }
            return bitCount == 1;
        }

        //public static unsafe float ReinterpretAsFloat(this int n) => *(float*)&n;
        /// <summary>
        /// Reinterpret without casting. If you don't know what this is doing, DO NOT USE IT! Uses Size4 struct behind the scenes, so if you call this more than once, make one of those.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static float ReinterpretAsFloat(this int n) => new Size4(n).asFloat;

        //public static unsafe double ReinterpretAsDouble(this long n) => *(double*)&n;
        /// <summary>
        /// Reinterpret without casting. If you don't know what this is doing, DO NOT USE IT! Uses Size8 struct behind the scenes, so if you call this more than once, make one of those.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double ReinterpretAsDouble(this long n) => new Size8(n).asDouble;

        /// <summary>
        /// Count how many bits are set on a number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int BitCount(this int num) {
            int count = (num >> 31) & 1;
            num &= 0x_7fff_ffff;

            while (num != 0) {
                count += num & 1;
                num >>= 1;
            }
            return count;
        }

        /// <summary>
        /// Count how many bits are set on a 64 bit number. Be aware of how casting negative numbers to different widths affects the bits
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static long BitCountLong(this long num) {
            long count = (num >> 63) & 1;
            num &= 0x_7fff_ffff_ffff_ffff;

            while (num != 0) {
                count += num & 1;
                num >>= 1;
            }
            return count;
        }


    }
}
