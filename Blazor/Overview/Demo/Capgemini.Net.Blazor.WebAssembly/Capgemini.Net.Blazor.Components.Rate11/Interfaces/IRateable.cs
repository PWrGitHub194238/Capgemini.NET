namespace Capgemini.Net.Blazor.Components.Rate11.Interfaces
{
    public interface IRateable
    {
        int? CurrentRate { get; set; }

        decimal AverageRate { get; set; }
    }
}
