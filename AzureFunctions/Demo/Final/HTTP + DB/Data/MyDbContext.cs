namespace Data
{
    using Microsoft.EntityFrameworkCore;
    using WebApi;

    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> dbContextOptions) : base(options: dbContextOptions)
        {

        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>().HasKey(w => new { w.Date, w.TemperatureC, w.Summary });
            base.OnModelCreating(modelBuilder);
        }
    }
}
