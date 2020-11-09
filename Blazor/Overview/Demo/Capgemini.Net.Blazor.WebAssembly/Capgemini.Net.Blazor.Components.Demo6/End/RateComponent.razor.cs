using Capgemini.Net.Blazor.Components.Demo;

namespace Capgemini.Net.Blazor.Components.Demo6.End
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
