using System.Linq;
using Capgemini.Net.Blazor.Api.Repositories;
using Capgemini.Net.Blazor.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Capgemini.Net.Blazor.Api.Data
{
    public static class Setup
    {
        public static void AddSqlServerRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BlazorDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        public static void AddSqlServerRepositories(this IServiceCollection services, IConfiguration configuration, string connectionStringKey)
            => AddSqlServerRepositories(services, configuration.GetConnectionString(connectionStringKey));

        public static void AddSqlServerRepositories(this IServiceCollection services, IConfiguration configuration)
            => AddSqlServerRepositories(services, configuration, "SqlServer");

        public static void SeedDatabase(this IApplicationBuilder app)
        {
            BlazorDbContext context = app.ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<BlazorDbContext>();

            Migrate(context);

            SeedRateRanges(context);
            SeedCategories(context);
            SeedProducts(context);
        }

        private static void Migrate(BlazorDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
        private static void SeedRateRanges(BlazorDbContext context)
        {
            if (!context.RateRanges.Any())
            {
                AddInitialRateRanges(context);
                context.SaveChanges();
            }
        }

        private static void AddInitialRateRanges(BlazorDbContext context)
        {
            context.RateRanges.AddRange(
                InitialData.RateRanges
            );
        }

        private static void SeedCategories(BlazorDbContext context)
        {
            if (!context.Categories.Any())
            {
                AddInitialCategories(context);
                context.SaveChanges();
            }
        }

        private static void AddInitialCategories(BlazorDbContext context)
        {
            context.Categories.AddRange(
                InitialData.Categories
            );
        }

        private static void SeedProducts(BlazorDbContext context)
        {
            if (!context.Products.Any())
            {
                AddInitialProducts(context);
                context.SaveChanges();
            }
        }

        private static void AddInitialProducts(BlazorDbContext context)
        {
            context.Products.AddRange(
                InitialData.Products
            );
        }
    }
}