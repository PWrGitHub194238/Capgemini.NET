using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Tile.Base
{
    public class TileBase : ComponentBase
    {
        [Parameter]
        public EventCallback TileOpened { get; set; }


        [Parameter]
        public EventCallback TileClosed { get; set; }
    }
}
