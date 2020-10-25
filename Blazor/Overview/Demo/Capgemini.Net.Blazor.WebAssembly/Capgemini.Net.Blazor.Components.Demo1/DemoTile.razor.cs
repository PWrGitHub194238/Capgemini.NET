namespace Capgemini.Net.Blazor.Components.Demo1
{
    public partial class DemoTile
    {
        public const string Href = "demo1";

        public static TileIcon Icon => TileIcon.PAINT_BRUSH;

        public static string Title => "Demo 1";

        public static string Description => "Non-blazor component using standard HTML event attributes to call JavaScript function on trigger.";
    }
}
