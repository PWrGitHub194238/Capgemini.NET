using Capgemini.Net.Blazor.Components.Rate10.Interfaces;
using Capgemini.Net.Blazor.Components.Rate10.Models;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Rate10.Base
{
    public class RateableProductBase : ComponentBase
    {
        [Parameter]
        public IRateableProduct Product { get; set; } = new RateableProduct();
    }
}
