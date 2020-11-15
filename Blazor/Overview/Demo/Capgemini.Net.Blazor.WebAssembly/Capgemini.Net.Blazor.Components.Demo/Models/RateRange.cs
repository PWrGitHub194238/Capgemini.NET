using Capgemini.Net.Blazor.Components.Demo.Interfaces;

namespace Capgemini.Net.Blazor.Components.Demo.Models
{
    public class RateRange : IRateRange
    {
        public int MinRate { get; set; }

        public int MaxRate { get; set; }
    }
}
