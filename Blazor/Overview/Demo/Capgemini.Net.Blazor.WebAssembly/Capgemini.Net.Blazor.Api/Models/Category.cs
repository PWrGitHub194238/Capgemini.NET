using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Api.Models
{
    public class Category : Shared.Category
    {
        public virtual ICollection<Product> Products { get; set; } = new Collection<Product>();
    }
}
