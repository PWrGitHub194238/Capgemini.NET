namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public class RateContextNoAttributes : IRateContext
    {
        public int MaxRate { get; set; } = 5;

        public string Icon { get; set; } = "fa-star";

        public int AvgRate { get; set; } = 1;

    }
}
