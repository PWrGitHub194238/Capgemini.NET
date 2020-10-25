using Capgemini.Net.Blazor.Components.Tile.Services.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces;
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Tile
{
    public partial class FullTileContainer
    {
        [Inject]
        public IJSInteropService JSInteropService { get; set; } = default!;

        [Inject]
        public ICheckboxSideNavService CheckboxSideNavService { get; set; } = default!;

        [Parameter]
        public RenderFragment DemoEndPoint { get; set; } = default!;

        [Parameter]
        public RenderFragment DemoStartPoint { get; set; } = default!;

        [Parameter]
        public DemoChecklistContext? ChecklistContext { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            if (ChecklistContext is not null)
            {
                (leftPercentWidthDefault, rightPercentWidthDefault) = await JSInteropService.GetSplitterPosition(ChecklistContext);
            }
            await base.OnInitializedAsync();
        }

        internal async Task OnSplitterPositionChanged((double, double) splitterPosition)
        {
            if (ChecklistContext is not null)
            {
                leftPercentWidthDefault = splitterPosition.Item1 * 100;
                rightPercentWidthDefault = splitterPosition.Item2 * 100;
                await JSInteropService.SetSplitterPosition(ChecklistContext, splitterPosition);
            }
        }

        private double leftPercentWidthDefault;

        private double rightPercentWidthDefault;
    }
}
