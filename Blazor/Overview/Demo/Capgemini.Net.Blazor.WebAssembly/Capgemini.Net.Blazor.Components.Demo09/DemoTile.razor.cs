using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo09
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo09";

        public static TileIcon Icon => TileIcon.DOWNLOAD;

        public static string Title => "Use HttpClient in Blazor: fetching data for the API";

        public static string Description => "Injecting HttpClient from Dependency Injection Container, handling incomplete asynchronous actions at render, usage of the NavigationManager, default markup for RenderFragments.";
    }
}
