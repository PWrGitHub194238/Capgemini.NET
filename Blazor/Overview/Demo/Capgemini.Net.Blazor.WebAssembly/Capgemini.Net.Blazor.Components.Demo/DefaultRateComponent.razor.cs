namespace Capgemini.Net.Blazor.Components.Demo
{
    public partial class DefaultRateComponent : RateComponentBase
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
