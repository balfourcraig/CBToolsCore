using System;

namespace CBTools_Core.IO
{
	public static partial class ConsoleRead
	{
				public static Int16 ReadInt16 (bool showMessageIfInvalid = true)
		{
			if (Int16.TryParse(Console.ReadLine(), out Int16 result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Int16");
				return ReadInt16(showMessageIfInvalid);
			}
		}

		public static Int16 ReadInt16(in Int16 max, bool showMessageIfInvalid = true)
		{
			if (Int16.TryParse(Console.ReadLine(), out Int16 result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Int16");
            
			return ReadInt16(max, showMessageIfInvalid);
		}

				public static Int32 ReadInt32 (bool showMessageIfInvalid = true)
		{
			if (Int32.TryParse(Console.ReadLine(), out Int32 result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Int32");
				return ReadInt32(showMessageIfInvalid);
			}
		}

		public static Int32 ReadInt32(in Int32 max, bool showMessageIfInvalid = true)
		{
			if (Int32.TryParse(Console.ReadLine(), out Int32 result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Int32");
            
			return ReadInt32(max, showMessageIfInvalid);
		}

				public static Int64 ReadInt64 (bool showMessageIfInvalid = true)
		{
			if (Int64.TryParse(Console.ReadLine(), out Int64 result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Int64");
				return ReadInt64(showMessageIfInvalid);
			}
		}

		public static Int64 ReadInt64(in Int64 max, bool showMessageIfInvalid = true)
		{
			if (Int64.TryParse(Console.ReadLine(), out Int64 result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Int64");
            
			return ReadInt64(max, showMessageIfInvalid);
		}

				public static UInt16 ReadUInt16 (bool showMessageIfInvalid = true)
		{
			if (UInt16.TryParse(Console.ReadLine(), out UInt16 result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not UInt16");
				return ReadUInt16(showMessageIfInvalid);
			}
		}

		public static UInt16 ReadUInt16(in UInt16 max, bool showMessageIfInvalid = true)
		{
			if (UInt16.TryParse(Console.ReadLine(), out UInt16 result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a UInt16");
            
			return ReadUInt16(max, showMessageIfInvalid);
		}

				public static UInt32 ReadUInt32 (bool showMessageIfInvalid = true)
		{
			if (UInt32.TryParse(Console.ReadLine(), out UInt32 result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not UInt32");
				return ReadUInt32(showMessageIfInvalid);
			}
		}

		public static UInt32 ReadUInt32(in UInt32 max, bool showMessageIfInvalid = true)
		{
			if (UInt32.TryParse(Console.ReadLine(), out UInt32 result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a UInt32");
            
			return ReadUInt32(max, showMessageIfInvalid);
		}

				public static UInt64 ReadUInt64 (bool showMessageIfInvalid = true)
		{
			if (UInt64.TryParse(Console.ReadLine(), out UInt64 result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not UInt64");
				return ReadUInt64(showMessageIfInvalid);
			}
		}

		public static UInt64 ReadUInt64(in UInt64 max, bool showMessageIfInvalid = true)
		{
			if (UInt64.TryParse(Console.ReadLine(), out UInt64 result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a UInt64");
            
			return ReadUInt64(max, showMessageIfInvalid);
		}

				public static Byte ReadByte (bool showMessageIfInvalid = true)
		{
			if (Byte.TryParse(Console.ReadLine(), out Byte result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Byte");
				return ReadByte(showMessageIfInvalid);
			}
		}

		public static Byte ReadByte(in Byte max, bool showMessageIfInvalid = true)
		{
			if (Byte.TryParse(Console.ReadLine(), out Byte result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Byte");
            
			return ReadByte(max, showMessageIfInvalid);
		}

				public static SByte ReadSByte (bool showMessageIfInvalid = true)
		{
			if (SByte.TryParse(Console.ReadLine(), out SByte result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not SByte");
				return ReadSByte(showMessageIfInvalid);
			}
		}

		public static SByte ReadSByte(in SByte max, bool showMessageIfInvalid = true)
		{
			if (SByte.TryParse(Console.ReadLine(), out SByte result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a SByte");
            
			return ReadSByte(max, showMessageIfInvalid);
		}

				public static Double ReadDouble (bool showMessageIfInvalid = true)
		{
			if (Double.TryParse(Console.ReadLine(), out Double result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Double");
				return ReadDouble(showMessageIfInvalid);
			}
		}

		public static Double ReadDouble(in Double max, bool showMessageIfInvalid = true)
		{
			if (Double.TryParse(Console.ReadLine(), out Double result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Double");
            
			return ReadDouble(max, showMessageIfInvalid);
		}

				public static Single ReadSingle (bool showMessageIfInvalid = true)
		{
			if (Single.TryParse(Console.ReadLine(), out Single result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Single");
				return ReadSingle(showMessageIfInvalid);
			}
		}

		public static Single ReadSingle(in Single max, bool showMessageIfInvalid = true)
		{
			if (Single.TryParse(Console.ReadLine(), out Single result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Single");
            
			return ReadSingle(max, showMessageIfInvalid);
		}

				public static Decimal ReadDecimal (bool showMessageIfInvalid = true)
		{
			if (Decimal.TryParse(Console.ReadLine(), out Decimal result))
				return result;
			else
			{
				if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not Decimal");
				return ReadDecimal(showMessageIfInvalid);
			}
		}

		public static Decimal ReadDecimal(in Decimal max, bool showMessageIfInvalid = true)
		{
			if (Decimal.TryParse(Console.ReadLine(), out Decimal result))
			{
				if(result <= max)
					return result;

				else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);
			}
			else if (showMessageIfInvalid)
					ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a Decimal");
            
			return ReadDecimal(max, showMessageIfInvalid);
		}

			}
}