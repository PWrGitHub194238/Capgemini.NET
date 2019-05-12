using _17.TargetTypedNewExpressions.Utils;
using System;
using System.Drawing;

namespace _17.TargetTypedNewExpressions
{
    internal static class ConsolColours
    {
        private static readonly Random random = new Random();

        internal static void Write(char[][] charMap, int i, int j)
        {
            char field = charMap[i][j];
            Color fieldColor = Color.White;
            if (i == 0)
            {
                fieldColor = PowerShellColors.Colors[random.Next(2) == 0 ? 14 : 10];
            }
            else if (i > 8)
            {
                fieldColor = PowerShellColors.Colors[8];
            }
            else if (field.Equals('S'))
            {
                fieldColor = PowerShellColors.Colors[13];
            }
            else if (field.Equals('E'))
            {
                fieldColor = PowerShellColors.Colors[12];
            }
            else if (field.Equals('#'))
            {
                if (!charMap[i - 1][j].Equals('#'))
                {
                    fieldColor = PowerShellColors.Colors[1];
                }
                else
                {
                    fieldColor = PowerShellColors.Colors[3];
                }
            }
            Colorful.Console.Write(field, fieldColor);
        }
    }
}
