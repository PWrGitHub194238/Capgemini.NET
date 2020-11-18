using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo.Examples.Demo09
{
    public partial class PointContext4
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Parameter]
        public int Example { get; set; }

        [Parameter]
        public int ProductId { get; set; }
    }
}
