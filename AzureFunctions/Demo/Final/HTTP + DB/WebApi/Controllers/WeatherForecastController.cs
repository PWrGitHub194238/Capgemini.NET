namespace WebApi.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync() => await dbContext.WeatherForecasts.AsNoTracking().ToListAsync();

        [HttpPost]
        public async void PostAsync([FromQuery] int howMany)
        {
            await dbContext.WeatherForecasts.AddRangeAsync(WeatherForecast.GenerateData(howMany));
            await dbContext.SaveChangesAsync();
        }
    }
}
