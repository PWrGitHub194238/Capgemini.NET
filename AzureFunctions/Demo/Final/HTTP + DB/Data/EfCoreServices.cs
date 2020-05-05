using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class EfCoreServices
    {
        public static void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase(databaseName: "in-memory"));
        }
    }
}
