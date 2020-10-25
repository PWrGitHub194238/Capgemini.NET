using Capgemini.Net.Blazor.Components.Rate10.Interfaces;

namespace Capgemini.Net.Blazor.Components.Rate10.Models
{
    public class RateRange : IRateRange
    {
        public int MinRate { get; set; }

        public int MaxRate { get; set; }
    }
}
