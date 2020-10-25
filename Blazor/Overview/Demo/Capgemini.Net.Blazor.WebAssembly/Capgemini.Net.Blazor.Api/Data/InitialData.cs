using Capgemini.Net.Blazor.Api.Models;

namespace Capgemini.Net.Blazor.Api.Data
{
    public static class InitialData
    {
        public static RateRange[] RateRanges = new[]
{
            new RateRange
            {
               MinRate = 1,
               MaxRate = 5,
            },
            new RateRange
            {
               MinRate = 1,
               MaxRate = 10,
            },
        };

        public static Category[] Categories = new[]
        {
            new Category
            {
                Name = "Watersports",
            },
            new Category
            {
                Name = "Soccer",
            },
            new Category
            {
                Name = "Chess",
            },
        };

        public static Product[] Products = new[]
        {
            new Product
            {
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275,
                Category = Categories[0],
                AverageRate = 1,
                RateRange = RateRanges[0],
            },
            new Product
            {
                Name = "Lifejacket",
                Description = "Protective and fashionable",
                Price = 48.95m,
                Category = Categories[0],
                AverageRate = 2,
                RateRange = RateRanges[0],
            },
            new Product
            {
                Name = "Soccer Ball",
                Description = "FIFA-approved size and weight",
                Price = 19.50m,
                Category = Categories[1],
                AverageRate = 3,
                RateRange = RateRanges[0],
            },
            new Product
            {
                Name = "Corner Flags",
                Description = "Give your playing field a professional touch",
                Price = 34.95m,
                Category = Categories[1],
                AverageRate = 4,
                RateRange = RateRanges[0],
            },
            new Product
            {
                Name = "Stadium",
                Description = "Flat-packed 35,000-seat stadium",
                Price = 79500,
                Category = Categories[1],
                AverageRate = 5,
                RateRange = RateRanges[0],
            },
            new Product
            {
                Name = "Thinking Cap",
                Description = "Improve brain efficiency by 75%",
                Price = 16,
                Category = Categories[2],
                AverageRate = 2.56m,
                RateRange = RateRanges[1],
            },
            new Product
            {
                Name = "Unsteady Chair",
                Description = "Secretly give your opponent a disadvantage",
                Price = 29.95m,
                Category = Categories[2],
                AverageRate = 1.28m,
                RateRange = RateRanges[1],
            },
            new Product
            {
                Name = "Human Chess Board",
                Description = "A fun game for the family",
                Price = 75,
                Category = Categories[2],
                AverageRate = 2,
                RateRange = RateRanges[1],
            },
            new Product
            {
                Name = "Bling-Bling King",
                Description = "Gold-plated, diamond-studded King",
                Price = 1200,
                Category = Categories[2],
                AverageRate = 3,
                RateRange = RateRanges[1],
            }
        };
    }
}
