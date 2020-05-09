using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi;

namespace HttpTrigger
{
    public static class WeatherForecastFunc
    {
        private const string DATABASE_NAME = "weather-forecast-db";
        private const string COLECTION_NAME = "weather-forecasts";
        private const string CONNECTION_STRING_KEY = "CosmosDbConnectionString";

        /* Entry point for the function
        *
        * Every static method with that attribute will be discover during program's startup.
        *
        * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-dotnet-class-library
        */
        [FunctionName("WeatherForecastFunc")]
        public static async Task<IActionResult> Run(
            /* One of many available attributes that describes by which source/event the Azure Function will be triggered
            *
            * Attribute for triggering the method upon incoming HTTP request with an optionally defined Route.
            * Here we can configure an AuthorizationLevel (does the sender of the request is required to include 
            * a 'x-functions-key' header with a system/master API key), a set of HTTP methods to be accepted
            * and a Route parameter that defines our HTTP route with optional parameters.
            * If not provided the FunctionName will be used.
            *
            * This trigger accepts both HTTP GET and HTTP POST methods but executes differently
            * based on IsGet(HttpRequest) and IsPost(HttpRequest) methods.
            *
            * Returns HttpRequest object with all data about the request that triggers the function.
            * 
            * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk-extensions/blob/master/src/WebJobs.Extensions.Http/HttpTriggerAttribute.cs
            * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-http-webhook
            */
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", "POST", Route = "WeatherForecast")]
            HttpRequest req,
            /* One of many available attributes that describes an input binding for the parameter.
            *
            * This attribute defines the input binding in the way that weatherForecastsIn parameter of type IEnumerable
            * will be fed by any WeatherForecast documents found in the defined CosmosDB No-SQL document collection 
            * of the given name COLECTION_NAME returned from the database with the given name DATABASE_NAME.
            * Providing that the given ConnectionString is valid for the given database, the selected  document collection
            * will be queried for all documents that exists in that particular collection ('SELECT * FROM c').
            * Each of the documents returned from the given database collection will be mapped to WeatherForecast objects.
            *
            * ConnectionStringSetting value defines a key in 'local.settings.json' file that stores all settings for the Azure Function locally.
            * From that file the value from the key-value pair with the given key will be used as a connection string to the database.
            * 
            * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk-extensions/blob/dev/src/WebJobs.Extensions.CosmosDB/CosmosDBAttribute.cs
            * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-cosmosdb-v2-input?tabs=csharp
            */
            [CosmosDB(databaseName: DATABASE_NAME,
                collectionName: COLECTION_NAME,
                ConnectionStringSetting = CONNECTION_STRING_KEY,
                SqlQuery = "SELECT * FROM c")]
            IEnumerable<WeatherForecast> weatherForecastsIn,
            /* One of many available attributes that describes an output binding for the parameter.
            *
            * This attribute defines the output binding in the way that weatherForecastsOut parameter of type IAsyncCollector
            * will be used to feed  the defined CosmosDB No-SQL document collection 
            * of the given name COLECTION_NAME with all the objects added with AddAsync() method.
            * As name of the parameter type suggests it will serve as a collector for objects to be added
            * to the database with the given name DATABASE_NAME once the method is executed.
            *
            * ConnectionStringSetting value defines a key in 'local.settings.json' file that stores all settings for the Azure Function locally.
            * From that file the value from the key-value pair with the given key will be used as a connection string to the database.
            * 
            * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk-extensions/blob/dev/src/WebJobs.Extensions.CosmosDB/CosmosDBAttribute.cs
            * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-cosmosdb-v2-output?tabs=csharp
            */
            [CosmosDB(
                databaseName: DATABASE_NAME,
                collectionName: COLECTION_NAME,
                ConnectionStringSetting = CONNECTION_STRING_KEY)]
            IAsyncCollector<WeatherForecast> weatherForecastsOut,
            ILogger log)
        {
            // On a HTTP GET request just return a result set of WeatherForecasts returned by the 'SELECT * FROM c' query
            // where 'c' is a 'weather-forecast' collection of documents from the 'weather-forecast-db' database.
            if (IsGet(req))
            {
                return new OkObjectResult(weatherForecastsIn);
            }

            // On a HTTP POST request verify that the request defines a 'howMany' query parameter and if it does,
            // for that many times generate a random WeatherForecast and add its entry to the 'weather-forecast' collection of documents.
            if (IsPost(req) && IsHowManyDefined(req, out int howMany))
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

        private static bool IsHowManyDefined(HttpRequest req, out int howMany)
        {
            howMany = 0;
            return req.Query.ContainsKey("howMany") && int.TryParse(req.Query["howMany"], out howMany);
        }
    }
}
