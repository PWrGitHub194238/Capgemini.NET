using Capgemini.Net.Blazor.Shared.Base;

namespace Capgemini.Net.Blazor.Shared
{
    public class Product : ProductBase
    {
        public Category Category { get; set; } = new Category();
    }
}
