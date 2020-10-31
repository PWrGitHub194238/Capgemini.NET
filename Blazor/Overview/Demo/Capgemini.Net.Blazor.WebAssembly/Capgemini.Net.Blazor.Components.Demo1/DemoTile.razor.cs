using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo1
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo1";

        public static TileIcon Icon => TileIcon.HOME;

        public static string Title => "Rate component: from JavaScript through TypeScript to Blazor";

        public static string Description => "Non-Blazor component using standard HTML event attributes to call JavaScript function on trigger. Introduction of Blazor events, one-way binding and @code blocks.";
    }
}
