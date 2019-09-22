using System;
using System.Collections.Generic;
using System.Text;

namespace CBTools_Core.Extensions
{
    public static partial class BoolExtensions
    {
        /// <summary>
        /// Returns 1 for true, 0 for false as float.
        /// Unless you specifically need a float, use AsInt
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float AsFloat(this bool x) => x ? 1f : 0f;

        /// <summary>
        /// Returns 1 for true, 0 for false
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int AsInt(this bool b) => b ? 1 : 0;
    }
}
