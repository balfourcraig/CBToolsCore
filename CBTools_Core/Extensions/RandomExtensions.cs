using System;

namespace CBTools_Core.Extensions {
    public static class RandomExtensions {


        /// <summary>
        /// Generates a pair of random numbers with Gausian distribution by Box-Muller transformation
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="xMean">Mean of the x dimension</param>
        /// <param name="yMean">Mean of the y dimension</param>
        /// <param name="xStdDev">Standard deviation of x dimension</param>
        /// <param name="yStdDev">Standard deviation of y dimension</param>
        /// <returns></returns>
        public static (double x, double y) NextGausianPair(this Random rand, double xMean, double yMean, double xStdDev, double yStdDev) {
            double sq = Math.Sqrt(-2.0 * Math.Log(1.0 - rand.NextDouble()));
            double u2 = (1.0 - rand.NextDouble()) * 2.0 * Math.PI;
            double randStdNormalX = sq * Math.Sin(u2);
            double randStdNormalY = sq * Math.Cos(u2);
            double x = xMean + xStdDev * randStdNormalX;
            double y = yMean + yStdDev * randStdNormalY;
            return (x, y);
        }


        //public static ColorRGBA RandomColor(this Random rand, byte lightness) {
        //    int light3 = lightness * 3;
        //    byte[] arr = new byte[3];
        //    arr[0] = (byte)(rand.Next(0, light3) / 3);
        //    arr[1] = (byte)(rand.Next(0, light3 - arr[0]) / 3);
        //    arr[2] = (byte)(rand.Next(0, light3 - arr[0] - arr[1]) / 3);
        //    arr.ShuffleInplace();
        //    return new ColorRGBA(arr[0], arr[1], arr[2]);
        //}

        /// <summary>
        /// Generates a pair of gausian distributed random points, with mean = 0 and std dev = 1
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        public static (double x, double y) NextGausianPair(this Random rand) => rand.NextGausianPair(0, 0, 1, 1);

        /// <summary>
        /// Generates a pair of gausian distributed random points, with a given mean and std dev = 1
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="mean">Mean of both points</param>
        /// <returns></returns>
        public static (double x, double y) NextGausianPair(this Random rand, double mean) => rand.NextGausianPair(mean, mean, 1, 1);

        /// <summary>
        /// Generates a pair of gausian distributed random points, with a given mean and std dev
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="mean">Mean of both points</param>
        /// <param name="stdDev">Std dev of both points</param>
        /// <returns></returns>
        public static (double x, double y) NextGausianPair(this Random rand, double mean, double stdDev) => rand.NextGausianPair(mean, mean, stdDev, stdDev);

        /// <summary>
        /// Random true/false. Just does rand.NextDouble() >= 0.5
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        public static bool NextBool(this Random rand) => rand.NextDouble() >= 0.5;

        /// <summary>
        /// Gets a random char from a-z
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="upperCaseChance">0-1 chance for an upper case letter. 1 for all caps, 0 for all lower etc</param>
        /// <returns></returns>
        public static char NextLetter(this Random rand, double upperCaseChance = 0.5)
            => rand.NextDouble() <= upperCaseChance ? (char)rand.Next('A', 'Z' + 1) : (char)rand.Next('a', 'z' + 1);


        /// <summary>
        /// Generate a random string with given length bounds
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="minLength">Inclusive min length</param>
        /// <param name="maxLength">Exclusive upper length</param>
        /// <param name="upperCaseChance"></param>
        /// <returns></returns>
        public static string NextString(this Random rand, int minLength, int maxLength, double upperCaseChance = 0.5) {
            int length = rand.Next(minLength, maxLength);
            char[] letters = new char[length];
            for (int i = 0; i < length; i++) {
                letters[i] = rand.NextLetter(upperCaseChance);
            }
            return new string(letters);
        }

        public static float NextFloat(this Random rand, float min, float max) {
            float diff = max - min;
            return (float)rand.NextDouble() * diff + min;
        }

        public static double NextDouble(this Random rand, double min, double max) {
            double diff = max - min;
            return rand.NextDouble() * diff + min;
        }
    }
}
