using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo12
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo12";

        public static TileIcon Icon => TileIcon.FLAG;

        public static string Title => "Cascading components: generate child content for rate icons as render fragments on runtime";

        public static string Description => "Capturing the logic for a cascading value with its own component, wrapping any child content, using self-reference as cascading parameter to call logic of the parent component form render fragment's content.";
    }
}
