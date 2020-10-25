using Capgemini.Net.Blazor.Shared.Interfaces.Context;

namespace Capgemini.Net.Blazor.Shared.Interfaces
{
    public interface ICheckboxSideNavService
    {
        void OpenSideNav(DemoChecklistContext context);
        
        void CloseSideNav();
    }
}
