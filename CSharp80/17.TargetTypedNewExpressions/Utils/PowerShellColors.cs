using System.Drawing;

namespace _17.TargetTypedNewExpressions.Utils
{
    public static class PowerShellColors
    {
        public static Color[] Colors = new Color[]
        {
            // Color.FromArgb(0, 0, 0, 0),
            Color.FromArgb(0, 0, 0, 128),
            Color.FromArgb(0, 0, 128, 0),
            Color.FromArgb(0, 0, 128, 128),
            Color.FromArgb(0, 128, 0, 0),
            Color.FromArgb(0, 1, 36, 86),
            Color.FromArgb(0, 238, 237, 240),
            Color.FromArgb(0, 192, 192, 192),
            Color.FromArgb(0, 128, 128, 128),
            Color.FromArgb(0, 0, 0, 255),
            Color.FromArgb(0, 0, 255, 0),
            Color.FromArgb(0, 0, 255, 255),
            Color.FromArgb(0, 255, 0, 0),
            Color.FromArgb(0, 255, 0, 255),
            Color.FromArgb(0, 255, 255, 0),
            Color.FromArgb(0, 255, 255, 255)
        };

        public static void WrapPowerShellColors()
        {
            foreach (Color color in Colors)
            {
                Colorful.Console.Write(' ', color);
            }
            Colorful.Console.Write('\r');
        }
    }
}
