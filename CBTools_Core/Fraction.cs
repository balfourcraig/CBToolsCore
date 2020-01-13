using CBTools_Core.Extensions;
using System;
using System.Collections.Generic;

namespace CBTools_Core {
    /// <summary>
    /// Represents a fractional number as two integers
    /// </summary>
    public readonly struct Fraction : IEquatable<Fraction> {
        /// <summary>
        /// 0/1 Use this rather than 0/0 or you may hit div0 errors
        /// </summary>
        public static Fraction Zero => new Fraction(0, 1);

        /// <summary>
        /// Unit
        /// </summary>
        public static Fraction One => new Fraction(1, 1);

        /// <summary>
        /// Numerator. The "top" number 
        /// </summary>
        public readonly int num;

        /// <summary>
        /// Denominator. The "bottom" number
        /// </summary>
        public readonly int den;

        /// <summary>
        /// Approximate the value as a double.
        /// </summary>
        public double Real => (double)num / den;

        /// <summary>
        /// Flip the fraction. x/y -> y/x
        /// </summary>
        public Fraction Reciprocal => new Fraction(den, num);

        /// <summary>
        /// Returns true if the fraction represents a whole number
        /// </summary>
        public bool IsInteger => num % den == 0;

        /// <summary>
        /// Create a fraction
        /// </summary>
        /// <param name="num">Numerator, the top number</param>
        /// <param name="den">Denominator, the bottom number</param>
        public Fraction(int num, int den) {
            int absCorrect = 1;
            if (num < 0)
                absCorrect *= -1;
            if (den < 0)
                absCorrect *= -1;
            this.num = Math.Abs(num) * absCorrect;
            this.den = Math.Abs(den);
        }

        /// <summary>
        /// Create a whole number fraction. Denominator is set to 1 automatically
        /// </summary>
        /// <param name="num"></param>
        public Fraction(int num) {
            this.num = num;
            this.den = 1;
        }

        public static explicit operator double(Fraction frac) {
            return frac.Real;
        }

        public static implicit operator Fraction(int i) {
            return new Fraction(i);
        }

        public static explicit operator Fraction(double d) {
            if (lookupTable.TryGetValue(d, out Fraction f))
                return f;

            double num = d;
            int den = 1;

            int runs = 0;
            while (runs++ < 10 && (num % 1 > 0.1) && (den < int.MaxValue - (den * 10))) {
                num *= 10;
                den *= 10;
            }

            int gcd = den.GreatestCommonDivisor((int)num);
            return new Fraction((int)(num / gcd), den / gcd);
        }

        public override int GetHashCode() {//Not sure if this is a great hash code.
            int gcd = den.GreatestCommonDivisor(num);
            return (unchecked(3474701531 * (num / gcd) * (den / gcd))).GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj == null || !(obj is Fraction))
                return false;

            var frac = (Fraction)obj;

            int num1 = this.num * frac.den;
            int num2 = frac.num * this.den;

            return num1 == num2;
        }

        public bool Equals(Fraction other) => this == other;

        public static Fraction operator -(Fraction frac) {
            return new Fraction(frac.num * -1, frac.den);
        }

        public static Fraction operator ++(Fraction frac) {
            return new Fraction(frac.num + frac.den, frac.den);
        }

        public static Fraction operator --(Fraction frac) {
            return new Fraction(frac.num - frac.den, frac.den);
        }

        public static bool operator ==(Fraction lhs, Fraction rhs) {
            return lhs.num * rhs.den == rhs.num * lhs.den;
        }

        public static bool operator !=(Fraction lhs, Fraction rhs) {
            return lhs.num * rhs.den != rhs.num * lhs.den;
        }

        public static Fraction operator +(Fraction lhs, Fraction rhs) {
            int den = Math.Abs(lhs.den * rhs.den);
            int num = lhs.num * rhs.den + rhs.num * lhs.den;

            int gcd = num.GreatestCommonDivisor(den);
            return new Fraction(num / gcd, den / gcd);
        }

        public static Fraction operator -(Fraction lhs, Fraction rhs) {
            int den = Math.Abs(lhs.den * rhs.den);
            int num = lhs.num * rhs.den - rhs.num * lhs.den;

            int gcd = num.GreatestCommonDivisor(den);
            return new Fraction(num / gcd, den / gcd);
        }

