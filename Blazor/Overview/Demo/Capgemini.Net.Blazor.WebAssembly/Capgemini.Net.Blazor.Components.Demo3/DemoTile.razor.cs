namespace Capgemini.Net.Blazor.Components.Demo3
{
    public partial class DemoTile
    {
        public const string Href = "demo3";

        public static TileIcon Icon => TileIcon.PAINT_BRUSH;

        public static string Title => "TypeScript Component";

        public static string Description => "Non-Blazor component using standard HTML event attributes to call JavaScript function on trigger.";
    }
}
