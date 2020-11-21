using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo11
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo11";

        public static TileIcon Icon => TileIcon.NETWORKING;

        public static string Title => "Custom value binding: encapsulating logic for the rate selection icons";

        public static string Description => "Replace base logic for icon rating, create an icon rate component wit multiple custom event callbacks and parameters, replacing parameter-callback pairs with data binding.";
    }
}
