namespace Capgemini.Net.Blazor.Components.Demo
{
    public partial class AlternativeRateComponent : RateComponentBase
    {
        public static readonly string HOVERED_STYLE = "hovered";

        public int TempRate { get; private set; } = int.MinValue;

        protected override void OnParametersSet()
        {
            if (Rate >= MaxRate)
            {
                Rate = MaxRate;
            }
        }

        private string GetHoveredClass(int index) => TempRate switch
        {
            int rate when rate.Equals(index) => HOVERED_STYLE,
            _ => string.Empty,
        };
    }
}
