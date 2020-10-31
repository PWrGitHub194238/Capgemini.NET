using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo2
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo2";

        public static TileIcon Icon => TileIcon.CHAIN;

        public static string Title => "Rate range & icon customization: component parameters";

        public static string Description => "Introduction of an input parameters for Rate component's range and images. Container-Presenter pattern with component allowing to modify the parameter values for child Rate component.";
    }
}
