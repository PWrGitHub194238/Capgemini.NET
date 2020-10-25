using Capgemini.Net.Blazor.Components.Extensions;
using Capgemini.Net.Blazor.Components.Services.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components
{
    public partial class ChecklistSidenav
    {
        [Inject]
        public IJSInteropService JSInteropService { get; set; } = default!;

        [Parameter]
        public DemoChecklistContext? Context { get; set; }

        [Parameter]
        public EventCallback<DemoChecklistPointContext> PointSelected { get; set; }

        [Parameter]
        public EventCallback PointDeSelected { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Context is not null)
            {
                Context = await Context.UpdatePointStatesFromLocalStore(JSInteropService);
            }
            await base.OnInitializedAsync();
        }

        public async void OnPointSelected(DemoChecklistPointContext selectedPoint)
        {
            await PointDeSelected.InvokeAsync();
            await Task.Delay(200);
            await PointSelected.InvokeAsync(selectedPoint);
        }

        internal async Task OnPointStateChanged(DemoChecklistPointContext selectedPoint)
        {
            if (Context is not null)
            {
                await JSInteropService.SetContextPointState(Context, selectedPoint);
            }
        }
    }
}
