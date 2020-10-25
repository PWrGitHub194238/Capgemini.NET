using Capgemini.Net.Blazor.Components.Splitter.Base;
using Capgemini.Net.Blazor.Components.Splitter.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Splitter.Services
{
    public class JSInteropService : IJSInteropService
    {
        private const string jsNamespace = "capgemini_net_blazor_components_splitter";

        private readonly IJSRuntime jSRuntime;

        public JSInteropService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
        }

        public async ValueTask<DOMRect> GetBoundingClientRect(ElementReference containerRef)
            => await jSRuntime.InvokeAsync<DOMRect>($"{jsNamespace}.getBoundingClientRect", containerRef);
    }
}
