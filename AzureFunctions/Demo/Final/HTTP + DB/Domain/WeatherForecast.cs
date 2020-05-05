using Newtonsoft.Json;
using System;
using System.Linq;

namespace WebApi
{
    public class WeatherForecast
    {
        public static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // Defines a mapping for the defined partition key inside the Cosmos DB database and collection of WeatherForecasts.
        // This attribute allows the results returned from the database to be mapped into the WeatherForecast objects.
        [JsonProperty(PropertyName = "id")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "temperatoure-c")]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        public static WeatherForecast[] GenerateData(int howMany)
        {
            var rng = new Random();
            return Enumerable.Range(1, howMany).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index).ToShortDateString(),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
