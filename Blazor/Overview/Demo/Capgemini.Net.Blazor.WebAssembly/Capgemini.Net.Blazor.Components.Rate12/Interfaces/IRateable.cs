namespace Capgemini.Net.Blazor.Components.Rate12.Interfaces
{
    public interface IRateable
    {
        int? CurrentRate { get; set; }

        decimal AverageRate { get; set; }
    }
}
