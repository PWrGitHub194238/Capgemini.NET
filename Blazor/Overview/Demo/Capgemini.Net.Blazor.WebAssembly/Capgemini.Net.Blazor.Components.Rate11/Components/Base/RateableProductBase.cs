using Capgemini.Net.Blazor.Components.Rate11.Interfaces;
using Capgemini.Net.Blazor.Components.Rate11.Models;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Rate11.Base
{
    public class RateableProductBase : ComponentBase
    {
        [Parameter]
        public IRateableProduct Product { get; set; } = new RateableProduct();
    }
}
