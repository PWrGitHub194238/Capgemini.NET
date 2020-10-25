using Capgemini.Net.Blazor.WebAssembly.Shared.Interfaces;

namespace Capgemini.Net.Blazor.WebAssembly.Shared
{
    public class ProductBase : IProduct
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        int IRateable.CurrentRate { get; set; }

        int IRateable.AverageRate { get; set; }

        IRateRange IRateable.RateRange { get; set; }
    }
}
