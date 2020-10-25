using Capgemini.Net.Blazor.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Capgemini.Net.Blazor.Api.Data
{
    public class BlazorDbContext : DbContext
    {
        public BlazorDbContext(DbContextOptions<BlazorDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(nameof(Product.Price)).HasColumnType("decimal(8, 2)");
            modelBuilder.Entity<Product>().Property(nameof(Product.AverageRate)).HasColumnType("decimal(8, 2)");
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryFK);
            modelBuilder.Entity<Product>().HasOne(p => p.RateRange).WithMany(rr => rr.Products).HasForeignKey(p => p.RateRangeFK);
            modelBuilder.Entity<Product>().HasMany(p => p.ProductRates).WithOne(pr => pr.Product).HasForeignKey(pr => pr.ProductFK);

            modelBuilder.Entity<Category>().HasKey(p => p.Name);
            modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryFK);

            modelBuilder.Entity<RateRange>().HasKey(r => r.Id);
            modelBuilder.Entity<RateRange>().HasMany(r => r.Products).WithOne(p => p.RateRange).HasForeignKey(p => p.RateRangeFK);

            modelBuilder.Entity<ProductRate>().HasKey(r => r.Id);
            modelBuilder.Entity<ProductRate>().HasOne(pr => pr.Product).WithMany(p => p.ProductRates).HasForeignKey(pr => pr.ProductFK);
        }

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<RateRange> RateRanges => Set<RateRange>();

        public DbSet<ProductRate> ProductRates => Set<ProductRate>();
    }
}
