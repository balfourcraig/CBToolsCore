using System;

namespace CBTools_Core {
    public class PhiRandom {
        private readonly static Random seedMaker = new Random();

        private const double goldenRatio = 0.6180339887498948482045868343656381;//This is actually 1/phi but modding to 1 it's exactly the same result. May get an extra bit of accuracy this way too, idk

        private double state;
        private readonly double noise;

        /// <summary>
        /// Sequence will begin at a random starting point
        /// </summary>
        /// <param name="noise">Amount of noise to be added from System.Random. A value of about 0.1 works well for most cases</param>
        public PhiRandom(double noise = 0.1) : this(noise, seedMaker.NextDouble()) {
        }

        /// <summary>
        /// Sequence will begin at a fixed place
        /// </summary>
        /// <param name="noise">Amount of noise to be added from System.Random. A value of about 0.1 works well for most cases</param>
        /// <param name="startValue">Where the sequence should begin</param>
        public PhiRandom(double noise, double startValue) {
            state = startValue;
            this.noise = noise;
        }

        private double Sample() {
            state = (state + goldenRatio) % 1.0;

            return noise == 0 ? state : (state + (seedMaker.NextDouble() * noise)) % 1;
        }

        /// <summary>
        /// Returns the next double between 0-1 in the current sequence
        /// </summary>
        /// <returns></returns>
        public double Next() => Sample();

        /// <summary>
        /// Returns the next double between 0-1 in the current sequence
        /// </summary>
        /// <param name="max">Multiply result by</param>
        /// <returns></returns>
        public double Next(double max) => Sample() * max;

        /// <summary>
        /// Returns the next double between 0-1 in the current sequence
        /// </summary>
        /// <param name="min">result shifted by this much</param>
        /// <param name="max">result multiplied by diff max-min</param>
        /// <returns></returns>
        public double Next(double min, double max) => Sample() * (max - min) + min;

        /// <summary>
        /// Just floors and casts the result of double Next
        /// </summary>
        /// <param name="min">Inclusive lower bound</param>
        /// <param name="max">Exclusive upper bound</param>
        /// <returns></returns>
        public int NextInt(int max) => (int)Math.Floor(Next(max));

        /// <summary>
        /// Just floors and casts the result of double Next
        /// </summary>
        /// <param name="min">Inclusive lower bound</param>
        /// <param name="max">Exclusive upper bound</param>
        /// <returns></returns>
        public int NextInt(int min, int max) => (int)Math.Floor(Next(min, max));

        /// <summary>
        /// Evenly distributed boolean in sequence.
        /// </summary>
        /// <returns></returns>
        public bool NextBool() => Next(100) % 2 == 0;//Kinda funky check here. Should be a little less predictable though

        /// <summary>
        /// Rearranges an array using golden ratio positioning. Modifies the original array!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public void ShuffleInplace<T>(T[] array) {
            for (int n = array.Length - 1; n > 0; --n) {
                int r = NextInt(0, n);
                T temp = array[r];
                array[r] = array[n - 1];
                array[n - 1] = temp;
            }
            //return array;
        }

        public (double x, double y) NextGausianPair(double xMean, double yMean, double xStdDev, double yStdDev) {
            double sq = Math.Sqrt(-2.0 * Math.Log(1.0 - Next()));
            double u2 = (1.0 - Next()) * 2.0 * Math.PI;
            double randStdNormalX = sq * Math.Sin(u2);
            double randStdNormalY = sq * Math.Cos(u2);
            double x = xMean + xStdDev * randStdNormalX;
            double y = yMean + yStdDev * randStdNormalY;
            return (x, y);
        }
    }
}
