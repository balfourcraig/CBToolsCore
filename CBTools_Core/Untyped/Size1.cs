using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CBTools_Core.Untyped
{
    [StructLayout(LayoutKind.Explicit, Size = 1)]
    public readonly struct Size1 : IEquatable<Size1>, IEquatable<bool>, IEquatable<byte>, IEquatable<sbyte>
    {
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

        public Size1(bool b)
        {
            this.asByte = default;
            this.asSByte = default;
            this.asBool = b;
        }

        public Size1(byte b)
        {
            this.asBool = default;
            this.asSByte = default;
            this.asByte = b;

        }

        public Size1(sbyte b)
        {
            this.asByte = default;
            this.asBool = default;
            this.asSByte = b;
        }

        public bool Equals(Size1 other) => other.asByte == this.asByte;

        public bool Equals(bool other) => other == this.asBool;

        public bool Equals(byte other) => other == this.asByte;

        public bool Equals(sbyte other) => other == this.asSByte;

        public static bool operator ==(Size1 x, Size1 y) => x.asByte == y.asByte;
        public static bool operator !=(Size1 x, Size1 y) => !(x.asByte == y.asByte);

        
        public static bool operator ==(Size1 x, bool y) => x.asBool == y;
        public static bool operator ==(Size1 x, byte y) => x.asByte == y;
        public static bool operator ==(Size1 x, sbyte y) => x.asSByte == y;

        
        public static bool operator !=(Size1 x, bool y) => !(x.asBool == y);
        public static bool operator !=(Size1 x, byte y) => !(x.asByte == y);
        public static bool operator !=(Size1 x, sbyte y) => !(x.asSByte == y);


        public override int GetHashCode() => asByte;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (obj is Size1)
            {
                return this == (Size1)obj;
            }
            else return false;
        }
    }
}
