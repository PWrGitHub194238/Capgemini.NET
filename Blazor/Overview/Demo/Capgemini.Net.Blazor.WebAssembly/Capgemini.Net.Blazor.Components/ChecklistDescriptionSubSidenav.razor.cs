using Capgemini.Net.Blazor.Components.Services.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components
{
    public partial class ChecklistDescriptionSubSidenav
    {
        [Inject]
        public IJSInteropService JSInteropService { get; set; } = default!;

        [Parameter]
        public DemoChecklistPointContext? Context { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        internal ElementReference? DescriptionDevElementRef { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (DescriptionDevElementRef is not null)
            {
                await JSInteropService.HighlightAllUnderWithPrism(DescriptionDevElementRef.Value);
            }

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
