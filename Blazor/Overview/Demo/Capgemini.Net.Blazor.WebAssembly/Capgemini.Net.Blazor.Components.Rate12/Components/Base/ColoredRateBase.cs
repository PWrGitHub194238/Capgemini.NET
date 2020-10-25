using Capgemini.Net.Blazor.Components.Rate12.Interfaces;
using Capgemini.Net.Blazor.Components.Rate12.Models;
using Microsoft.AspNetCore.Components;
using System;

namespace Capgemini.Net.Blazor.Components.Rate12.Base
{
    public class ColoredRateBase : ComponentBase
    {
        [Parameter]
        public IRateRange RateRange { get; set; } = new RateRange();

        [Parameter]
        public decimal Value { get; set; }

        [Parameter]
        public Colors MinColor { get; set; } = Colors.RED;

        [Parameter]
        public Colors MaxColor { get; set; } = Colors.GREEN;

        protected decimal Percentage => (Value - RateRange.MinRate) / (RateRange.MaxRate - RateRange.MinRate);

        public string GetHslColor(decimal percent, Colors start, Colors end) => $"hsl({(int)Math.Floor((int)start + ((int)end - (int)start) * percent)}, 100%, 50%)";
    }
}
