namespace Capgemini.Net.Blazor.Components.Demo
{
    public partial class AlternativeRouteComponent : RateComponentBase
    {
        public static readonly string HOVERED_STYLE = "hovered";

        public int TempRate { get; private set; }

        protected override void OnParametersSet()
        {
            if (Rate >= MaxRate)
            {
                Rate = MaxRate;
            }
        }

        private string GetHoveredStyle(int index) => TempRate switch
        {
            int rate when rate.Equals(index) => HOVERED_STYLE,
            _ => string.Empty,
        };
    }
}