        public static Fraction operator *(Fraction lhs, Fraction rhs) {
            int den = Math.Abs(lhs.den * rhs.den);
            int num = lhs.num * rhs.num;

            int gcd = num.GreatestCommonDivisor(den);
            return new Fraction(num / gcd, den / gcd);
        }

        public static Fraction operator /(Fraction lhs, Fraction rhs) {
            int den = Math.Abs(lhs.den * rhs.num);
            int num = lhs.num * rhs.den;

            int gcd = num.GreatestCommonDivisor(den);
            return new Fraction(num / gcd, den / gcd);
        }

        public static bool operator <(Fraction lhs, Fraction rhs) {
            return (lhs.num * rhs.den) < (rhs.num * lhs.den);
        }

        public static bool operator >(Fraction lhs, Fraction rhs) {
            return (lhs.num * rhs.den) > (rhs.num * lhs.den);
        }

        public static bool operator >=(Fraction lhs, Fraction rhs) {
            return (lhs.num * rhs.den) >= (rhs.num * lhs.den);
        }

        public static bool operator <=(Fraction lhs, Fraction rhs) {
            return (lhs.num * rhs.den) <= (rhs.num * lhs.den);
        }

        public override string ToString() {
            if (den == 1)
                return num.ToString();
            return num + "/" + den;
        }

