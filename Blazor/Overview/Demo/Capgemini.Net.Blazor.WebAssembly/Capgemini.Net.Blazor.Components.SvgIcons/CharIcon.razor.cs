using Capgemini.Net.Blazor.Components.SvgIcons.Base;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.SvgIcons
{
    public partial class CharIcon : IconBase
    {
        [Parameter]
        public char Icon { get; set; }

        protected override void OnParametersSet()
        {
            Height ??= "36px";
            Width ??= "36px";
            base.OnParametersSet();
        }
    }
}
