using Capgemini.Net.Blazor.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Api.Repositories.Interfaces
{
    public interface IProductRepository
    {
        ValueTask<Product> FindProductByIdAsync(int productId);

        Task<Product> GetProductByIdAsync(int productId);

        Task<List<ProductRate>> GetProductRatesByIdAsync(int productId);

        Task<List<ProductRate>> GetProductRatesByIdAsync(int productId, Expression<Func<ProductRate, bool>> predicate);

        Product RateProduct(Product product, int productRate);

        Product UpdateAvgRate(Product product, IEnumerable<ProductRate> productRates);
    }
}
