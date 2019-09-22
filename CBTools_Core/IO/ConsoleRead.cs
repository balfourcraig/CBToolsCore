using CBTools_Core.Extensions;
using System;


namespace CBTools_Core.IO
{
    public static partial class ConsoleRead
    {
        private const string TOO_HIGH = "Input value too high";

        public static int ReadSingleDigitNumberInstant(bool showMessageIfInvalid = true, bool intercept = false)
        {
            char input = Console.ReadKey(true).KeyChar;
            if (input.IsNumeric())
            {
                if (!intercept)
                    Console.WriteLine(input);
                return input - 48;
            }
            else if (showMessageIfInvalid)
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a number");

            return ReadSingleDigitNumberInstant(showMessageIfInvalid, intercept);
        }

        public static int ReadSingleDigitNumberInstant(int max, bool showMessageIfInvalid = true, bool intercept = false)
        {
            char input = Console.ReadKey(true).KeyChar;

            if (input.IsNumeric())
            {
                if ((input - 48) <= max)
                {
                    if (!intercept)
                        Console.WriteLine(input);
                    return input - 48;
                }
                else if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", TOO_HIGH);

            }
            else if (showMessageIfInvalid)
                ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Not a number");

            return ReadSingleDigitNumberInstant(max, showMessageIfInvalid, intercept);
        }

        public static string ReadNonWhitespace(bool showMessageIfInvalid = true)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                if (showMessageIfInvalid)
                    ConsoleWrite.WriteLinesColored(ConsoleColor.Red, "Invalid input", "Input was null of whitespace");
                return ReadNonWhitespace(showMessageIfInvalid);
            }
            else
                return input;
        }

        public static bool ReadIfNotFlag(out string input, string flag = "#", bool acceptWhitespace = false, bool showMessageIfInvalid = true)
        {
            if (acceptWhitespace)
                input = Console.ReadLine();
            else
                input = ReadNonWhitespace(showMessageIfInvalid);

            return input != flag;
        }

        public static void LoopAction(Action<string> action, string flag = "#")
        {
            while (ReadIfNotFlag(out string input, flag))
            {
                action(input);
            }
        }
    }
}
