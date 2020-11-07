using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components
{
    public partial class CapInput
    {
        [Parameter]
        public string Label { get; set; } = default!;

        [Parameter]
        public string InputName { get; set; } = default!;

        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;
    }
}
