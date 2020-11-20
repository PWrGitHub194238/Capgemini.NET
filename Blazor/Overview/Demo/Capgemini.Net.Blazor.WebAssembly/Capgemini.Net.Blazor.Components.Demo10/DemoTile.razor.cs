using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo10
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo10";

        public static TileIcon Icon => TileIcon.ARROWS;

        public static string Title => "Custom event callbacks: reacting on user interaction";

        public static string Description => "Synchronizing rate component values with the API, utilize POST response body, redrawing the component on user interaction, redirecting on component's initialization";
    }
}
