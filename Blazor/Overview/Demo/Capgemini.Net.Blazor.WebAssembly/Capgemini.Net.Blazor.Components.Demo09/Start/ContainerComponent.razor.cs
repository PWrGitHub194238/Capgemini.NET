using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Capgemini.Net.Blazor.Components.Demo.Models;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo09.Start
{
    public partial class ContainerComponent
    {
        private readonly IRateableProduct product = new RateableProduct()
        {
            MinRate = 2,
            AverageRate = 3,
            MaxRate = 6,
            CurrentRate = 3,
        };

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;
    }
}
