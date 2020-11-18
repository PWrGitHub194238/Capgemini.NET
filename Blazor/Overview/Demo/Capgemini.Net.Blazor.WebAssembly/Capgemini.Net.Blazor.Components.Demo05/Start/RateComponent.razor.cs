using Capgemini.Net.Blazor.Components.Demo;

namespace Capgemini.Net.Blazor.Components.Demo05.Start
{
    public partial class RateComponent : RateComponentBase
    {
        protected override void OnParametersSet()
        {
            if (Rate >= MaxRate)
            {
                Rate = MaxRate;
            }
        }
    }
}
