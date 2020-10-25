using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public partial class Rate7 : Rate7Base
    {
        [Parameter]
        public RateContext RateContext { get; set; } = default!;

        protected override void OnParametersSet()
        {
            if (Rate >= RateContext.MaxRate)
            {
                Rate = RateContext.MaxRate;
            }
        }
    }
}
