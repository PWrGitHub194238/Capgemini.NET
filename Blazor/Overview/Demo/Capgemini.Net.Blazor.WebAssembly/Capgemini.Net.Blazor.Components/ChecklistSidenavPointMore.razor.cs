using Capgemini.Net.Blazor.Components.SvgIcons.Base;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components
{
    public partial class ChecklistSidenavPointMore
    {

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Selected { get; set; }

        [Parameter]
        public EventCallback OnClick { get; set; }

        internal string OpenClosedCssState => Selected ? "opened" : "closed";

        internal IconTheme Theme => Disabled ? IconTheme.DISABLED : IconTheme.DARK;

        internal void OnClickHandler()
        {
            if (!Disabled)
            {
                OnClick.InvokeAsync();
            }
        }
    }
}
