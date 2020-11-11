using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo8.Start
{
    public partial class ContainerComponent
    {
        [Parameter]
        public RenderFragment<RateContext> ChildContent { get; set; } = default!;

        [Parameter]
        public RenderFragment<AverageRateContext> AvgRate { get; set; } = default!;

        private readonly RateContext rateContext = new RateContext();

        private AverageRateContext AverageRateContext => new AverageRateContext()
        {
            MinRate = 1,
            AvgRate = rateContext.AvgRate,
            MaxRate = rateContext.MaxRate,
        };
    }
}
