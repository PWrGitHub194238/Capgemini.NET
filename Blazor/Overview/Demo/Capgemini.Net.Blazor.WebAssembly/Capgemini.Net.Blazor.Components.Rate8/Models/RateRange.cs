using Capgemini.Net.Blazor.Components.Rate9.Interfaces;

namespace Capgemini.Net.Blazor.Components.Rate9.Models
{
    public class RateRange : IRateRange
    {
        public int MinRate { get; set; }

        public int MaxRate { get; set; }
    }
}
