using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CBTools_Core.Untyped
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public readonly struct Size4 : IEquatable<Size4>, IEquatable<int>, IEquatable<uint>, IEquatable<float>
    {
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

        public Size4(int i)
        {
            this.asFloat = default;
            this.asUInt = default;
            this.asInt = i;
        }

        public Size4(uint ui)
        {
            this.asFloat = default;
            this.asInt = default;
            this.asUInt = ui;
        }

        public Size4(float f)
        {
            this.asInt = default;
            this.asUInt = default;
            this.asFloat = f;
        }

        public bool Equals(Size4 other) => this.asUInt == other.asUInt;
        public bool Equals(int other) => this.asInt == other;
        public bool Equals(uint other) => this.asUInt == other;
        public bool Equals(float other) => this.asFloat == other;

        public static bool operator ==(Size4 x, Size4 y) => x.asInt == y.asInt;
        public static bool operator ==(Size4 x, int y) => x.asInt == y;
        public static bool operator ==(Size4 x, uint y) => x.asUInt == y;
        public static bool operator ==(Size4 x, float y) => x.asFloat == y;

        public static bool operator !=(Size4 x, Size4 y) => !(x.asInt == y.asInt);
        public static bool operator !=(Size4 x, int y) => !(x.asInt == y);
        public static bool operator !=(Size4 x, uint y) => !(x.asUInt == y);
        public static bool operator !=(Size4 x, float y) => !(x.asFloat == y);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Size4)
                return this == (Size4)obj;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return asInt;
        }
    }
}
