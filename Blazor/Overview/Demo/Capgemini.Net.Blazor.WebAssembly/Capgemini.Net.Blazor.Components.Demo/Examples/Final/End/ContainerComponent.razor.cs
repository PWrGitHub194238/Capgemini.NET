using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo.Examples.Final.End
{
    public partial class ContainerComponent
    {
        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        private IRateableProduct product = new RateableProductAdapter(new RateableProductViewModel()
        {
            MinRate = 1,
            CurrentRate = 4,
            AverageRate = 8.5M,
            MaxRate = 12,
        });

        public void ChangeProductRate(int productRate) => product.CurrentRate = productRate;
    }
}
