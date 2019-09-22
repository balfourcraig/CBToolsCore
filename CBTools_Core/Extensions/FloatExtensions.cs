using CBTools_Core.Untyped;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CBTools_Core.Extensions
{
    public static class FloatExtensions
    {
        //Float
        public static bool IsEven(this float x) => (x % 2) == 0;

        public static bool AsBool(this float x) => x != 0;

        public static float Circular(this float num, float max, float min = 0)
        {
            float diff = (max - min) + 1;
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

        public static int ReinterpretAsInt(this float n) => new Size4(n).asInt;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float FastInverseSquareRoot(float number)
        {
            float x2 = number * 0.5f;

            int i = 0x5f375a86 - ((new Size4(number).asInt) >> 1);//Evil bit level hacking ;)

            number = new Size4(i).asFloat;//Abusing StructLayout to "safely" reinterpret float-int

            return number * (1.5f - (x2 * number * number));
        }


        //Double
        //public static unsafe long ReinterpretAsLong(this double n) => *(long*)&n;
        public static long ReinterpretAsLong(this double n) => new Size8(n).asLong;

        public static bool IsEven(this double x) => (x % 2) == 0;

        public static bool AsBool(this double x) => x != 0;

        public static double Circular(this double num, double max, double min = 0)
        {
            double diff = (max - min) + 1;
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
    }
}
