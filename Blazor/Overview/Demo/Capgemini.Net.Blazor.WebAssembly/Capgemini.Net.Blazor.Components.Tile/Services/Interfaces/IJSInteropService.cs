using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Tile.Services.Interfaces
{
    public interface IJSInteropService
    {
        ValueTask SetSplitterPosition(DemoChecklistContext context, (double, double) splitterPosition);

        ValueTask<(double, double)> GetSplitterPosition(DemoChecklistContext context);
    }
}
