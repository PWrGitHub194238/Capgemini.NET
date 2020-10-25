using Microsoft.AspNetCore.Components;
using static Capgemini.Net.Blazor.Components.Rate11.Rate;

namespace Capgemini.Net.Blazor.Components.Rate11.RateIcon
{
    public partial class RateIcon : ComponentBase
    {
        //[CascadingParameter(Name = nameof(TempRateValue))]
        //public int TempRateValue { get; set; }

        //[CascadingParameter(Name = nameof(Focused))]
        //public bool Focused { get; set; }

        [CascadingParameter(Name = nameof(RateContext))]
        public RateContext Context { get; set; } = default!;

        [Parameter]
        public int Rate { get; set; }

        [Parameter]
        public string ActiveCss { get; set; } = "fas";

        [Parameter]
        public string InActiveCss { get; set; } = "far";

        [Parameter]
        public int Value { get; set; }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        [Parameter]
        public string Icon { get; set; } = "fa-star";

        //public bool Focused { get; set; }
    }
}