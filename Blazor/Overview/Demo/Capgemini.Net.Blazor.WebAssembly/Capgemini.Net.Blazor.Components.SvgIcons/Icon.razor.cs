using Capgemini.Net.Blazor.Components.SvgIcons.Base;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.SvgIcons
{
    public partial class Icon : IconBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;

        public string CssThemeName => Theme switch
        {
            IconTheme.LIGHT => "light-background",
            IconTheme.DARK => "dark-background",
            IconTheme.DISABLED => "disabled",
            _ => "disabled",
        };
    }
}
