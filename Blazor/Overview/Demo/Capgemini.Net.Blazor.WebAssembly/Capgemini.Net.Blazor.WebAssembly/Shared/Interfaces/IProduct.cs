namespace Capgemini.Net.Blazor.WebAssembly.Shared.Interfaces
{
    public interface IProduct : IRateable
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}
