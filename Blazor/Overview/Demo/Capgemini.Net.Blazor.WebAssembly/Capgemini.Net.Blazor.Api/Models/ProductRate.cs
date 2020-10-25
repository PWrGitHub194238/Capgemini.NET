namespace Capgemini.Net.Blazor.Api.Models
{
    public class ProductRate
    {
        public int Id { get; set; }

        public int ProductFK { get; set; }

        public virtual Product Product { get; set; } = new Product();

        public int Rate { get; set; }
    }
}
