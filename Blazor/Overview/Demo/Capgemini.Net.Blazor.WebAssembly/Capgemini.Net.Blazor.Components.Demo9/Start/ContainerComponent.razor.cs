using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Capgemini.Net.Blazor.Components.Demo.Models;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo9.Start
{
    public partial class ContainerComponent
    {
        private readonly IRateableProduct product = new RateableProduct();

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;
    }
}
