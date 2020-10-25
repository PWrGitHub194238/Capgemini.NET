using Capgemini.Net.Blazor.Api.Data;
using Capgemini.Net.Blazor.Api.Models;
using Capgemini.Net.Blazor.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BlazorDbContext dbContext;

        public ProductRepository(BlazorDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public ValueTask<Product> FindProductByIdAsync(int productId)
            => dbContext.Products.FindAsync(productId);

        public Task<Product> GetProductByIdAsync(int productId)
            => dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.RateRange)
                .SingleOrDefaultAsync(p => p.Id.Equals(productId));

        public Task<List<ProductRate>> GetProductRatesByIdAsync(int productId)
            => GetProductRatesByIdAsync(productId, (_) => true);

        public Task<List<ProductRate>> GetProductRatesByIdAsync(int productId, Expression<Func<ProductRate, bool>> predicate)
            => dbContext.Products
                .Include(p => p.ProductRates)
                .Where(p => p.Id.Equals(productId))
                .SelectMany(p => p.ProductRates)
                .Where(predicate).ToListAsync();

        public Product RateProduct(Product product, int productRate)
        {
            product.ProductRates.Add(new ProductRate
            {
                Product = product,
                Rate = productRate
            });

            dbContext.Update(product);

            return product;
        }

        public Product UpdateAvgRate(Product product, IEnumerable<ProductRate> productRates)
        {
            product.AverageRate = (decimal)productRates.Average(pr => pr.Rate);

            dbContext.Update(product);

            return product;
        }
    }
}
