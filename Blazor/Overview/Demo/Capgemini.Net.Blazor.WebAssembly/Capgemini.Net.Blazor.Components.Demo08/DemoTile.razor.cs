using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo08
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo08";

        public static TileIcon Icon => TileIcon.SATISFACTION;

        public static string Title => "Blazor form validation: controlling user inputs";

        public static string Description => "Custom validation attributes, narrowing down of the allowed user inputs, adding a support for the form errors summary, attribute splatting";
    }
}
