
using Microsoft.AspNetCore.Components;
using System;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public class AverageRateComponentBase : ComponentBase
    {
        [Parameter]
        public int MinRate { get; set; }

        [Parameter]
        public decimal AvgRate { get; set; }

        [Parameter]
        public int MaxRate { get; set; }


        protected override void OnParametersSet()
        {
            if (MinRate > 0 && AvgRate < MinRate)
            {
                throw new ArgumentOutOfRangeException(nameof(AvgRate), $"{nameof(AvgRate)} for the {nameof(AverageRateComponentBase)} component has to be greater or equal that {nameof(MinRate)}");
            } else if (MaxRate > 0 && MaxRate < AvgRate)
            {
                throw new ArgumentOutOfRangeException(nameof(AvgRate), $"{nameof(AvgRate)} for the {nameof(AverageRateComponentBase)} component has to be smaller or equal that {nameof(MaxRate)}");
            }

            base.OnParametersSet();
        }
    }
}
