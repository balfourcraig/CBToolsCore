using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CBTools_Core
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public readonly struct IPv4Address : IEquatable<IPv4Address>
    {
        [FieldOffset(0)]
        public readonly byte oct1;
        [FieldOffset(1)]
        public readonly byte oct2;
        [FieldOffset(2)]
        public readonly byte oct3;
        [FieldOffset(3)]
        public readonly byte oct4;

        [FieldOffset(0)]
        public readonly uint address;

        [Obsolete("Use field address to avoid a method call")]
        public int Int => (oct1 << 24) | (oct2 << 16) | (oct3 << 8) | (oct4);

        public string Bin => string.Join(".",
            Convert.ToString(oct1, 2).PadLeft(8, '0'),
            Convert.ToString(oct2, 2).PadLeft(8, '0'),
            Convert.ToString(oct3, 2).PadLeft(8, '0'),
            Convert.ToString(oct4, 2).PadLeft(8, '0')
        );

        public IPv4Address(byte oct1, byte oct2, byte oct3, byte oct4)
        {
            this.address = 0;
            this.oct1 = oct1;
            this.oct2 = oct2;
            this.oct3 = oct3;
            this.oct4 = oct4;
        }

        public IPv4Address(uint address)
        {
            oct1 = 0;
            oct2 = 0;
            oct3 = 0;
            oct4 = 0;
            this.address = address;
        }

        public IPv4Address(string address)
        {
            this.address = 0;
            if (string.IsNullOrWhiteSpace(address))
                throw new InvalidOperationException("String address must not be empty");
            else
            {
                string[] parts = address.Split('.');
                if (parts.Length == 4)
                {
                    this.oct1 = byte.Parse(parts[0]);
                    this.oct2 = byte.Parse(parts[1]);
                    this.oct3 = byte.Parse(parts[2]);
                    this.oct4 = byte.Parse(parts[3]);
                }
                else
                    throw new InvalidOperationException("String address must be in the format a.b.c.d");
            }
        }

        public static bool operator ==(IPv4Address x, IPv4Address y) => x.oct1 == y.oct1 && x.oct2 == y.oct2 && x.oct3 == y.oct3 && x.oct4 == y.oct4;
        public static bool operator !=(IPv4Address x, IPv4Address y) => !(x == y);
        public override bool Equals(object obj) => obj is IPv4Address && this == (IPv4Address)obj;
        public bool Equals(IPv4Address other) => this == other;
        public override int GetHashCode() => (int)address;

        public override string ToString() => string.Join(".", oct1.ToString(), oct2.ToString(), oct3.ToString(), oct4.ToString());//Repeated code to avoid always going through the toBase switch for such a common call
        public string ToString(string separator, int toBase = 10)
        {
            switch (toBase)
            {
                case 2:
                    return string.Join(separator,
                        Convert.ToString(oct1, 2).PadLeft(8, '0'),
                        Convert.ToString(oct2, 2).PadLeft(8, '0'),
                        Convert.ToString(oct3, 2).PadLeft(8, '0'),
                        Convert.ToString(oct4, 2).PadLeft(8, '0'));
                case 10:
                    return string.Join(separator, oct1.ToString(), oct2.ToString(), oct3.ToString(), oct4.ToString());
                case 16:
                    return string.Join(separator,
                        Convert.ToString(oct1, 16).PadLeft(4, '0'),
                        Convert.ToString(oct2, 16).PadLeft(4, '0'),
                        Convert.ToString(oct3, 16).PadLeft(4, '0'),
                        Convert.ToString(oct4, 16).PadLeft(4, '0'));
                default:
                    return string.Join(separator,
                        Convert.ToString(oct1, toBase),
                        Convert.ToString(oct2, toBase),
                        Convert.ToString(oct3, toBase),
                        Convert.ToString(oct4, toBase));
            }
        }
    }
}
