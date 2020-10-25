using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components
{
    public partial class ChecklistSidenavPoint
    {
        [Parameter]
        public DemoChecklistPointContext Context { get; set; } = default!;

        [Parameter]
        public EventCallback<DemoChecklistPointContext> PointSelected { get; set; }

        [Parameter]
        public EventCallback PointDeSelected { get; set; }

        [Parameter]
        public EventCallback<DemoChecklistPointContext> PointStateChanged { get; set; }

        [CascadingParameter]
        public DemoChecklistPointContext? Selected { get; set; }

        internal bool IsPointSelected => Selected is not null && Selected.Equals(Context);

        internal void OnPointSelected()
        {
            if (IsPointSelected)
            {
                PointDeSelected.InvokeAsync();
            } else
            {
                PointSelected.InvokeAsync(Context);
            }
        }

        internal async Task OnStateChanged(bool isDone)
        {
            Context.IsDone = isDone;
            await PointStateChanged.InvokeAsync(Context);
        }
    }
}
