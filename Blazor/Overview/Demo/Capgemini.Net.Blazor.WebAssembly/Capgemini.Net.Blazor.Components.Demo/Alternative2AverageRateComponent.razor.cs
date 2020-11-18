using System;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public partial class Alternative2AverageRateComponent : AverageRateComponentBase
    {
        private double Percentage => MaxRate - MinRate is 0 ? 0 : (double)(AvgRate - MinRate) / (MaxRate - MinRate);

        private string GetHslColor(double percent, Colors start, Colors end) => $"hsl({(int)Math.Floor((int)start + ((int)end - (int)start) * percent)}, 100%, 50%)";

        public enum Colors
        {
            RED = 0,
            ORANGE = 39,
            YELLOW = 60,
            GREEN = 120,
            TURQUOISE = 180,
            BLUE = 240,
            PINK = 300
        }
    }
}
