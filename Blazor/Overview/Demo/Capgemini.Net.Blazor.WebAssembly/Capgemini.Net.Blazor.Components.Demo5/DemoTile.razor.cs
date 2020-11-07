using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo5
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo5";

        public static TileIcon Icon => TileIcon.STORAGE;

        public static string Title => "Build-in components: using Blazor forms to modify custom model for a component";

        public static string Description => "Wrapping parameter set for RateComponent with custom model. Adding average rate property for component. Usage of EditForm Blazor component with build-in cascading value to modify object properties.";
    }
}
