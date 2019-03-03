using System;
using System.Drawing;

namespace _13.RangesAndIndices.Utils
{
    internal static class ConsolColours
    {
        internal static void WrapPowerShellColors()
        {
            Color powershellBackgroundColor = Color.FromArgb(0, 1, 33, 84);
            Color powershellForegroundColor = Color.FromArgb(0, 204, 204, 204);

            Colorful.Console.Write(" ", powershellBackgroundColor);
            Colorful.Console.Write(" ", powershellForegroundColor);

            Console.Write('\r');
        }
    }
}
