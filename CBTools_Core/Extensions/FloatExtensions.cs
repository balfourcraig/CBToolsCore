using CBTools_Core.Untyped;
using System;
using System.Runtime.CompilerServices;

namespace CBTools_Core.Extensions {
    public static class FloatExtensions {
        //Float
        public static bool IsEven(this float x) => (x % 2) == 0;

        public static bool AsBool(this float x) => x != 0;

        public static float Circular(this float num, float max, float min = 0) {
            float diff = (max - min) + 1;
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

        public static bool ApproxEquals(this float x, float y, float threshold = 0.00001f) => Math.Abs(x - y) < threshold;

        public static int ReinterpretAsInt(this float n) => new Size4(n).asInt;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float FastInverseSquareRoot(float number) {
            float x2 = number * 0.5f;

            int i = 0x5f375a86 - ((new Size4(number).asInt) >> 1);//Evil bit level hacking ;)

            number = new Size4(i).asFloat;//Abusing StructLayout to "safely" reinterpret float-int

            return number * (1.5f - (x2 * number * number));
        }

        /// <summary>
        /// Rounds to the nearest integer, rather than just truncating
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int RoundToInt(this float num) => (int)(num + 0.5f);

        /// <summary>
        /// Bias numbers towards small values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="bias">0 is linear, closer to 1 will favour low numbers</param>
        /// <returns></returns>
        public static float Bias(this float x, float bias) {
            //adjust input to make control feel more linear
            double k = Math.Pow(1 - bias, 3);
            return (float)((x * k) / (x * k - x + 1));
        }


        //Double
        //public static unsafe long ReinterpretAsLong(this double n) => *(long*)&n;
        public static long ReinterpretAsLong(this double n) => new Size8(n).asLong;

        public static bool IsEven(this double x) => (x % 2) == 0;

        public static bool AsBool(this double x) => x != 0;

        public static double Circular(this double num, double max, double min = 0) {
            
            
            double diff = (max - min) + 1;
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

        public static bool ApproxEquals(this double x, double y, double threshold = 0.00001) => Math.Abs(x - y) < threshold;

        /// <summary>
        /// Rounds to the nearest integer, rather than just truncating
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int RoundToInt(this double num) => (int)(num + 0.5);
    }
}
