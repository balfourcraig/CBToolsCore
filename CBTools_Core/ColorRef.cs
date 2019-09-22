using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Runtime.InteropServices;

namespace CBTools_Core
{
    /// <summary>
    /// This is the same structure as ColorRGBA, but as a ref struct
    /// This may be useful for performance critical code, but in most cases use ColorRGBA
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public readonly ref struct ColorRef
    {
        private const uint _oddMask    = 0x_aaaa_aaaa;
        private const uint _evenMask   = 0x_5555_5555;

        [FieldOffset(0)]
        public readonly byte r;
        [FieldOffset(1)]
        public readonly byte g;
        [FieldOffset(2)]
        public readonly byte b;
        [FieldOffset(3)]
        public readonly byte a;

        [FieldOffset(0)]
        public readonly uint packed;

        public string Hex => string.Concat("#",
                    Convert.ToString(r, 16).PadLeft(2, '0'),
                    Convert.ToString(g, 16).PadLeft(2, '0'),
                    Convert.ToString(b, 16).PadLeft(2, '0')
                );

        [Obsolete("This calculates value as an int, use filed 'packed' instead which uses fieldOffset to avoid any method calls or allocations")]
        public int ToInt() => b | g << 8 | r << 16 | a << 24;

        public ColorRGBA PromoteToStruct() => new ColorRGBA(r, g, b, a);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r">Red 0-255</param>
        /// <param name="g">Green 0-255</param>
        /// <param name="b">Blue 0-255</param>
        /// <param name="a">Alpha  0-255</param>
        public ColorRef(byte r, byte g, byte b, byte a)
        {
            this.packed = 0;
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        /// <summary>
        /// Set rgb, alpha is set as 255
        /// </summary>
        /// <param name="r">Red 0-255</param>
        /// <param name="g">Green 0-255</param>
        /// <param name="b">Blue 0-255</param>
        public ColorRef(byte r, byte g, byte b) : this(r, g, b, 255)
        {
        }

        /// <summary>
        /// rgb are all set to gray, alpha is set to 255
        /// </summary>
        /// <param name="gray">valid: 0-255</param>
        public ColorRef(byte gray) : this(gray, 255)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gray">valid: 0-255</param>
        /// <param name="alpha">valid: 0-255</param>
        public ColorRef(byte gray, byte alpha) : this(gray, gray, gray, alpha)
        {
        }

        /// <summary>
        /// Reconstruct from compressed form,
        /// compressed form can be from saved color, or from ColorRGBA
        /// </summary>
        public ColorRef(uint compressed)
        {
            this.r = 0;
            this.g = 0;
            this.b = 0;
            this.a = 0;
            this.packed = compressed;
        }

        /// <summary>
        /// Build from hex string. This is slower than all the other constructors,
        /// so don't use it in performance critical areas
        /// </summary>
        /// <param name="hexValue">Can have leading # or not</param>
        public ColorRef(string hexValue)
        {
            this.packed = 0;

            if (string.IsNullOrWhiteSpace(hexValue))
                r = g = b = 0;
            else
            {
                byte start = 0;
                if (hexValue[start] == '#')
                    start = 1;

                if (hexValue.Length == 6 + start)
                {
                    r = byte.Parse(hexValue.Substring(0 + start, 2), NumberStyles.HexNumber);
                    g = byte.Parse(hexValue.Substring(2 + start, 2), NumberStyles.HexNumber);
                    b = byte.Parse(hexValue.Substring(4 + start, 2), NumberStyles.HexNumber);
                }
                else if (hexValue.Length == 3 + start)
                {
                    r = byte.Parse(hexValue.Substring(0 + start, 1), NumberStyles.HexNumber);
                    g = byte.Parse(hexValue.Substring(1 + start, 1), NumberStyles.HexNumber);
                    b = byte.Parse(hexValue.Substring(2 + start, 1), NumberStyles.HexNumber);

                    r |= (byte)(r << 4);
                    g |= (byte)(g << 4);
                    b |= (byte)(b << 4);
                }
                else
                {
                    throw new Exception("hex value was not in correct format");
                }
            }
            this.a = 255;
        }

        public void Deconstruct(out byte r, out byte g, out byte b, out byte a)
        {
            r = this.r;
            g = this.g;
            b = this.b;
            a = this.a;
        }

        public static bool operator ==(ColorRef x, ColorRef y)
        {
            return (
                x.r == y.r
                && x.g == y.g
                && x.b == y.b
                && x.a == y.a
                );
        }
        public static bool operator !=(ColorRef x, ColorRef y) => !(x == y);

        /// <summary>
        /// Note that this will NOT invert the alpha component.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static ColorRef operator ~(ColorRef x) => new ColorRef((byte)(255 - x.r), (byte)(255 - x.g), (byte)(255 - x.b), x.a);

        public bool Equals(ColorRef obj) => this == obj;

        public override int GetHashCode()
        {
            throw new InvalidOperationException("Cannot box ref struct");
        }

        public override string ToString() => Hex;

        public override bool Equals(object obj)
        {
            throw new NotSupportedException();
        }

        public static explicit operator ColorRGBA(ColorRef color)
        {
            return new ColorRGBA(color.packed);
        }

        public static explicit operator ColorRef(ColorRGBA color)
        {
            return new ColorRef(color.packed);
        }
    }
}
