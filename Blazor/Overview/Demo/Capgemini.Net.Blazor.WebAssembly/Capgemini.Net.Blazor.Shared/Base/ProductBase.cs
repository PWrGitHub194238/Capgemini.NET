using Capgemini.Net.Blazor.Shared.Interfaces;

namespace Capgemini.Net.Blazor.Shared.Base
{
    public class ProductBase : IRateable
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;

        public decimal AverageRate { get; set; }
    }
}
