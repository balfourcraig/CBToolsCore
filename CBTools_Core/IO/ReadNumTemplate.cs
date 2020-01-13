using System;

namespace CBTools_Core.IO {
    public static partial class ConsoleRead {
        public static short ReadInt16(bool showMessageIfInvalid = true) {
            if (short.TryParse(Console.ReadLine(), out short result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Int16");
                return ReadInt16(showMessageIfInvalid);
            }
        }

        public static short ReadInt16(in short max, bool showMessageIfInvalid = true) {
            if (short.TryParse(Console.ReadLine(), out short result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Int16");
            }

            return ReadInt16(max, showMessageIfInvalid);
        }

        public static int ReadInt32(bool showMessageIfInvalid = true) {
            if (int.TryParse(Console.ReadLine(), out int result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Int32");
                return ReadInt32(showMessageIfInvalid);
            }
        }

        public static int ReadInt32(in int max, bool showMessageIfInvalid = true) {
            if (int.TryParse(Console.ReadLine(), out int result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Int32");
            }

            return ReadInt32(max, showMessageIfInvalid);
        }

        public static long ReadInt64(bool showMessageIfInvalid = true) {
            if (long.TryParse(Console.ReadLine(), out long result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Int64");
                return ReadInt64(showMessageIfInvalid);
            }
        }

        public static long ReadInt64(in long max, bool showMessageIfInvalid = true) {
            if (long.TryParse(Console.ReadLine(), out long result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Int64");
            }

            return ReadInt64(max, showMessageIfInvalid);
        }

        public static ushort ReadUInt16(bool showMessageIfInvalid = true) {
            if (ushort.TryParse(Console.ReadLine(), out ushort result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not UInt16");
                return ReadUInt16(showMessageIfInvalid);
            }
        }

        public static ushort ReadUInt16(in ushort max, bool showMessageIfInvalid = true) {
            if (ushort.TryParse(Console.ReadLine(), out ushort result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a UInt16");
            }

            return ReadUInt16(max, showMessageIfInvalid);
        }

        public static uint ReadUInt32(bool showMessageIfInvalid = true) {
            if (uint.TryParse(Console.ReadLine(), out uint result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not UInt32");
                return ReadUInt32(showMessageIfInvalid);
            }
        }

        public static uint ReadUInt32(in uint max, bool showMessageIfInvalid = true) {
            if (uint.TryParse(Console.ReadLine(), out uint result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a UInt32");
            }

            return ReadUInt32(max, showMessageIfInvalid);
        }

        public static ulong ReadUInt64(bool showMessageIfInvalid = true) {
            if (ulong.TryParse(Console.ReadLine(), out ulong result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not UInt64");
                return ReadUInt64(showMessageIfInvalid);
            }
        }

        public static ulong ReadUInt64(in ulong max, bool showMessageIfInvalid = true) {
            if (ulong.TryParse(Console.ReadLine(), out ulong result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a UInt64");
            }

            return ReadUInt64(max, showMessageIfInvalid);
        }

        public static byte ReadByte(bool showMessageIfInvalid = true) {
            if (byte.TryParse(Console.ReadLine(), out byte result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Byte");
                return ReadByte(showMessageIfInvalid);
            }
        }

        public static byte ReadByte(in byte max, bool showMessageIfInvalid = true) {
            if (byte.TryParse(Console.ReadLine(), out byte result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Byte");
            }

            return ReadByte(max, showMessageIfInvalid);
        }

        public static sbyte ReadSByte(bool showMessageIfInvalid = true) {
            if (sbyte.TryParse(Console.ReadLine(), out sbyte result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not SByte");
                return ReadSByte(showMessageIfInvalid);
            }
        }

        public static sbyte ReadSByte(in sbyte max, bool showMessageIfInvalid = true) {
            if (sbyte.TryParse(Console.ReadLine(), out sbyte result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a SByte");
            }

            return ReadSByte(max, showMessageIfInvalid);
        }

        public static double ReadDouble(bool showMessageIfInvalid = true) {
            if (double.TryParse(Console.ReadLine(), out double result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Double");
                return ReadDouble(showMessageIfInvalid);
            }
        }

        public static double ReadDouble(in double max, bool showMessageIfInvalid = true) {
            if (double.TryParse(Console.ReadLine(), out double result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Double");
            }

            return ReadDouble(max, showMessageIfInvalid);
        }

        public static float ReadSingle(bool showMessageIfInvalid = true) {
            if (float.TryParse(Console.ReadLine(), out float result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Single");
                return ReadSingle(showMessageIfInvalid);
            }
        }

        public static float ReadSingle(in float max, bool showMessageIfInvalid = true) {
            if (float.TryParse(Console.ReadLine(), out float result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Single");
            }

            return ReadSingle(max, showMessageIfInvalid);
        }

        public static decimal ReadDecimal(bool showMessageIfInvalid = true) {
            if (decimal.TryParse(Console.ReadLine(), out decimal result)) {
                return result;
            }
            else {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Decimal");
                return ReadDecimal(showMessageIfInvalid);
            }
        }

        public static decimal ReadDecimal(in decimal max, bool showMessageIfInvalid = true) {
            if (decimal.TryParse(Console.ReadLine(), out decimal result)) {
                if (result <= max)
                    return result;

                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
            }
            else if (showMessageIfInvalid) {
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Decimal");
            }

            return ReadDecimal(max, showMessageIfInvalid);
        }

    }
}