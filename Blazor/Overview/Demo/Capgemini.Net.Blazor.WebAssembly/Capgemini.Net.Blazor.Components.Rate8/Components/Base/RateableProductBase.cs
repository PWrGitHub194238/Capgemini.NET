using Capgemini.Net.Blazor.Components.Rate9.Interfaces;
using Capgemini.Net.Blazor.Components.Rate9.Models;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Rate9.Base
{
    public class RateableProductBase : ComponentBase
    {
        [Parameter]
        public IRateableProduct Product { get; set; } = new RateableProduct();
    }
}
