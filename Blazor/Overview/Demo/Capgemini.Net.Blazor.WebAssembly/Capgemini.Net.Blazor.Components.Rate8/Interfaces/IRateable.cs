namespace Capgemini.Net.Blazor.Components.Rate9.Interfaces
{
    public interface IRateable
    {
        int CurrentRate { get; set; }

        decimal AverageRate { get; set; }
    }
}
