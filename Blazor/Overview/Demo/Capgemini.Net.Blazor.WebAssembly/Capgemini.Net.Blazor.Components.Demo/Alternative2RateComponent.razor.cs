using System;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public partial class Alternative2RateComponent : RateComponentBase
    {
        private static readonly Random rng = new Random();

        private static readonly string[] colors = {
            "80b8d6",
            "88d5ed",
            "c8ff16",
            "00c37b",
            "15596b",
            "0f999c",
            "01d1d0",
            "ff6327",
            "ff7e83",
            "cb2980",
            "860864",
            "6d64cc",
            "7e39ba",
            "4701a7",
            "0070ad",
            "12abdb",
            "ff304c",
            "ececec",
        };

        public int TempRate { get; private set; } = int.MinValue;

        protected override void OnParametersSet()
        {
            if (Rate >= MaxRate)
            {
                Rate = MaxRate;
            }
        }

        private string GetHoveredStyle(int index) => TempRate switch
        {
            int rate when rate.Equals(index) => $"color: #{GetRandomColor()}",
            _ => string.Empty,
        };

        private static string GetRandomColor() => colors[rng.Next(0, colors.Length - 1)];
    }
}
