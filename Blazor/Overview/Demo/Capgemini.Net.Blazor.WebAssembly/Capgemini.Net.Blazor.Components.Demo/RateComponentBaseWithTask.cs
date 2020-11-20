using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public class RateComponentBaseWithTask : ComponentBase
    {
        public static readonly string ACTIVE_STYLE = "fas";

        public static readonly string INACTIVE_STYLE = "far";

        private int rate = 0;

        private int tempRate = 0;

        protected int Rate
        {
            get => rate + 1;
            set
            {
                rate = value - 1;
                tempRate = rate;
            }
        }

        protected virtual Task SetRate()
        {
            rate = tempRate;
            return Task.CompletedTask;
        }

        protected void ShowRate(int index) => tempRate = index;

        protected void RevertRate() => tempRate = rate;

        protected bool IsActive(int index) => index <= tempRate;
    }
}
