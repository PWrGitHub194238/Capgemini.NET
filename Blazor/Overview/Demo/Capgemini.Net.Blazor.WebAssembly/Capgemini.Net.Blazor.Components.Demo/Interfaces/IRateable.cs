namespace Capgemini.Net.Blazor.Components.Demo.Interfaces
{
    public interface IRateable
    {
        int? CurrentRate { get; set; }

        decimal AverageRate { get; set; }
    }
}
