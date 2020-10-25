using Capgemini.Net.Blazor.Shared.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Capgemini.Net.Blazor.WebAssembly.Client.Shared;

namespace Capgemini.Net.Blazor.WebAssembly.Client
{
    public class CheckboxSideNavService : ICheckboxSideNavService
    {
        private MainLayout checkboxSideNavLayout = default!;

        public void Bound(MainLayout mainLayout)
        {
            checkboxSideNavLayout = mainLayout;
        }

        public void OpenSideNav(DemoChecklistContext context) => checkboxSideNavLayout?.OpenSideNav(context);

        public void CloseSideNav() => checkboxSideNavLayout?.CloseSideNav();
    }
}
