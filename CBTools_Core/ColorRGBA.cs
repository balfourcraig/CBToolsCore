using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace CBTools_Core {
    /// <summary>
    /// Simple color struct consisting of 4 bytes. 3 for color (RGB), 1 for alpha
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public readonly struct ColorRGBA : IEquatable<ColorRGBA> {
        private const uint _oddMask = 0x_aaaa_aaaa;
        private const uint _evenMask = 0x_5555_5555;

        //Basic web colors
        public static ColorRGBA White => new ColorRGBA(255, 255, 255);
        public static ColorRGBA Silver => new ColorRGBA(192, 192, 192);
        public static ColorRGBA Gray => new ColorRGBA(128, 128, 128);
        public static ColorRGBA Black => new ColorRGBA(0, 0, 0);
        public static ColorRGBA Red => new ColorRGBA(255, 0, 0);
        public static ColorRGBA Maroon => new ColorRGBA(128, 0, 0);
        public static ColorRGBA Yellow => new ColorRGBA(255, 255, 0);
        public static ColorRGBA Olive => new ColorRGBA(128, 128, 0);
        public static ColorRGBA Lime => new ColorRGBA(0, 255, 0);
        public static ColorRGBA Green => new ColorRGBA(0, 128, 0);
        public static ColorRGBA Aqua => new ColorRGBA(0, 255, 255);
        public static ColorRGBA Teal => new ColorRGBA(0, 128, 128);
        public static ColorRGBA Blue => new ColorRGBA(0, 0, 255);
        public static ColorRGBA Navy => new ColorRGBA(0, 0, 128);
        public static ColorRGBA Fuchsia => new ColorRGBA(255, 0, 255);
        public static ColorRGBA Purple => new ColorRGBA(128, 0, 128);

        /// <summary>
        /// Red component
        /// </summary>
        [FieldOffset(0)]
        public readonly byte r;

        /// <summary>
        /// Green component
        /// </summary>
        [FieldOffset(1)]
        public readonly byte g;

        /// <summary>
        /// Blue component
        /// </summary>
        [FieldOffset(2)]
        public readonly byte b;

        /// <summary>
        /// Alpha (transperant) component
        /// </summary>
        [FieldOffset(3)]
        public readonly byte a;

        /// <summary>
        /// all four bytes in sequential order, interpreted as a uint.
        /// Order: RGBA
        /// </summary>
        [FieldOffset(0)]
        public readonly uint packed;

        public string Hex => string.Concat("#",
                    Convert.ToString(r, 16).PadLeft(2, '0'),
                    Convert.ToString(g, 16).PadLeft(2, '0'),
                    Convert.ToString(b, 16).PadLeft(2, '0')
                );

        /// <summary>
        /// Copy as a refStruct.
        /// Warning: creates new object on stack
        /// </summary>
        /// <returns></returns>
        public ColorRef AsRefStruct() => new ColorRef(r, g, b, a);

        public string RGB => string.Join(",", r, g, b);
        public string RGBA => string.Join(",", r, g, b, a);

        public string HTMLRGBA => "rgba(" + RGBA + ")";

        [Obsolete("This calculates value as an int, use filed 'packed' instead which uses fieldOffset to avoid any method calls or allocations")]
        public int ToInt() => b | g << 8 | r << 16 | a << 24;

        /// <summary>
        /// Calculates hue, saturation, and value
        /// </summary>
        public (float h, float s, float v) HSV //This has a lot of duplicated code, but it's there to prevent re-calculating things which shouldn't be. So it stays.
        {
            get {
                float r = this.r / 255f;
                float g = this.g / 255f;
                float b = this.b / 255f;

                float min = Math.Min(r, Math.Min(g, b));
                float max = Math.Max(r, Math.Max(g, b));

                //HUE
                float hue = 0;
                if (min != max) {
                    if (max == r)
                        hue = (g - b) / (max - min);

                    else if (max == g)
                        hue = 2f + (b - r) / (max - min);

                    else if (max == b)
                        hue = 4f + (r - g) / (max - min);

                    hue *= 60;
                    if (hue < 0)
                        hue += 360;
                }

                //SAT
                float sat = 0;
                if (max > 0) {
                    sat = (max - min) / max;
                }

                //VAL
                float val = max;

                return (hue, sat, val);
            }
        }

        public void Deconstruct(out byte r, out byte g, out byte b, out byte a) {
            r = this.r;
            g = this.g;
            b = this.b;
            a = this.a;
        }

        public float Hue {
            get {
                float min = Math.Min(r, Math.Min(g, b));
                float max = Math.Max(r, Math.Max(g, b));

                if (min == max) {
                    return 0;
                }
                else {
                    float hue = 0f;
                    if (max == r)
                        hue = (g - b) / (max - min);

                    else if (max == g)
                        hue = 2f + (b - r) / (max - min);

                    else if (max == b)
                        hue = 4f + (r - g) / (max - min);

                    hue *= 60f;
                    if (hue < 0)
                        hue += 360f;

                    return hue;
                }
            }
        }

        public float Value => Math.Max(r, Math.Max(g, b)) / 255f;

        public float Saturation {
            get {
                float max = Math.Max(r, Math.Max(g, b)) / 255f;
                float min = Math.Min(r, Math.Min(g, b)) / 255f;

                if (max > 0)
                    return (max - min) / max;
                else
                    return 0;
            }
        }

        public float Luminosity {
            get {
                float max = Math.Max(r, Math.Max(g, b));
                float min = Math.Min(r, Math.Min(g, b));

                return (max + min) / 2f / 255f;
            }
        }

        /// <summary>
        /// Lightens rgb by the given amount (up to a maximum of 255)
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public ColorRGBA LightenBy(byte amount) => new ColorRGBA(
                (byte)Math.Min(255, r + amount),
                (byte)Math.Min(255, g + amount),
                (byte)Math.Min(255, b + amount)
            );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="r">Red 0-255</param>
        /// <param name="g">Green 0-255</param>
        /// <param name="b">Blue 0-255</param>
        /// <param name="a">Alpha 0-255</param>
        public ColorRGBA(byte r, byte g, byte b, byte a) {
            this.packed = 0;
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r">Red 0-255</param>
        /// <param name="g">Green 0-255</param>
        /// <param name="b">Blue 0-255</param>
        public ColorRGBA(byte r, byte g, byte b) : this(r, g, b, 255) {
        }

        /// <summary>
        /// RGB all set to gray, alpha is 255
        /// </summary>
        /// <param name="gray">0-255</param>
        public ColorRGBA(byte gray) : this(gray, 255) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gray">RGB all set to this</param>
        /// <param name="alpha"></param>
        public ColorRGBA(byte gray, byte alpha) : this(gray, gray, gray, alpha) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressed">From packed saved from ColorRGB or ColorRef</param>
        public ColorRGBA(uint compressed) {
            this.r = 0;
            this.g = 0;
            this.b = 0;
            this.a = 0;
            this.packed = compressed;
        }

        /// <summary>
        /// Build from string, if valid. A little slower than the other constructors
        /// </summary>
        /// <param name="hexValue">May have leading # or not</param>
        public ColorRGBA(string hexValue) {
            this.packed = 0;
            if (string.IsNullOrWhiteSpace(hexValue)) {
                r = g = b = 0;
            }
            else {
                byte start = 0;
                if (hexValue[start] == '#')
                    start = 1;

                if (hexValue.Length == 6 + start) {
                    r = byte.Parse(hexValue.Substring(0 + start, 2), NumberStyles.HexNumber);
                    g = byte.Parse(hexValue.Substring(2 + start, 2), NumberStyles.HexNumber);
                    b = byte.Parse(hexValue.Substring(4 + start, 2), NumberStyles.HexNumber);
                }
                else if (hexValue.Length == 3 + start) {
                    r = byte.Parse(hexValue.Substring(0 + start, 1), NumberStyles.HexNumber);
                    g = byte.Parse(hexValue.Substring(1 + start, 1), NumberStyles.HexNumber);
                    b = byte.Parse(hexValue.Substring(2 + start, 1), NumberStyles.HexNumber);

                    r |= (byte)(r << 4);
                    g |= (byte)(g << 4);
                    b |= (byte)(b << 4);
                }
                else {
                    throw new Exception("hex value was not in correct format");
                }
            }
            this.a = 255;
        }

        /// <summary>
        /// Convert to gray shade
        /// </summary>
        /// <param name="proportional">Whether to average rgb evenly, or to weight as r 30%, g 59%, b 11%</param>
        /// <returns></returns>
        public ColorRGBA Grayscale(bool proportional = false) {
            if (proportional)
                return new ColorRGBA((byte)((r + g + b) / 3f));
            else
                return new ColorRGBA((byte)(r * 0.3f + g * 0.59f + b * 0.11f));
        }


        public static bool operator ==(ColorRGBA x, ColorRGBA y) {
            return (
                x.r == y.r
                && x.g == y.g
                && x.b == y.b
                && x.a == y.a
            );
        }
        public static bool operator !=(ColorRGBA x, ColorRGBA y) {
            return !(x == y);
        }

        /// <summary>
        /// Note that this will NOT invert the alpha component.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static ColorRGBA operator ~(ColorRGBA x) {
            return new ColorRGBA((byte)(255 - x.r), (byte)(255 - x.g), (byte)(255 - x.b), x.a);
        }

        public override bool Equals(object obj) => obj is ColorRGBA && this == (ColorRGBA)obj;
        public bool Equals(ColorRGBA obj) => this == obj;

        public override int GetHashCode() {
            //Is this a good hash function? Is it too complex?
            return (int)(
                ((a & _oddMask) << 24) | ((b & _oddMask) << 16) | ((g & _oddMask) << 8) | (r & _oddMask) |      //ODD bits ABGR
                ((g & _evenMask) << 24) | ((r & _evenMask) << 16) | ((a & _evenMask) << 8) | (b & _evenMask)    //EVEN bits GRAB
                );
            //unchecked((int)packed);
        }

        public override string ToString() => RGBA;

        public static explicit operator ColorRGBA(ColorRef color) {
            return new ColorRGBA(color.packed);
        }

        public static explicit operator ColorRef(ColorRGBA color) {
            return new ColorRef(color.packed);
        }

        public static implicit operator uint(ColorRGBA color) {
            return color.packed;
        }

        public static explicit operator ColorRGBA(uint num) {
            return new ColorRGBA(num);
        }
    }
}
