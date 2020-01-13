using System;

namespace CBTools_Core.Extensions {
    public static partial class IntExtensions {
        public static string ToBinaryString(this short num) => Convert.ToString(num, 2).PadLeft(sizeof(short) * 8, '0');

        public static string ToBinaryString(this short num, char delimeter) {
            char[] chars = new char[sizeof(short) * 8 + sizeof(short) * 2];
            for (int i = 0, inner = 0; inner < sizeof(short) * 8; i++, inner++) {
                if (inner > 0 && inner % 4 == 0)
                    chars[chars.Length - 1 - i++] = delimeter;
                chars[chars.Length - 1 - i] = (char)(((num >> inner) & 0b_1) + '0');
            }
            chars[0] = delimeter;
            return "0b" + new string(chars);
        }
        public static string ToBinaryString(this int num) => Convert.ToString(num, 2).PadLeft(sizeof(int) * 8, '0');

        public static string ToBinaryString(this int num, char delimeter) {
            char[] chars = new char[sizeof(int) * 8 + sizeof(int) * 2];
            for (int i = 0, inner = 0; inner < sizeof(int) * 8; i++, inner++) {
                if (inner > 0 && inner % 4 == 0)
                    chars[chars.Length - 1 - i++] = delimeter;
                chars[chars.Length - 1 - i] = (char)(((num >> inner) & 0b_1) + '0');
            }
            chars[0] = delimeter;
            return "0b" + new string(chars);
        }
        public static string ToBinaryString(this long num) => Convert.ToString(num, 2).PadLeft(sizeof(long) * 8, '0');

        public static string ToBinaryString(this long num, char delimeter) {
            char[] chars = new char[sizeof(long) * 8 + sizeof(long) * 2];
            for (int i = 0, inner = 0; inner < sizeof(long) * 8; i++, inner++) {
                if (inner > 0 && inner % 4 == 0)
                    chars[chars.Length - 1 - i++] = delimeter;
                chars[chars.Length - 1 - i] = (char)(((num >> inner) & 0b_1) + '0');
            }
            chars[0] = delimeter;
            return "0b" + new string(chars);
        }
        public static string ToBinaryString(this ushort num) => Convert.ToString(num, 2).PadLeft(sizeof(ushort) * 8, '0');

        public static string ToBinaryString(this ushort num, char delimeter) {
            char[] chars = new char[sizeof(ushort) * 8 + sizeof(ushort) * 2];
            for (int i = 0, inner = 0; inner < sizeof(ushort) * 8; i++, inner++) {
                if (inner > 0 && inner % 4 == 0)
                    chars[chars.Length - 1 - i++] = delimeter;
                chars[chars.Length - 1 - i] = (char)(((num >> inner) & 0b_1) + '0');
            }
            chars[0] = delimeter;
            return "0b" + new string(chars);
        }
        public static string ToBinaryString(this uint num) => Convert.ToString(num, 2).PadLeft(sizeof(uint) * 8, '0');

        public static string ToBinaryString(this uint num, char delimeter) {
            char[] chars = new char[sizeof(uint) * 8 + sizeof(uint) * 2];
            for (int i = 0, inner = 0; inner < sizeof(uint) * 8; i++, inner++) {
                if (inner > 0 && inner % 4 == 0)
                    chars[chars.Length - 1 - i++] = delimeter;
                chars[chars.Length - 1 - i] = (char)(((num >> inner) & 0b_1) + '0');
            }
            chars[0] = delimeter;
            return "0b" + new string(chars);
        }
        public static string ToBinaryString(this byte num) => Convert.ToString(num, 2).PadLeft(sizeof(byte) * 8, '0');

        public static string ToBinaryString(this byte num, char delimeter) {
            char[] chars = new char[sizeof(byte) * 8 + sizeof(byte) * 2];
            for (int i = 0, inner = 0; inner < sizeof(byte) * 8; i++, inner++) {
                if (inner > 0 && inner % 4 == 0)
                    chars[chars.Length - 1 - i++] = delimeter;
                chars[chars.Length - 1 - i] = (char)(((num >> inner) & 0b_1) + '0');
            }
            chars[0] = delimeter;
            return "0b" + new string(chars);
        }
        public static string ToBinaryString(this sbyte num) => Convert.ToString(num, 2).PadLeft(sizeof(sbyte) * 8, '0');

        public static string ToBinaryString(this sbyte num, char delimeter) {
            char[] chars = new char[sizeof(sbyte) * 8 + sizeof(sbyte) * 2];
            for (int i = 0, inner = 0; inner < sizeof(sbyte) * 8; i++, inner++) {
                if (inner > 0 && inner % 4 == 0)
                    chars[chars.Length - 1 - i++] = delimeter;
                chars[chars.Length - 1 - i] = (char)(((num >> inner) & 0b_1) + '0');
            }
            chars[0] = delimeter;
            return "0b" + new string(chars);
        }
    }
}