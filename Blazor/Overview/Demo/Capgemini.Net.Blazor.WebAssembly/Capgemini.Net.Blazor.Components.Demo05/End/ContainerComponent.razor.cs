using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo05.End
{
    public partial class ContainerComponent
    {
        private readonly RateContext rateContext = new RateContext();

        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;
    }
}
