using Capgemini.Net.Blazor.Components.Rate12.Interfaces;

namespace Capgemini.Net.Blazor.Components.Rate12.Models
{
    public class RateableProduct : IRateableProduct
    {
        public int? CurrentRate { get; set; }

        public decimal AverageRate { get; set; }

        public int MinRate { get; set; }

        public int MaxRate { get; set; }
    }
}
