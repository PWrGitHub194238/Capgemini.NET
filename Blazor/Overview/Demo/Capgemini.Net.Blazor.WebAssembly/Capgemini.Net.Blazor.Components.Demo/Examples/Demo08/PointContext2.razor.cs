using Capgemini.Net.Blazor.Components.Demo.Examples.Demo08.End;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo.Examples.Demo08
{
    public partial class PointContext2
    {
        [Parameter]
        public int Example { get; set; }

        private readonly RateContext rateContext = new RateContext()
        {
            MaxRate = 3
        };
    }
}
