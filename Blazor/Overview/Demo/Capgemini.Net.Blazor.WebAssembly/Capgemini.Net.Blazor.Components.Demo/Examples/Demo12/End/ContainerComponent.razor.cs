using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo.Examples.Demo12.End
{
    public partial class ContainerComponent
    {
        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        private IRateableProduct product = new RateableProductAdapter(new RateableProductViewModel()
        {
            MinRate = 1,
            CurrentRate = 2,
            AverageRate = 3.5M,
            MaxRate = 5,
        });

        public void ChangeProductRate(int productRate) => product.CurrentRate = productRate;
    }
}
