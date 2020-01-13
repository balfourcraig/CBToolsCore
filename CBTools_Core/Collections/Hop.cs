using System;
using System.Runtime.InteropServices;

namespace CBTools_Core.Collections {
    [StructLayout(LayoutKind.Explicit, Size = 6)]
    public readonly struct Hop : IEquatable<Hop> {
        [FieldOffset(0)]
        public readonly ushort from;
        [FieldOffset(2)]
        public readonly ushort to;
        [FieldOffset(4)]
        public readonly bool expected;
        [FieldOffset(5)]
        public readonly bool valid;

        [FieldOffset(0)]
        private readonly int fromTo;

        /// <summary>
        /// Convert this struct to a single int32 for transport. Doing this will throw an exception if either from or to are less than 0
        /// </summary>
        public int ToInt => fromTo | (expected ? (1 << 31) : 0) | (valid ? (1 << 15) : 0);

        public void Deconstruct(out ushort from, out ushort to, out bool expected, out bool valid) {
            from = this.from;
            to = this.to;
            expected = this.expected;
            valid = this.valid;
        }

        public Hop(ushort from, ushort to, bool expected = true, bool valid = true) {
            if (from > (ushort.MaxValue / 2) || to > (ushort.MaxValue / 2))
                throw new ArgumentException("from or too too large");
            this.fromTo = 0;
            this.from = from;
            this.to = to;
            this.expected = expected;
            this.valid = valid;
        }

        public Hop(int compressed) {
            this.fromTo = 0;
            expected = compressed < 0;
            valid = (compressed & (1 << 15)) >> 15 == 1;

            compressed &= (((1 << 31) >> 31) ^ (1 << 31)) ^ (1 << 15);//Set both sign bits to 0

            from = 0;
            to = 0;
            fromTo = compressed;

            //from = (ushort)compressed;
            //to = (ushort)(compressed >> 16);
        }

        /// <summary>
        /// Reconstruct a Hop object from an int (int format as produced by Hop.AsInt)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Hop FromInt(int i) {
            bool expected = i < 0;
            bool valid = (i & (1 << 15)) >> 15 == 1;

            i &= (((1 << 31) >> 31) ^ (1 << 31)) ^ (1 << 15);//Set both sign bits to 0
            //Also, WUT?!

            ushort from = (ushort)i;
            ushort to = (ushort)(i >> 16);

            return new Hop(from, to, expected, valid);
        }

        public override string ToString() => string.Join(',', from, to, expected, valid);

        public override int GetHashCode() => ToInt;

        public override bool Equals(object obj) {
            if (obj == null) {
                throw new NullReferenceException("Comparison to null");
            }
            else {
                if (obj is Hop)
                    return this == (Hop)obj;
                else
                    throw new Exception("Comparision of Hop to invalid type");
            }
        }

        public Hop Reverse(bool expected, bool valid) => new Hop(this.to, this.from, expected, valid);
        public Hop Reverse() => new Hop(to, from, expected, valid);

        public static bool operator ==(Hop x, Hop Y) {
            return x.ToInt == Y.ToInt;
        }

        public static bool operator !=(Hop x, Hop Y) {
            return x.ToInt != Y.ToInt;
        }

        public bool Equals(Hop other) => this == other;
    }
}
