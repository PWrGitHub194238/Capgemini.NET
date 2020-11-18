using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo06
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo06";

        public static TileIcon Icon => TileIcon.DIRECTION;

        public static string Title => "Generic render fragment: give a child content a context of the parent";

        public static string Description => "Replacing cascading values with a generic type of render fragments. Providing context of a parent component to child content.";
    }
}
