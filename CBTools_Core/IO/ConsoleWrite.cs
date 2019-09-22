using System;
using System.Collections.Generic;
using System.Text;

namespace CBTools_Core.IO
{
    public static class ConsoleWrite
    {
        public static void WriteLines(params string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
                Console.WriteLine(lines[i]);
        }

        public static void WriteLinesColored(ConsoleColor foregroundColor, params string[] lines)
        {
            Console.ForegroundColor = foregroundColor;
            for (int i = 0; i < lines.Length; i++)
                Console.WriteLine(lines[i]);

            Console.ResetColor();
        }

        public static void WriteLinesColored(ConsoleColor foregroundColor, ConsoleColor backgroundcolor, params string[] lines)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundcolor;
            for (int i = 0; i < lines.Length; i++)
                Console.WriteLine(lines[i]);

            Console.ResetColor();
        }

        public static void WriteColored(ConsoleColor foregroundcolor, string text)
        {
            Console.ForegroundColor = foregroundcolor;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteColored(ConsoleColor foregroundcolor, ConsoleColor backgroundcolor, string text)
        {
            Console.ForegroundColor = foregroundcolor;
            Console.BackgroundColor = backgroundcolor;
            Console.Write(text);
            Console.ResetColor();
        }


        public static void WriteBlocksLine(params (string contents, ConsoleColor foreground, ConsoleColor background)[] blocks)
        {

            foreach ((string, ConsoleColor, ConsoleColor)tup in blocks)
            {
                (string contents, ConsoleColor foreground, ConsoleColor background) = tup;
                Console.ForegroundColor = foreground;
                Console.BackgroundColor = background;
                Console.Write(contents);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void WriteBlocks(ConsoleColor foreground, ConsoleColor background, string contents)
        {
            WriteBlocksLine((contents, foreground, background));
        }

        public static void CharLine(char paddingChat = '*', ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("".PadRight(Console.BufferWidth - 1, paddingChat));
            Console.ResetColor();
        }
    }
}
