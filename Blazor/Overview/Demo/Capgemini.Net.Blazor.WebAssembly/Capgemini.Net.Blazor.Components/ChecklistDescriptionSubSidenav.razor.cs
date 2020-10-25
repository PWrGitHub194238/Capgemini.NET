using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components
{
    public partial class ChecklistDescriptionSubSidenav
    {
        [Parameter]
        public DemoChecklistPointContext? Context { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }
    }
}
