using System;

namespace CBTools_Core.Geometry {
    public static class CBShape {
        /// <summary>
        /// Generates points for a regular polygon at position x,y with its points on the circle of radius
        /// </summary>
        /// <param name="sides">Number of sides on the polygon</param>
        /// <param name="x">Origin X</param>
        /// <param name="y">Origin Y</param>
        /// <param name="radius">Radius of circle where points fall</param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static (double, double)[] RegularPolygon(int sides = 3, double x = 0, double y = 0, double radius = 1, double rotation = 0) {
            if (sides <= 2)
                throw new ArgumentException("A polygon must have at least 3 sides");

            double a = Math.PI * 2.0 / sides;
            var points = new (double, double)[sides];
            for (int i = 0; i < sides; i++) {
                double angle = i * a + rotation;
                points[i] = (Math.Sin(angle) * radius + x, Math.Cos(angle) * radius + y);
            }
            return points;
        }

        /// <summary>
        /// Same as RegularPolygon, but using floats. Still uses Math (double) where required
        /// </summary>
        /// <param name="sides">Number of sides on the polygon</param>
        /// <param name="x">Origin X</param>
        /// <param name="y">Origin Y</param>
        /// <param name="radius">Radius of circle where points fall</param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static (float, float)[] RegularPolygonF(int sides = 3, float x = 0, float y = 0, float radius = 1, float rotation = 0) {
            if (sides <= 2)
                throw new ArgumentException("A polygon must have at least 3 sides");

            float a = (float)Math.PI * 2f / sides;
            var points = new (float, float)[sides];
            for (int i = 0; i < sides; i++) {
                float angle = i * a + rotation;
                points[i] = ((float)Math.Sin(angle) * radius + x, (float)Math.Cos(angle) * radius + y);
            }
            return points;
        }
    }
}
