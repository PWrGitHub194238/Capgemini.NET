using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public partial class Rate5 : Rate5Base
    {
        [CascadingParameter]
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
