using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public partial class Rate8 : Rate8Base
    {
        [Parameter]
        public IRateContext RateContext { get; set; } = default!;

        protected override void OnParametersSet()
        {
            if (Rate >= RateContext.MaxRate)
            {
                Rate = RateContext.MaxRate;
            }
        }
    }
}
