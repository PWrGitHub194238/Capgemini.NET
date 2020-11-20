using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo11
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo11";

        public static TileIcon Icon => TileIcon.NETWORKING;

        public static string Title => "TypeScript Component";

        public static string Description => "Non-Blazor component using standard HTML event attributes to call JavaScript function on trigger.";
    }
}
