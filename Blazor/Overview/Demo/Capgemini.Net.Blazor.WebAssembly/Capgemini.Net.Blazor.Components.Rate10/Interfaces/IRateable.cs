namespace Capgemini.Net.Blazor.Components.Rate10.Interfaces
{
    public interface IRateable
    {
        int? CurrentRate { get; set; }

        decimal AverageRate { get; set; }
    }
}
