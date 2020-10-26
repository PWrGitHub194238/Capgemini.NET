namespace Capgemini.Net.Blazor.Components.Demo11
{
    public partial class DemoTile
    {
        public const string Href = "demo11";

        public static TileIcon Icon => TileIcon.PAINT_BRUSH;

        public static string Title => "TypeScript Component";

        public static string Description => "Non-Blazor component using standard HTML event attributes to call JavaScript function on trigger.";
    }
}
