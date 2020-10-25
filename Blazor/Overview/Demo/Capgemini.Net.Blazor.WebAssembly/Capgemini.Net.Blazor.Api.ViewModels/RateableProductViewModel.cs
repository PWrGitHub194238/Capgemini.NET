using Capgemini.Net.Blazor.Shared;

namespace Capgemini.Net.Blazor.Api.ViewModels
{
    public class RateableProductViewModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public Category? Category { get; set; }

        public int? CurrentRate { get; set; }

        public decimal AverageRate { get; set; }

        public int MinRate { get; set; }

        public int MaxRate { get; set; }
    }
}
