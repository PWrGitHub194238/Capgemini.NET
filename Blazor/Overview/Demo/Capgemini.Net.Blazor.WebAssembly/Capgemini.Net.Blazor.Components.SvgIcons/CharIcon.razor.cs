using Capgemini.Net.Blazor.Components.SvgIcons.Base;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.SvgIcons
{
    public partial class CharIcon : IconBase
    {
        [Parameter]
        public char Icon { get; set; }

        [Parameter]
        public int X { get; set; } = 13;

        [Parameter]
        public int Y { get; set; } = 25;

        protected override void OnParametersSet()
        {
            Height ??= "36px";
            Width ??= "36px";
            base.OnParametersSet();
        }
    }
}
