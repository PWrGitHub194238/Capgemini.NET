using Capgemini.Net.Blazor.Components.Services.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Services
{
    public class JSInteropService : IJSInteropService
    {
        private const string jsNamespace = "capgemini_net_blazor_components";

        private readonly IJSRuntime jSRuntime;

        public JSInteropService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
        }

        public async ValueTask<IDictionary<string, bool>> GetContextPointStates(DemoChecklistContext context)
            => await jSRuntime.InvokeAsync<IDictionary<string, bool>>($"{jsNamespace}.getContextPointStates", new {
                context.Name,
                Points = context.Points.Select(p
                    => new {
                        p.Name,
                        p.IsDone,
                    })
                });

        public async ValueTask SetContextPointState(DemoChecklistContext context, DemoChecklistPointContext contextPoint)
            => await jSRuntime.InvokeVoidAsync($"{jsNamespace}.setContextPointState", GetContextPointKeyName(context, contextPoint), contextPoint.IsDone);

        public string GetContextPointKeyName(DemoChecklistContext context, DemoChecklistPointContext contextPoint)
            => $"{context.Name}_{contextPoint.Name}";

        public async ValueTask HighlightAllUnderWithPrism(ElementReference elementReference)
            => await jSRuntime.InvokeVoidAsync("Prism.highlightAllUnder", elementReference);
    }
}
