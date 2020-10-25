using Capgemini.Net.Blazor.Components.Tile.Services.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Tile.Services
{
    public class JSInteropService : IJSInteropService
    {
        private const string jsNamespace = "capgemini_net_blazor_components_tile";

        private readonly IJSRuntime jSRuntime;

        public JSInteropService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
        }

        public async ValueTask SetSplitterPosition(DemoChecklistContext context, (double, double) splitterPosition)
            => await jSRuntime.InvokeVoidAsync($"{jsNamespace}.setSplitterPosition", GetContextPointKeyName(context), splitterPosition.Item1 * 100, splitterPosition.Item2 * 100);

        public async ValueTask<(double, double)> GetSplitterPosition(DemoChecklistContext context)
        {
            var result = await jSRuntime.InvokeAsync<double[]>($"{jsNamespace}.getSplitterPosition", GetContextPointKeyName(context));
            return (result[0], result[1]);
        }

        private static string GetContextPointKeyName(DemoChecklistContext context)
            => $"{context.Name}_splitter";
    }
}
