using Capgemini.Net.Blazor.Components.Services.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {

        [Inject]
        public IJSInteropService JSInteropService { get; set; } = default!;

        [Inject]
        public ICheckboxSideNavService CheckboxSideNavService { get; set; } = default!;

        [Inject]
        public INavigationService NavigationService { get; set; } = default!;

        internal string OpenClosedSideNavCssState => demoChecklist is null ? "closed" : "opened";

        internal string OpenClosedSubSideNavCssState => pointContext is null ? "closed" : "opened";

        internal DemoChecklistContext? demoChecklist;

        internal DemoChecklistPointContext? pointContext;

        internal static bool[] isExpanded = new bool[12];

        public void OpenSideNav(DemoChecklistContext context) => InvokeAsync(() =>
        {
            demoChecklist = context;
            NavigationService.OpenSideNav();
            StateHasChanged();
        });

        public void CloseSideNav() => InvokeAsync(() =>
        {
            demoChecklist = null;
            NavigationService.CloseSideNav();
            StateHasChanged();
        });

        protected override void OnInitialized()
        {
            ((CheckboxSideNavService)CheckboxSideNavService).Bound(this);
            base.OnInitialized();
        }

        internal void OnPointDeSelected() => InvokeAsync(() =>
        {
            pointContext = null;
            NavigationService.CloseSubSideNav();
            StateHasChanged();
        });

        internal void OnPointSelected(DemoChecklistPointContext selectedContext) => InvokeAsync(() =>
        {
            pointContext = selectedContext;
            NavigationService.OpenSubSideNav(selectedContext.Order);
            StateHasChanged();
        });

        internal void OnSideNavClosed()
        {
            if (pointContext is not null)
            {
                OnSubSideNavClosed();
            }
            demoChecklist = null;
            NavigationService.CloseSideNav();
        }

        internal void OnSubSideNavClosed()
        {
            pointContext = null;
            NavigationService.CloseSubSideNav();
        }

        internal static string GetTileCssClass(int tileIndex) {
            return isExpanded[tileIndex - 1] ? "demo-tile demo-tile-opened" : $"demo-tile demo-tile-{tileIndex}-closed";
        }

        internal static void OnTileOpened(int tileIndex)
        {
            isExpanded[tileIndex - 1] = true;
        }

        internal static void OnTileClosed(int tileIndex)
        {
            isExpanded[tileIndex - 1] = false;
        }
    }
}
