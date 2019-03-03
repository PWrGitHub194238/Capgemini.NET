using _11.NullableReferenceTpes.System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace _11.NullableReferenceTpes.Utils
{
    public class ConsolColours
    {
        internal static void WrapPowerShellColors()
        {
            Color powershellBackgroundColor = Color.FromArgb(0, 1, 33, 84);
            Color powershellForegroundColor = Color.FromArgb(0, 204, 204, 204);

            Colorful.Console.Write(" ", powershellBackgroundColor);
            Colorful.Console.Write(" ", powershellForegroundColor);

            Console.Write('\r');
        }

        private static Queue<Color> GetAsciiFontColours(int numberOfLines = 3)
        {
            Queue<Color> fontColourQueue = new Queue<Color>();
            int numberOfConsoleLines = 4 * numberOfLines;
            int colourDelta = 255 / numberOfConsoleLines;

            for (int i = 0; i < numberOfConsoleLines; i += 1)
            {
                fontColourQueue.Enqueue(
                    Color.FromArgb(
                        255 - colourDelta * Math.Abs(numberOfConsoleLines / 2 - i),
                        255 - colourDelta * Math.Abs(numberOfConsoleLines / 2 - i),
                        255));
            }
            return fontColourQueue;
        }

        private static Colorful.ColorAlternator GetColorAlternatorForLine(Queue<Color> colourQueue)
    => new Colorful.ColorAlternatorFactory()
            .GetAlternator(1, new Color[] {
                        colourQueue.Peek(),
                        colourQueue.ShiftOne().Peek(),
                        colourQueue.ShiftOne().Peek(),
                        colourQueue.ShiftOne().Peek()
            });

        public static void PrintFancyText(string[] lines, int colourSpeed = 1, int delay = 200)
        {
            WrapPowerShellColors();

            (int cl, int ct) = (Console.CursorLeft, Console.CursorTop);
            Queue<Color> fontColourQueue = GetAsciiFontColours(numberOfLines: lines.Length);

            while (true)
            {
                foreach (string line in lines)
                {
                    Colorful.Console.WriteAsciiAlternating(line,
                        GetColorAlternatorForLine(colourQueue: fontColourQueue));
                }
                Console.SetCursorPosition(
                    cl,
                    ct);
                fontColourQueue.Shift(colourSpeed);
                Thread.Sleep(delay);
            }
        }
    }
}