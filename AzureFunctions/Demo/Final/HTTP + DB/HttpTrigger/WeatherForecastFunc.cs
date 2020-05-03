namespace HttpTrigger
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebApi;

    public static class WeatherForecastFunc
    {
        private const string DATABASE_NAME = "weather-forecast-db";
        private const string COLECTION_NAME = "weather-forecasts";
        private const string CONNECTION_STRING_KEY = "CosmosDbConnectionString";

        [FunctionName("WeatherForecastFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", "POST", Route = "WeatherForecast")]
            HttpRequest req,
            [CosmosDB(databaseName: DATABASE_NAME,
                collectionName: COLECTION_NAME,
                ConnectionStringSetting = CONNECTION_STRING_KEY,
                SqlQuery = "SELECT * FROM c")]
            IEnumerable<WeatherForecast> weatherForecastsIn,
            [CosmosDB(
                databaseName: DATABASE_NAME,
                collectionName: COLECTION_NAME,
                ConnectionStringSetting = CONNECTION_STRING_KEY)]
            IAsyncCollector<WeatherForecast> weatherForecastsOut,
            ILogger log)
        {
            if (IsGet(req))
            {
                return new OkObjectResult(weatherForecastsIn);
            }

            if (IsPost(req) && req.Query.ContainsKey("howMany") && int.TryParse(req.Query["howMany"], out int howMany))
            {
                foreach (WeatherForecast weatherForecast in WeatherForecast.GenerateData(howMany))
                {
                    await weatherForecastsOut.AddAsync(weatherForecast);
                }

                return new OkResult();
            }

            return new NotFoundResult();
        }


        private static bool IsGet(HttpRequest req) => IsHttp(req, "GET");

        private static bool IsPost(HttpRequest req) => IsHttp(req, "POST");

        private static bool IsHttp(HttpRequest req, string httpMethod) => req.Method.ToUpperInvariant().Equals(httpMethod.ToUpperInvariant());
    }
}
