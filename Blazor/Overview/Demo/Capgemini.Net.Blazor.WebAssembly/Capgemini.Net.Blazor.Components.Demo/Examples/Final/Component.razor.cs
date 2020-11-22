using Capgemini.Net.Blazor.Components.Demo.Examples.Final.End;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo.Examples.Final
{
    public partial class Component
    {
        [Parameter]
        public int Example { get; set; }

        private ContainerComponent? ContainerComponentRef;

        public void ChangeProductRate(int productRate)
        {
            if (ContainerComponentRef is not null)
            {
                ContainerComponentRef.ChangeProductRate(productRate);
            }
        }

        private static string GetRateIcon(int productRate, int rateRange) => productRate switch
        {
            int rate when rate <= rateRange * 0.2 * 1 => "fa-angry",
            int rate when rateRange * 0.2 * 1 < rate
                && rate <= rateRange * 0.2 * 2 => "fa-sad-tear",
            int rate when rateRange * 0.2 * 2 < rate
                && rate <= rateRange * 0.2 * 3 => "fa-meh-blank",
            int rate when rateRange * 0.2 * 3 < rate
                && rate <= rateRange * 0.2 * 4 => "fa-smile-beam",
            int rate when rateRange * 0.2 * 4 < rate
                && rate <= rateRange * 0.2 * 5 => "fa-grin-stars",
            _ => "fa-grin-stars",
        };
    }
}
