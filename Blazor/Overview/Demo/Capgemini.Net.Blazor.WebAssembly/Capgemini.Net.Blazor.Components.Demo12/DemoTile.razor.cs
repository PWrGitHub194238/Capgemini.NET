using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo12
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo12";

        public static TileIcon Icon => TileIcon.FLAG;

        public static string Title => "TypeScript Component";

        public static string Description => "Providing yet another layer of customization by render fragments for rate icons, ";
    }
}
