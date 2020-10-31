using Capgemini.Net.Blazor.Components.Services.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Tile.Base
{
    public class DemoTileBase : TileBase
    {
        [Inject]
        public IJSInteropService JSInteropService { get; set; } = default!;

        [Parameter]
        public int CompletedPointCount { get; set; }

        [Parameter]
        public int PointCount { get; set; }

        public async Task CloseTile(DemoChecklistContext context)
        {
            await CalculateTileCompletion(context);
            await TileClosed.InvokeAsync();
        }

        private async Task CalculateTileCompletion(DemoChecklistContext context)
        {
            IDictionary<string, bool> contextPointStates = await JSInteropService.GetContextPointStates(context);
            CompletedPointCount = contextPointStates.Count(_ => _.Value);
            PointCount = contextPointStates.Count;
        }
    }
}
