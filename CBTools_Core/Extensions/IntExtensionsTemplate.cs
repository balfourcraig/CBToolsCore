namespace CBTools_Core.Extensions {
    public static partial class IntExtensions {
        public static bool IsEven(this short num) => (num & 1) == 0;

        public static bool AsBool(this short num) => num != 0;

        public static bool IsEven(this int num) => (num & 1) == 0;

        public static bool AsBool(this int num) => num != 0;

        public static bool IsEven(this long num) => (num & 1) == 0;

        public static bool AsBool(this long num) => num != 0;

        public static bool IsEven(this ushort num) => (num & 1) == 0;

        public static bool AsBool(this ushort num) => num != 0;

        public static bool IsEven(this uint num) => (num & 1) == 0;

        public static bool AsBool(this uint num) => num != 0;

        public static bool IsEven(this ulong num) => (num & 1) == 0;

        public static bool AsBool(this ulong num) => num != 0;

        public static bool IsEven(this byte num) => (num & 1) == 0;

        public static bool AsBool(this byte num) => num != 0;

        public static bool IsEven(this sbyte num) => (num & 1) == 0;

        public static bool AsBool(this sbyte num) => num != 0;

    }
}