        private static readonly Dictionary<double, Fraction> lookupTable = new Dictionary<double, Fraction>() {
            {1.0 / 2, new Fraction(1, 2)},
            {1.0 / 3, new Fraction(1, 3)},
            {1.0 / 4, new Fraction(1, 4)},
            {1.0 / 5, new Fraction(1, 5)},
            {1.0 / 6, new Fraction(1, 6)},
            {1.0 / 7, new Fraction(1, 7)},
            {1.0 / 8, new Fraction(1, 8)},
            {1.0 / 9, new Fraction(1, 9)},
            {1.0 / 10, new Fraction(1, 10)},
            {1.0 / 11, new Fraction(1, 11)},
            {1.0 / 12, new Fraction(1, 12)},
            {1.0 / 13, new Fraction(1, 13)},
            {1.0 / 14, new Fraction(1, 14)},
            {1.0 / 15, new Fraction(1, 15)},
            {1.0 / 16, new Fraction(1, 16)},
            {1.0 / 17, new Fraction(1, 17)},
            {1.0 / 18, new Fraction(1, 18)},
            {1.0 / 19, new Fraction(1, 19)},
            {1.0 / 20, new Fraction(1, 20)},
            {1.0 / 21, new Fraction(1, 21)},
            {1.0 / 22, new Fraction(1, 22)},
            {1.0 / 23, new Fraction(1, 23)},
            {1.0 / 24, new Fraction(1, 24)},
            {1.0 / 25, new Fraction(1, 25)},
            {1.0 / 26, new Fraction(1, 26)},
            {1.0 / 27, new Fraction(1, 27)},
            {1.0 / 28, new Fraction(1, 28)},
            {1.0 / 29, new Fraction(1, 29)},
            {1.0 / 30, new Fraction(1, 30)},
            {1.0 / 31, new Fraction(1, 31)},
            {1.0 / 32, new Fraction(1, 32)},
            {1.0 / 33, new Fraction(1, 33)},
            {1.0 / 34, new Fraction(1, 34)},
            {2.0 / 2, new Fraction(2, 2)},
            {2.0 / 3, new Fraction(2, 3)},
            {2.0 / 5, new Fraction(2, 5)},
            {2.0 / 7, new Fraction(2, 7)},
            {2.0 / 9, new Fraction(2, 9)},
            {2.0 / 11, new Fraction(2, 11)},
            {2.0 / 13, new Fraction(2, 13)},
            {2.0 / 15, new Fraction(2, 15)},
            {2.0 / 17, new Fraction(2, 17)},
            {2.0 / 19, new Fraction(2, 19)},
            {2.0 / 21, new Fraction(2, 21)},
            {2.0 / 23, new Fraction(2, 23)},
            {2.0 / 25, new Fraction(2, 25)},
            {2.0 / 27, new Fraction(2, 27)},
            {2.0 / 29, new Fraction(2, 29)},
            {2.0 / 31, new Fraction(2, 31)},
            {2.0 / 33, new Fraction(2, 33)},
            {3.0 / 2, new Fraction(3, 2)},
            {3.0 / 4, new Fraction(3, 4)},
            {3.0 / 5, new Fraction(3, 5)},
            {3.0 / 7, new Fraction(3, 7)},
            {3.0 / 8, new Fraction(3, 8)},
            {3.0 / 10, new Fraction(3, 10)},
            {3.0 / 11, new Fraction(3, 11)},
            {3.0 / 13, new Fraction(3, 13)},
            {3.0 / 14, new Fraction(3, 14)},
            {3.0 / 16, new Fraction(3, 16)},
            {3.0 / 17, new Fraction(3, 17)},
            {3.0 / 19, new Fraction(3, 19)},
            {3.0 / 20, new Fraction(3, 20)},
            {3.0 / 22, new Fraction(3, 22)},
            {3.0 / 23, new Fraction(3, 23)},
            {3.0 / 25, new Fraction(3, 25)},
            {3.0 / 26, new Fraction(3, 26)},
            {3.0 / 28, new Fraction(3, 28)},
            {3.0 / 29, new Fraction(3, 29)},
            {3.0 / 31, new Fraction(3, 31)},
            {3.0 / 32, new Fraction(3, 32)},
            {3.0 / 34, new Fraction(3, 34)},
            {4.0 / 2, new Fraction(4, 2)},
            {4.0 / 3, new Fraction(4, 3)},
            {4.0 / 5, new Fraction(4, 5)},
            {4.0 / 7, new Fraction(4, 7)},
            {4.0 / 9, new Fraction(4, 9)},
            {4.0 / 11, new Fraction(4, 11)},
            {4.0 / 13, new Fraction(4, 13)},
            {4.0 / 15, new Fraction(4, 15)},
            {4.0 / 17, new Fraction(4, 17)},
            {4.0 / 19, new Fraction(4, 19)},
            {4.0 / 21, new Fraction(4, 21)},
            {4.0 / 23, new Fraction(4, 23)},
            {4.0 / 25, new Fraction(4, 25)},
            {4.0 / 27, new Fraction(4, 27)},
            {4.0 / 29, new Fraction(4, 29)},
            {4.0 / 31, new Fraction(4, 31)},
            {4.0 / 33, new Fraction(4, 33)},
            {5.0 / 2, new Fraction(5, 2)},
            {5.0 / 3, new Fraction(5, 3)},
            {5.0 / 4, new Fraction(5, 4)},
            {5.0 / 6, new Fraction(5, 6)},
            {5.0 / 7, new Fraction(5, 7)},
            {5.0 / 8, new Fraction(5, 8)},
            {5.0 / 9, new Fraction(5, 9)},
            {5.0 / 11, new Fraction(5, 11)},
            {5.0 / 12, new Fraction(5, 12)},
            {5.0 / 13, new Fraction(5, 13)},
            {5.0 / 14, new Fraction(5, 14)},
            {5.0 / 16, new Fraction(5, 16)},
            {5.0 / 17, new Fraction(5, 17)},
            {5.0 / 18, new Fraction(5, 18)},
            {5.0 / 19, new Fraction(5, 19)},
            {5.0 / 21, new Fraction(5, 21)},
            {5.0 / 22, new Fraction(5, 22)},
            {5.0 / 23, new Fraction(5, 23)},
            {5.0 / 24, new Fraction(5, 24)},
            {5.0 / 26, new Fraction(5, 26)},
            {5.0 / 27, new Fraction(5, 27)},
            {5.0 / 28, new Fraction(5, 28)},
            {5.0 / 29, new Fraction(5, 29)},
            {5.0 / 31, new Fraction(5, 31)},
            {5.0 / 32, new Fraction(5, 32)},
            {5.0 / 33, new Fraction(5, 33)},
            {5.0 / 34, new Fraction(5, 34)},
            {6.0 / 2, new Fraction(6, 2)},
            {6.0 / 5, new Fraction(6, 5)},
            {6.0 / 7, new Fraction(6, 7)},
            {6.0 / 11, new Fraction(6, 11)},
            {6.0 / 13, new Fraction(6, 13)},
            {6.0 / 17, new Fraction(6, 17)},
            {6.0 / 19, new Fraction(6, 19)},
            {6.0 / 23, new Fraction(6, 23)},
            {6.0 / 25, new Fraction(6, 25)},
            {6.0 / 29, new Fraction(6, 29)},
            {6.0 / 31, new Fraction(6, 31)},
            {7.0 / 2, new Fraction(7, 2)},
            {7.0 / 3, new Fraction(7, 3)},
            {7.0 / 4, new Fraction(7, 4)},
            {7.0 / 5, new Fraction(7, 5)},
            {7.0 / 6, new Fraction(7, 6)},
            {7.0 / 8, new Fraction(7, 8)},
            {7.0 / 9, new Fraction(7, 9)},
            {7.0 / 10, new Fraction(7, 10)},
            {7.0 / 11, new Fraction(7, 11)},
            {7.0 / 12, new Fraction(7, 12)},
            {7.0 / 13, new Fraction(7, 13)},
            {7.0 / 15, new Fraction(7, 15)},
            {7.0 / 16, new Fraction(7, 16)},
            {7.0 / 17, new Fraction(7, 17)},
            {7.0 / 18, new Fraction(7, 18)},
            {7.0 / 19, new Fraction(7, 19)},
            {7.0 / 20, new Fraction(7, 20)},
            {7.0 / 22, new Fraction(7, 22)},
            {7.0 / 23, new Fraction(7, 23)},
            {7.0 / 24, new Fraction(7, 24)},
            {7.0 / 25, new Fraction(7, 25)},
            {7.0 / 26, new Fraction(7, 26)},
            {7.0 / 27, new Fraction(7, 27)},
            {7.0 / 29, new Fraction(7, 29)},
            {7.0 / 30, new Fraction(7, 30)},
            {7.0 / 31, new Fraction(7, 31)},
            {7.0 / 32, new Fraction(7, 32)},
            {7.0 / 33, new Fraction(7, 33)},
            {7.0 / 34, new Fraction(7, 34)},
            {8.0 / 2, new Fraction(8, 2)},
            {8.0 / 3, new Fraction(8, 3)},
            {8.0 / 5, new Fraction(8, 5)},
            {8.0 / 7, new Fraction(8, 7)},
            {8.0 / 9, new Fraction(8, 9)},
            {8.0 / 11, new Fraction(8, 11)},
            {8.0 / 13, new Fraction(8, 13)},
            {8.0 / 15, new Fraction(8, 15)},
            {8.0 / 17, new Fraction(8, 17)},
            {8.0 / 19, new Fraction(8, 19)},
            {8.0 / 21, new Fraction(8, 21)},
            {8.0 / 23, new Fraction(8, 23)},
            {8.0 / 25, new Fraction(8, 25)},
            {8.0 / 27, new Fraction(8, 27)},
            {8.0 / 29, new Fraction(8, 29)},
            {8.0 / 31, new Fraction(8, 31)},
            {8.0 / 33, new Fraction(8, 33)},
            {9.0 / 2, new Fraction(9, 2)},
            {9.0 / 4, new Fraction(9, 4)},
            {9.0 / 5, new Fraction(9, 5)},
            {9.0 / 7, new Fraction(9, 7)},
            {9.0 / 8, new Fraction(9, 8)},
            {9.0 / 10, new Fraction(9, 10)},
            {9.0 / 11, new Fraction(9, 11)},
            {9.0 / 13, new Fraction(9, 13)},
            {9.0 / 14, new Fraction(9, 14)},
            {9.0 / 16, new Fraction(9, 16)},
            {9.0 / 17, new Fraction(9, 17)},
            {9.0 / 19, new Fraction(9, 19)},
            {9.0 / 20, new Fraction(9, 20)},
            {9.0 / 22, new Fraction(9, 22)},
            {9.0 / 23, new Fraction(9, 23)},
            {9.0 / 25, new Fraction(9, 25)},
            {9.0 / 26, new Fraction(9, 26)},
            {9.0 / 28, new Fraction(9, 28)},
            {9.0 / 29, new Fraction(9, 29)},
            {9.0 / 31, new Fraction(9, 31)},
            {9.0 / 32, new Fraction(9, 32)},
            {9.0 / 34, new Fraction(9, 34)},
        };
    }
}
