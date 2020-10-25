using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.ClickOutsideOverlay
{
    public partial class ClickOutsideOverlay
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public EventCallback OnOutsideClick { get; set; }
    }
}
