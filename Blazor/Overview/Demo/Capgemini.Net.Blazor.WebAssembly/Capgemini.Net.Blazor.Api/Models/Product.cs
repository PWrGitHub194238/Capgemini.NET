using Capgemini.Net.Blazor.Shared.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Api.Models
{
    public class Product : ProductBase
    {
        public int Id { get; set; }

        public string CategoryFK { get; set; } = string.Empty;

        public virtual Category Category { get; set; } = new Category();

        public int RateRangeFK { get; set; }

        public virtual RateRange RateRange { get; set; } = new RateRange();

        public virtual ICollection<ProductRate> ProductRates { get; set; } = new Collection<ProductRate>();
    }
}
