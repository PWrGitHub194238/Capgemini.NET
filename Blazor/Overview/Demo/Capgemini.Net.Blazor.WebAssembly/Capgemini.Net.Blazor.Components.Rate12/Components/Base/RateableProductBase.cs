using Capgemini.Net.Blazor.Components.Rate12.Interfaces;
using Capgemini.Net.Blazor.Components.Rate12.Models;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Rate12.Base
{
    public class RateableProductBase : ComponentBase
    {
        [Parameter]
        public IRateableProduct Product { get; set; } = new RateableProduct();
    }
}
