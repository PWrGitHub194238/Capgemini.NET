using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Rate12.RateIcon
{
    public partial class RateIcon : ComponentBase
    {
        [CascadingParameter]
        public Rate Container { get; set; } = default!;

        [Parameter]
        public int Rate { get; set; }

        [Parameter]
        public string Icon { get; set; } = default!;

        [Parameter]
        public string ActiveCss { get; set; } = "fas";

        [Parameter]
        public string InActiveCss { get; set; } = "far";
    }
}