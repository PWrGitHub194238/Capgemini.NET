namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public interface IRateContext
    {
        int MaxRate { get; set; }

        string Icon { get; set; }

        int AvgRate { get; set; }

    }
}
