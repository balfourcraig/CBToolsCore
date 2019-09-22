using CBTools_Core.Untyped;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBTools_Core.Extensions
{
    public static partial class IntExtensions
    {
        //TODO: Fix this horrible mess
        public static int Circular(this int num, int max, int min)
        { 
            int diff = (max - min) + 1;
            if (num < min)
            {
                while (num < min)
                    num += diff;
            }
            else
            {
                while (num > max)
                    num -= diff;
            }
            return num;
        }

        //public static int Circular(this int num, int max)
        //{

        //}

        public static bool IsPowerOfTwo(this int num)
        {
            int bitCount = 0;
            for (int i = 0; i < 31; i++)
            {
                bitCount += num & 1;
                if (bitCount > 1) break;
                num >>= i;
            }
            return bitCount == 1;
        }

        public static bool IsPowerOfTwo(this uint num)
        {
            int bitCount = 0;
            for (int i = 0; i < 32; i++)
            {
                bitCount += (int)num & 1;
                if (bitCount > 1) break;
                num >>= i;
            }
            return bitCount == 1;
        }

        public static bool IsPowerOfTwo(this long num)
        {
            int bitCount = 0;
            for (int i = 0; i < 63; i++)
            {
                bitCount += (int)num & 1;
                if (bitCount > 1) break;
                num >>= i;
            }
            return bitCount == 1;
        }

        public static bool IsPowerOfTwo(this ulong num)
        {
            int bitCount = 0;
            for (int i = 0; i < 64; i++)
            {
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
        public static int BitCount(this int num)
        {
            int count = (num >> 63) & 1;
            num &= 0x_7fff_ffff;

            while (num != 0)
            {
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
        public static long BitCountLong(this long num)
        {
            long count = (num >> 63) & 1;
            num &= 0x_7fff_ffff_ffff_ffff;

            while (num != 0)
            {
                count += num & 1;
                num >>= 1;
            }
            return count;
        }
    }
}
