using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Capgemini.Net.Blazor.Api.Data;
using Capgemini.Net.Blazor.Api.Models;
using Capgemini.Net.Blazor.Api.Repositories.Interfaces;
using Capgemini.Net.Blazor.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capgemini.Net.Blazor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly BlazorDbContext dbContext;
        private readonly IProductRepository productRepository;

        public ProductsController(BlazorDbContext dbContext, IProductRepository productRepository)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        [Route("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetProductCount() => await dbContext.Products.CountAsync();

        [HttpGet]
        [Route("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RateableProductViewModel>> GetProductByIdAsync([FromRoute] int productId)
        {
            Product product = await productRepository.GetProductByIdAsync(productId);

            if (product is null)
            {
                return NotFound();
            }

            IEnumerable<ProductRate> productRates = await productRepository.GetProductRatesByIdAsync(productId);

            return new RateableProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                // We are going to test it as a single user, so it will do the trick
                CurrentRate = productRates.LastOrDefault()?.Rate,
                AverageRate = product.AverageRate,
                MinRate = product.RateRange.MinRate,
                MaxRate = product.RateRange.MaxRate,
            };
        }

        [HttpPost]
        [Route("{productId}/rate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RateableProductViewModel>> RateProductAsync(
            [FromRoute] int productId,
            [FromBody] int productRate,
            CancellationToken cancellationToken)
        {
            Product product = await productRepository.GetProductByIdAsync(productId);

            if (product is null)
            {
                return NotFound();
            }

            IEnumerable<ProductRate> productRates = await productRepository.GetProductRatesByIdAsync(productId);

            product.ProductRates = (ICollection<ProductRate>)productRates;

            productRepository.RateProduct(product, productRate);

            productRepository.UpdateAvgRate(product, productRates);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new RateableProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                // We are going to test it as a single user, so it will do the trick
                CurrentRate = productRate,
                AverageRate = product.AverageRate,
                MinRate = product.RateRange.MinRate,
                MaxRate = product.RateRange.MaxRate,
            };
        }
    }
}