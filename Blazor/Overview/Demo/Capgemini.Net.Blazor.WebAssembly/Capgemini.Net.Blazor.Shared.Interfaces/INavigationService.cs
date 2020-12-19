namespace Capgemini.Net.Blazor.Shared.Interfaces
{
    public interface INavigationService
    {
        void ChangeProduct(int productId);

        void CloseSideNav();

        void CloseSubSideNav();

        void OpenSideNav();

        void OpenSubSideNav(int checklistPointIndex);

        bool ShouldOpenSideNav();

        bool ShouldOpenSubSideNav(int checklistPointIndex);
    }
}
