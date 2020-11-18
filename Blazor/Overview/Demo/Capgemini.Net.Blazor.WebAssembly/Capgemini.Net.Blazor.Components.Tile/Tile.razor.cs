using Capgemini.Net.Blazor.Components.Tile.Base;
using Microsoft.AspNetCore.Components;
using System.Globalization;
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

        internal bool IsCurrentlyNavigated => NavigationManager
            .ToBaseRelativePath(NavigationManager.Uri).ToLowerInvariant()
            .StartsWith(Href.ToLowerInvariant());

        internal bool ShowContent => !IsCollapsed && IsCurrentlyNavigated || IsExpanded;

        internal string OpenClosedCssState => ShowContent ? "opened" : "closed";

        internal string OpenClosedCssStyle => IsCurrentlyNavigated || IsExpanded ? "z-index: 850;" : string.Empty;

        internal string ProgressCssStyle => DemoTileProgressPercent switch
        {
            var d when d is < 0.25 => "0",
            var d when d is >= 0.25 and < 0.5 => "25",
            var d when d is >= 0.5 and < 0.75 => "50",
            var d when d is >= 0.75 and < 1 => "75",
            var d when d == 1 => "100",
            _ => "0",
        };

        internal double DemoTileProgressPercent => (CompletedPointCount / (double)PointCount);

        internal string DemoTileProgress => DemoTileProgressPercent.ToString("P0", cultureInfo);

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
            if (!ShowContent)
            {
                IsExpanded = true;
                IsCollapsed = false;
                await TileOpened.InvokeAsync();
                await Task.Delay(500);
                NavigationManager.NavigateTo(Href);
            }
        }

        internal async void CollapseTitle()
        {
            if (ShowContent)
            {
                IsExpanded = false;
                IsCollapsed = true;
                await TileClosed.InvokeAsync();
                await Task.Delay(500);
                NavigationManager.NavigateTo("/");
            }
        }


        private static readonly CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en");
    }
}
