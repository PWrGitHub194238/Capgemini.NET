using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public partial class Rate4 : Rate4Base
    {
        [CascadingParameter(Name = "MaxRate")]
        public int MaxRate { get; set; } = 5;

        [CascadingParameter]
        public string Icon { get; set; } = "fa-star";

        protected override void OnParametersSet()
        {
            if (Rate >= MaxRate)
            {
                Rate = MaxRate;
            }
        }
    }
}
