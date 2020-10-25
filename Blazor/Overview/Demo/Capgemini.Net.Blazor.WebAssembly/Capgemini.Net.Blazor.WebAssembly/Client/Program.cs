using System;
using System.Net.Http;
using System.Threading.Tasks;
using Capgemini.Net.Blazor.Components.Services;
using Capgemini.Net.Blazor.Shared.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Capgemini.Net.Blazor.WebAssembly.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(sp => 
                new HttpClient { BaseAddress = new Uri("http://localhost:5002/api/") });

            builder.Services.AddSingleton<Components.Services.Interfaces.IJSInteropService, JSInteropService>();
            builder.Services.AddSingleton<Components.Splitter.Services.Interfaces.IJSInteropService, Components.Splitter.Services.JSInteropService>();
            builder.Services.AddSingleton<Components.Tile.Services.Interfaces.IJSInteropService, Components.Tile.Services.JSInteropService>();

            builder.Services.AddScoped<ICheckboxSideNavService, CheckboxSideNavService>();

            await builder.Build().RunAsync();
        }
    }
}
