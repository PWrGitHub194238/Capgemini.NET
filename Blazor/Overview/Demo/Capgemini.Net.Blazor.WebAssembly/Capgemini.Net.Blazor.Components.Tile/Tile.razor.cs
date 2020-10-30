using Capgemini.Net.Blazor.Components.Tile.Base;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Tile
{
    public partial class Tile : DemoTileBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Parameter]
        public TileIcon Icon { get; set; }

        [Parameter]
        public string Title { get; set; } = default!;

        [Parameter]
        public string Description { get; set; } = default!;

        [Parameter]
        public string Href { get; set; } = default!;

        [CascadingParameter(Name = "NavigationChildContent")]
        public RenderFragment ChildContent { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            if (ShowContent)
            {
                await TileOpened.InvokeAsync();
            }
            else
            {
                await TileClosed.InvokeAsync();
            }
            await  base.OnInitializedAsync();
        }

        internal bool IsExpanded { get; set; }

        internal bool IsCollapsed { get; set; }

        internal bool IsCurrentlyNavigated => NavigationManager.ToBaseRelativePath(NavigationManager.Uri).ToLowerInvariant()
            .CompareTo(Href.ToLowerInvariant()) == 0;

        internal bool ShowContent => !IsCollapsed && IsCurrentlyNavigated || IsExpanded;

        internal string OpenClosedCssState => ShowContent ? "opened" : "closed";

        internal string OpenClosedCssStyle => IsCurrentlyNavigated || IsExpanded ? "z-index: 850;" : string.Empty;

        internal string TileIconPath => $"_content/Capgemini.Net.Blazor.Components.Tile/img/cap-tile-icons/{Icon.ToString().ToLowerInvariant().Replace("_", "-")}.png";

        internal void ToggleTitle() {
            if (ShowContent)
            {
                CollapseTitle();
            } else
            {
                ExpandTitle();
            }
        }
        internal async void ExpandTitle() {
            IsExpanded = true;
            IsCollapsed = false;
            await TileOpened.InvokeAsync();
            await Task.Delay(500);
            NavigationManager.NavigateTo(Href);
        }

        internal async void CollapseTitle()
        {
            IsExpanded = false;
            IsCollapsed = true;
            await TileClosed.InvokeAsync();
            await Task.Delay(500);
            NavigationManager.NavigateTo("/");
        }
    }
}
