using System;

namespace Capgemini.Net.Blazor.Components.Demo08.Start
{
    public class RateContext
    {
        private int maxRate = 6;
        private int avgRate = 3;
        private int iconIndex;

        public static readonly string[] Icons = {
            "fa-star",
            "fa-grin-stars",
            "fa-angry",
            "fa-sun"
        };

        public string Icon => Icons[iconIndex];

        public int MaxRate
        {
            get => maxRate;
            set => maxRate = Math.Max(2, value);
        }

        public int AvgRate
        {
            get => avgRate;
            set => avgRate = Math.Max(1, Math.Min(MaxRate, value));
        }

        public int IconIndex
        {
            get => iconIndex;
            set => iconIndex = value < 0
                ? Icons.Length - 1
                : value >= Icons.Length
                ? 0
                : value;
        }
    }
}
