using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Services.Interfaces
{
    public interface IJSInteropService
    {
        ValueTask<IDictionary<string, bool>> GetContextPointStates(DemoChecklistContext context);

        ValueTask SetContextPointState(DemoChecklistContext context, DemoChecklistPointContext contextPoint);

        string GetContextPointKeyName(DemoChecklistContext context, DemoChecklistPointContext contextPoint);
    }
}
