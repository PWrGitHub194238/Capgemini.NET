using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo4
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo4";

        public static TileIcon Icon => TileIcon.RECYCLE;

        public static string Title => "Improving components' re-usability: cascading values and render fragment";

        public static string Description => "Using of RenderFragment parameter type to provide custom child components. Use CascadingValue Razor component to provide context values for nested components.";
    }
}
