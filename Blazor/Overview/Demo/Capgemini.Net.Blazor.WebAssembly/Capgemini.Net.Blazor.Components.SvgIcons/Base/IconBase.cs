using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Capgemini.Net.Blazor.Components.SvgIcons.Base
{
    public class IconBase : ComponentBase
    {
        [Parameter]
        public IconTheme Theme { get; set; } = IconTheme.LIGHT;

        [Parameter]
        public string Height { get; set; } = default!;

        [Parameter]
        public string Width { get; set; } = default!;

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }
    }
}
