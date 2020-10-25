using Capgemini.Net.Blazor.Components.Services.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Extensions
{
    public static class DemoChecklistContextExtensions
    {
        public static async Task<DemoChecklistContext> UpdatePointStatesFromLocalStore(this DemoChecklistContext context, IJSInteropService jSInteropService)
        {
            IDictionary<string, bool> pointStates = await jSInteropService.GetContextPointStates(context);

            context.Points = new Collection<DemoChecklistPointContext>(
                    context.Points.Select(p => UpdatePointStateFromLocalStore(jSInteropService, pointStates, context, p))
                        .ToList());

            return context;
        }

        private static DemoChecklistPointContext UpdatePointStateFromLocalStore(
            IJSInteropService jSInteropService,
            IDictionary<string, bool> pointStates,
            DemoChecklistContext Context,
            DemoChecklistPointContext contextPoint)
        {
            string key = jSInteropService.GetContextPointKeyName(Context, contextPoint);

            if (pointStates.ContainsKey(key))
            {
                contextPoint.IsDone = pointStates[key];
            }

            return contextPoint;
        }
    }
}
