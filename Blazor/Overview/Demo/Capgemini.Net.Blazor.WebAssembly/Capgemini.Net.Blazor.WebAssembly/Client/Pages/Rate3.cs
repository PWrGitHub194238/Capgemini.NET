using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public partial class Rate3 : Rate3Base
    {
        [Parameter]
        public int MaxRate { get; set; } = 5;

        [Parameter]
        public string Icon { get; set; } = "fa-star";

        public override Task SetParametersAsync(ParameterView parameters)
        {
            MyProperty = 5;
            return base.SetParametersAsync(parameters);
        }

        protected override async Task OnParametersSetAsync()
        {
            var temp = MaxRate;
            MaxRate = 1;
            await Task.Delay(3000);
            MaxRate = 2;
            await Task.Delay(3000);
            MaxRate = temp;
            if (Rate >= MaxRate)
            {
                Rate = MaxRate;
            }
        }
    }
}
