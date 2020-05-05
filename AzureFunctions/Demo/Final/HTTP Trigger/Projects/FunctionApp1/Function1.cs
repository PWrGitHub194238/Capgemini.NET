using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;

namespace Http
{
    public static class Function1
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /* Entry point for the function
        *
        * Every static method with that attribute will be discover during program's startup.
        *
        * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-dotnet-class-library
        */
        [FunctionName("Function1")]
        public static IEnumerable<WeatherForecast> Run(
            /* One of many available attributes that describes by which source/event the Azure Function will be triggered
            *
            * Attribute for triggering the method upon incoming HTTP request with an optionally defined Route.
            * Here we can configure an AuthorizationLevel (does the sender of the request is required to include 
            * a 'x-functions-key' header with a system/master API key), a set of HTTP methods to be accepted
            * and a Route parameter that defines our HTTP route with optional parameters.
            * If not provided the FunctionName will be used.
            *
            * Returns HttpRequest object with all data about the request that triggers the function.
            * 
            * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk-extensions/blob/master/src/WebJobs.Extensions.Http/HttpTriggerAttribute.cs
            * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-http-webhook
            */
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "WeatherForecast")]
            HttpRequest req,
            ILogger log)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; internal set; }
        public int TemperatureC { get; internal set; }
        public string Summary { get; internal set; }
    }
}
