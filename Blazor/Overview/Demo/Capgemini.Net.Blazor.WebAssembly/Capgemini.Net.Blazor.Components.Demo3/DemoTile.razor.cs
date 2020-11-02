using Capgemini.Net.Blazor.Components.Tile.Base;

namespace Capgemini.Net.Blazor.Components.Demo3
{
    public partial class DemoTile : DemoTileBase
    {
        public const string Href = "demo3";

        public static TileIcon Icon => TileIcon.DIAGRAM;

        public static string Title => "Basic example refactoring: code-behind and CSS isolation";

        public static string Description => "Separating @code block to its own code-behind *.cs class enables more intellisense for .NET code along with IDE support for snippets/overrides. CSS isolation enables to work around CSS selector ambiguity with other styles in other components/assemblies.";
    }
}
