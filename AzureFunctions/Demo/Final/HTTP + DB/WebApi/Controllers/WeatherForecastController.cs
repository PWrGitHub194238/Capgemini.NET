using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    /* API entry point for any HTTP requests.
    *
    * Simple class marked as an API entry point for any HTTP request with defined route of 'api/[controller]'.
    * This class will accept any HTTP request asking for the endpoint on the 'api/WeatherForecast' path.
    */
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(MyDbContext dbContext, ILogger<WeatherForecastController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        /* Method to be called on particular GET request.
        *
        * This method is marked to be called on a default route ('api/[controller]')
        * only by the HTTP GET requests (HttpGet attribute)
        * without prior authorization (AllowAnonymous attribute).
        *
        * Once called the method will tap into the WeatherForecasts table in the database specified by the dbContext
        * and will return all of them asynchronously.
        */
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<WeatherForecast>> GetAsync() => await dbContext.WeatherForecasts.AsNoTracking().ToListAsync();

        /* Method to be called on particular POST request.
        *
        * This method is marked to be called on a default route ('api/[controller]')
        * only by the HTTP POST requests (HttpPost attribute)
        * without prior authorization (AllowAnonymous attribute).
        *
        * Once called the method will randomly generate as many WeatherForecast objects
        * as was defined in the 'howMany' query parameter, will add the range of those objects
        * for tracking and will add those entries to the database asynchronously.
        */
        [HttpPost]
        [AllowAnonymous]
        public async void PostAsync([FromQuery] int howMany)
        {
            await dbContext.WeatherForecasts.AddRangeAsync(WeatherForecast.GenerateData(howMany));
            await dbContext.SaveChangesAsync();
        }
    }
}
