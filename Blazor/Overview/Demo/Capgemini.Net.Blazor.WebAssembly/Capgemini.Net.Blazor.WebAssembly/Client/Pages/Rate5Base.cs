using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public class Rate5Base : ComponentBase
    {
        public static string ACTIVE_STYLE = "fas";

        public static string INACTIVE_STYLE = "far";

        private int rate;

        protected int Rate
        {
            get => rate + 1;
            set
            {
                rate = value - 1;
                tempRate = rate;
            }
        }

        protected int tempRate = 0;

        protected bool IsActive(int index) => index <= tempRate;

        protected void SetRate() => rate = tempRate;

        protected void ShowRate(int index) => tempRate = index;

        protected void RevertRate() => tempRate = rate;
    }
}
