using System;
using System.Runtime.InteropServices;

namespace CBTools_Core.Untyped {
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public readonly struct Size4 : IEquatable<Size4>, IEquatable<int>, IEquatable<uint>, IEquatable<float> {
        /// <summary>
        /// Interpret 4 bytes as Int32
        /// </summary>
        [FieldOffset(0)]
        public readonly int asInt;

        /// <summary>
        /// Interpret 4 bytes as UInt32
        /// </summary>
        [FieldOffset(0)]
        public readonly uint asUInt;

        /// <summary>
        /// Interpret 4 bytes as Single float
        /// </summary>
        [FieldOffset(0)]
        public readonly float asFloat;

        public Size4(int i) {
            asFloat = default;
            asUInt = default;
            asInt = i;
        }

        public Size4(uint ui) {
            asFloat = default;
            asInt = default;
            asUInt = ui;
        }

        public Size4(float f) {
            asInt = default;
            asUInt = default;
            asFloat = f;
        }

        public bool Equals(Size4 other) => asUInt == other.asUInt;
        public bool Equals(int other) => asInt == other;
        public bool Equals(uint other) => asUInt == other;
        public bool Equals(float other) => asFloat == other;

        public static bool operator ==(Size4 x, Size4 y) => x.asInt == y.asInt;

        public static bool operator ==(Size4 x, int y) => x.asInt == y;

        public static bool operator ==(Size4 x, uint y) => x.asUInt == y;

        public static bool operator ==(Size4 x, float y) => x.asFloat == y;

        public static bool operator !=(Size4 x, Size4 y) => !(x.asInt == y.asInt);

        public static bool operator !=(Size4 x, int y) => !(x.asInt == y);

        public static bool operator !=(Size4 x, uint y) => !(x.asUInt == y);

        public static bool operator !=(Size4 x, float y) => !(x.asFloat == y);

        public override bool Equals(object obj) => obj != null && obj is Size4 size && this == size;

        public override int GetHashCode() => asInt;
    }
}
