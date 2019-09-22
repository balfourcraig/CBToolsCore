using System;

namespace CBTools_Core.Extensions
{
	public static partial class IntExtensions {
		public static bool IsEven (this Int16 num) => (num & 1) == 0;

		public static bool AsBool(this  Int16 num) => num != 0;

		public static bool IsEven (this Int32 num) => (num & 1) == 0;

		public static bool AsBool(this  Int32 num) => num != 0;

		public static bool IsEven (this Int64 num) => (num & 1) == 0;

		public static bool AsBool(this  Int64 num) => num != 0;

		public static bool IsEven (this UInt16 num) => (num & 1) == 0;

		public static bool AsBool(this  UInt16 num) => num != 0;

		public static bool IsEven (this UInt32 num) => (num & 1) == 0;

		public static bool AsBool(this  UInt32 num) => num != 0;

		public static bool IsEven (this UInt64 num) => (num & 1) == 0;

		public static bool AsBool(this  UInt64 num) => num != 0;

		public static bool IsEven (this Byte num) => (num & 1) == 0;

		public static bool AsBool(this  Byte num) => num != 0;

		public static bool IsEven (this SByte num) => (num & 1) == 0;

		public static bool AsBool(this  SByte num) => num != 0;

			}
}