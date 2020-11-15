namespace Capgemini.Net.Blazor.Components.Demo9.Start
{
    public class RateContext
    {
        public static readonly string[] Icons = {
            "fa-star",
            "fa-grin-stars",
            "fa-angry",
            "fa-sun"
        };

        public int MaxRate { get; set; } = 6;

        public int AvgRate { get; set; } = 3;

        public string Icon { get; set; } = Icons[0];
    }
}
