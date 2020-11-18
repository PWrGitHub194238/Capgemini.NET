using System;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public partial class AlternativeAverageRateComponent : AverageRateComponentBase
    {
        private string GetAvgRateLabel => DisplayStyleType switch
        {
            DisplayStyle.DEFAULT => $"{AvgRate} ({Label})",
            DisplayStyle.RATE_ONLY => $"{AvgRate}",
            DisplayStyle.LABEL_ONLY => $"{Label}",
            _ => $"{AvgRate} ({Label})",
        };

        private double Percentage => MaxRate - MinRate is 0 ? 0 : (double)(AvgRate - MinRate) / (MaxRate - MinRate);

        private string Label => Labels[Math.Max(0,(int)Math.Floor(Labels.Length * Percentage) - 1)];

        public enum DisplayStyle
        {
            DEFAULT,
            RATE_ONLY,
            LABEL_ONLY,
        }
    }
}
