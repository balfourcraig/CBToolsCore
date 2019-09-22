using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CBTools_Core.Untyped
{
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public readonly struct Size2 :IEquatable<Size2>, IEquatable<short>, IEquatable<ushort>, IEquatable<char>
    {
        /// <summary>
        /// Interpret 2 bytes as Int16
        /// </summary>
        [FieldOffset(0)]
        public readonly short asShort;

        /// <summary>
        /// Interpret 2 bytes as UInt16
        /// </summary>
        [FieldOffset(0)]
        public readonly ushort asUShort;

        /// <summary>
        /// Interpret 2 bytes as unicode char. Could be unexpected
        /// </summary>
        [FieldOffset(0)]
        public readonly char asChar;

        public Size2(short s)
        {
            this.asUShort = default;
            this.asChar = default;
            this.asShort = s;
        }

        public Size2(ushort us)
        {
            this.asChar = default;
            this.asShort = default;
            this.asUShort = us;
        }

        public Size2(char c)
        {
            this.asShort = default;
            this.asUShort = default;
            this.asChar = c;
        }

        public bool Equals(Size2 other) => this.asUShort == other.asUShort;
        public bool Equals(short other) => this.asShort == other;
        public bool Equals(ushort other) => this.asUShort == other;
        public bool Equals(char other) => this.asChar == other;

        public static bool operator ==(Size2 x, Size2 y) => x.asUShort == y.asUShort;
        public static bool operator ==(Size2 x, short y) => x.asShort == y;
        public static bool operator ==(Size2 x, ushort y) => x.asUShort == y;
        public static bool operator ==(Size2 x, char y) => x.asChar == y;

        public static bool operator !=(Size2 x, Size2 y) => !(x.asUShort == y.asUShort);
        public static bool operator !=(Size2 x, short y) => !(x.asShort == y);
        public static bool operator !=(Size2 x, ushort y) => !(x.asUShort == y);
        public static bool operator !=(Size2 x, char y) => !(x.asChar == y);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Size2)
                return this == (Size2)obj;
            else
                return false;
        }

        public override int GetHashCode() => asShort;
    }
}
