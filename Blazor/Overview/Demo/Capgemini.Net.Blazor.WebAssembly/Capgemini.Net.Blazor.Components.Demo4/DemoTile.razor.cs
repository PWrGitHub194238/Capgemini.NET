using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo4
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo4";

        public static TileIcon Icon => TileIcon.PAINT_BRUSH;

        public static string Title => "TypeScript Component";

        public static string Description => "Non-Blazor component using standard HTML event attributes to call JavaScript function on trigger.";
    }
}
