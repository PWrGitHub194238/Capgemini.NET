using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Capgemini.Net.Blazor.Components.Demo.Models;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo9.End
{
    public partial class ContainerComponent
    {
        private readonly IRateableProduct product = new RateableProduct();

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        //[Parameter]
        //public int Id { get; set; }

        //private IRateableProduct product = default!;

        //protected override async Task OnInitializedAsync()
        //{
        //    var rateableProductViewModel = await httpClient.GetFromJsonAsync<RateableProductViewModel>($"products/{Id}");

        //    if (rateableProductViewModel is not null)
        //    {
        //        product = new RateableProduct9Adapter(rateableProductViewModel);
        //    }
        //}
    }
}
