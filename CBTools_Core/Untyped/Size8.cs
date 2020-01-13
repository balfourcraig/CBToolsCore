using System;
using System.Runtime.InteropServices;

namespace CBTools_Core.Untyped {
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public readonly struct Size8 : IEquatable<Size8>, IEquatable<long>, IEquatable<ulong>, IEquatable<double> {
        /// <summary>
        /// Interpret 8 bytes as Int64
        /// </summary>
        [FieldOffset(0)]
        public readonly long asLong;

        /// <summary>
        /// Interpret 8 bytes as UInt64
        /// </summary>
        [FieldOffset(0)]
        public readonly ulong asULong;

        /// <summary>
        /// Interpret 8 bytes as Double
        /// </summary>
        [FieldOffset(0)]
        public readonly double asDouble;

        public Size8(long l) {
            this.asDouble = default;
            this.asULong = default;
            this.asLong = l;
        }

        public Size8(ulong ul) {
            this.asDouble = default;
            this.asLong = default;
            this.asULong = ul;
        }

        public Size8(double d) {
            this.asULong = default;
            this.asLong = default;
            this.asDouble = d;
        }

        public bool Equals(Size8 other) => asLong == other.asLong;
        public bool Equals(long other) => asLong == other;
        public bool Equals(ulong other) => asULong == other;
        public bool Equals(double other) => asDouble == other;

        public static bool operator ==(Size8 x, Size8 y) {
            return x.asLong == y.asLong;
        }

        public static bool operator ==(Size8 x, long y) {
            return x.asLong == y;
        }

        public static bool operator ==(Size8 x, ulong y) {
            return x.asULong == y;
        }

        public static bool operator ==(Size8 x, double y) {
            return x.asDouble == y;
        }

        public static bool operator !=(Size8 x, Size8 y) {
            return !(x.asLong == y.asLong);
        }

        public static bool operator !=(Size8 x, long y) {
            return !(x.asLong == y);
        }

        public static bool operator !=(Size8 x, ulong y) {
            return !(x.asULong == y);
        }

        public static bool operator !=(Size8 x, double y) {
            return !(x.asDouble == y);
        }

        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            else if (obj is Size8)
                return this == (Size8)obj;
            else
                return false;
        }

        public override int GetHashCode() => asLong.GetHashCode();
    }
}
