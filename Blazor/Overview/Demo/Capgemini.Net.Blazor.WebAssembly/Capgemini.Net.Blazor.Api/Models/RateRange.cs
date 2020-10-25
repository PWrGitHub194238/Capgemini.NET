using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Api.Models
{
    public class RateRange : Shared.RateRange
    {
        public int Id { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new Collection<Product>();
    }
}
