using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo07.Start
{
    public partial class ContainerComponent
    {
        private readonly RateContext rateContext = new RateContext();

        [Parameter]
        public RenderFragment<RateContext> ChildContent { get; set; } = default!;
    }
}
