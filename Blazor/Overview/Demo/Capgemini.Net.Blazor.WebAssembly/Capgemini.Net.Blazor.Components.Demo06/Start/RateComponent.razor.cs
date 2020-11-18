using Capgemini.Net.Blazor.Components.Demo;

namespace Capgemini.Net.Blazor.Components.Demo06.Start
{
    public partial class RateComponent : RateComponentBase
    {
        protected override void OnParametersSet()
        {
            if (Rate >= RateContext.MaxRate)
            {
                Rate = RateContext.MaxRate;
            }
        }
    }
}
