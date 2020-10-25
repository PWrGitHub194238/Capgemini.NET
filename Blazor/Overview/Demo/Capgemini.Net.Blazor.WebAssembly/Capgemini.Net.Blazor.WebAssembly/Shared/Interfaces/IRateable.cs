namespace Capgemini.Net.Blazor.WebAssembly.Shared.Interfaces
{
    public interface IRateable
    {
        int CurrentRate { get; set; }

        int AverageRate { get; set; }

        IRateRange RateRange { get; set; }
    }
}
