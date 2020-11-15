using Capgemini.Net.Blazor.Components.Demo.Interfaces;

namespace Capgemini.Net.Blazor.Components.Demo.Models
{
    public class RateableProduct : IRateableProduct
    {
        public int? CurrentRate { get; set; }

        public decimal AverageRate { get; set; }

        public int MinRate { get; set; }

        public int MaxRate { get; set; }
    }
}
