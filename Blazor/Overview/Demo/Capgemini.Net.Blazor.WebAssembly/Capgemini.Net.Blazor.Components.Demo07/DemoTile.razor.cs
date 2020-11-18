using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo07
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo07";

        public static TileIcon Icon => TileIcon.PUZZLE;

        public static string Title => "Multiple component fragments: customization of the component by replacing child content elements";

        public static string Description => "Decomposition of the RateComponent. Multiple generic render fragments usage. Allowing the component to be further customize without modifying the component's markup.";
    }
}
