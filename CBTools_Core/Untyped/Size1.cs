using System;
using System.Runtime.InteropServices;

namespace CBTools_Core.Untyped {
    [StructLayout(LayoutKind.Explicit, Size = 1)]
    public readonly struct Size1 : IEquatable<Size1>, IEquatable<bool>, IEquatable<byte>, IEquatable<sbyte> {
        /// <summary>
        /// Interpret byte as bool. This could be unexpected!
        /// </summary>
        [FieldOffset(0)]
        public readonly bool asBool;

        /// <summary>
        /// Interpret byte as byte
        /// </summary>
        [FieldOffset(0)]
        public readonly byte asByte;

        /// <summary>
        /// Interpret byte as sbyte (not a cast)
        /// </summary>
        [FieldOffset(0)]
        public readonly sbyte asSByte;

        public Size1(bool b) {
            this.asByte = default;
            this.asSByte = default;
            this.asBool = b;
        }

        public Size1(byte b) {
            asBool = default;
            asSByte = default;
            asByte = b;

        }

        public Size1(sbyte b) {
            asByte = default;
            asBool = default;
            asSByte = b;
        }

        public bool Equals(Size1 other) => other.asByte == asByte;
        public bool Equals(bool other) => other == asBool;
        public bool Equals(byte other) => other == asByte;
        public bool Equals(sbyte other) => other == asSByte;

        public static bool operator ==(Size1 x, Size1 y) => x.asByte == y.asByte;
        public static bool operator !=(Size1 x, Size1 y) => !(x.asByte == y.asByte);
        public static bool operator ==(Size1 x, bool y) => x.asBool == y;
        public static bool operator ==(Size1 x, byte y) => x.asByte == y;
        public static bool operator ==(Size1 x, sbyte y) => x.asSByte == y;
        public static bool operator !=(Size1 x, bool y) => !(x.asBool == y);
        public static bool operator !=(Size1 x, byte y) => !(x.asByte == y);
        public static bool operator !=(Size1 x, sbyte y) => !(x.asSByte == y);

        public override int GetHashCode() => asByte;

        public override bool Equals(object obj) => obj != null && obj is Size1 size && this == size;
    }
}
