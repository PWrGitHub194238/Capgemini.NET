using Microsoft.AspNetCore.Components;
using System;

namespace Capgemini.Net.Blazor.Components.Demo.Examples.Demo5
{
    public partial class PointContext5 : ComponentBase
    {

        [Parameter]
        public int MaxRate { get; set; }

        [Parameter]
        public int AvgRate { get; set; }

        [Parameter]
        public int IconIndex { get; set; }

        [Parameter]
        public int Example { get; set; }

        private readonly RateContext dumyContext = new RateContext()
        {
            AvgRate = 3,
            MaxRate = 5,
            IconIndex = 1,
        };

        protected override void OnInitialized()
        {
            dumyContext.MaxRate = MaxRate;
            dumyContext.AvgRate = AvgRate;
            dumyContext.IconIndex = IconIndex;

            base.OnInitialized();
        }

        private class RateContext
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

        private static string GetAvgRateLabel(int agvRate) => agvRate switch
        {
            int rate when rate is < 3 => $"{agvRate} (poor)",
            int rate when rate is >= 3 and < 5 => $"{agvRate} (good)",
            _ => $"{agvRate} (excellent)",
        };
    }
}
