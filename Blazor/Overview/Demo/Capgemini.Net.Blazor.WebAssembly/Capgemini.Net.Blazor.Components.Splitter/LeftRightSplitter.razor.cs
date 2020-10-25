using Capgemini.Net.Blazor.Components.Splitter.Base;
using Capgemini.Net.Blazor.Components.Splitter.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;

namespace Capgemini.Net.Blazor.Components.Splitter
{
    public partial class LeftRightSplitter
    {
        [Inject]
        public IJSInteropService JSInteropService { get; set; } = default!;

        [Parameter]
        public double LeftPercentWidthDefault { get; set; } = 50;

        [Parameter]
        public double LeftPercentMinWidthDefault { get; set; } = 5;

        [Parameter]
        public RenderFragment Left { get; set; } = default!;

        [Parameter]
        public double RightPercentWidthDefault { get; set; } = 50;

        [Parameter]
        public double RightPercentMinWidthDefault { get; set; } = 5;

        [Parameter]
        public RenderFragment Right { get; set; } = default!;

        [Parameter]
        public string ColorStyle { get; set; } = "#ececec";

        [Parameter]
        public string ColorOnHoverStyle { get; set; } = "#95e616";

        [Parameter]
        public string WidthStyle { get; set; } = "3px";

        [Parameter]
        public string WidthOnHoverStyle { get; set; } = "5px";

        [Parameter]
        public string SplitterPadding { get; set; } = "36px";

        [Parameter]
        public EventCallback<(double, double)> SplitterPositionChanged { get; set; }

        protected override void OnParametersSet()
        {
            leftPercentWidth = LeftPercentWidthDefault / 100;
            rightPercentWidth = RightPercentWidthDefault / 100;
            leftPercentMinWidth = LeftPercentMinWidthDefault / 100;
            rightPercentMinWidth = RightPercentMinWidthDefault / 100;
            base.OnParametersSet();
        }

        internal string MouseStateClassName => mouseState switch
        {
            MouseState.MOVE => nameof(MouseState.MOVE).ToLowerInvariant(),
            MouseState.OVER => nameof(MouseState.OVER).ToLowerInvariant(),
            MouseState.UP => nameof(MouseState.UP).ToLowerInvariant(),
            MouseState.DOWN => nameof(MouseState.DOWN).ToLowerInvariant(),
            MouseState.OUT => nameof(MouseState.OUT).ToLowerInvariant(),
            _ => string.Empty,
        };

        internal string LeftPercentWidth => leftPercentWidth.ToString("P4", cultureInfo);

        internal string LeftPercentMinWidth => leftPercentMinWidth.ToString("P4", cultureInfo);

        internal string SplitterColorStyle => mouseState switch
        {
            MouseState.OVER => ColorOnHoverStyle,
            _ => ColorStyle,
        };

        internal string SplitterWidthStyle => mouseState switch
        {
            MouseState.OVER => WidthOnHoverStyle,
            _ => WidthStyle,
        };

        internal string RightPercentMinWidth => rightPercentMinWidth.ToString("P4", cultureInfo);

        internal string RightPercentWidth => rightPercentWidth.ToString("P4", cultureInfo);

        internal ElementReference? ContainerRef { get; set; }

        internal MouseState mouseState;

        internal void OnMouseMove(MouseEventArgs args)
        {
            if (splitterClicked)
            {
                if (containerBox is not null)
                {
                    leftPercentWidth = (args.ClientX - containerBox.X) / containerBox.Width;
                    rightPercentWidth = 1 - leftPercentWidth;
                }
            }
        }

        internal void OnMouseOver(MouseEventArgs args)
        {
            if (!splitterClicked)
            {
                mouseState = MouseState.OVER;
            }
        }

        internal async void OnMouseDown(MouseEventArgs args)
        {
            if (mouseState == MouseState.OVER && ContainerRef is not null)
            {
                containerBox = await JSInteropService.GetBoundingClientRect(ContainerRef.Value);
                splitterClicked = true;
            }
        }
        internal void OnMouseUp(MouseEventArgs args)
        {
            if (splitterClicked)
            {
                mouseState = MouseState.UP;
                SplitterPositionChanged.InvokeAsync((leftPercentWidth, rightPercentWidth));
                splitterClicked = false;
            }
        }

        internal void OnMouseOut(MouseEventArgs args)
        {
            if (!splitterClicked)
            {
                mouseState = MouseState.OUT;
            }
        }

        internal void OnHideLeftClicked()
        {
            leftPercentWidth = leftPercentMinWidth;
            rightPercentWidth = 1 - leftPercentWidth;
        }

        internal void OnHideRightClicked()
        {
            rightPercentWidth = rightPercentMinWidth;
            leftPercentWidth = 1 - rightPercentWidth;
        }

        private static readonly CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en");

        private DOMRect? containerBox;

        private double leftPercentWidth;

        private double rightPercentWidth;

        private double leftPercentMinWidth;

        private double rightPercentMinWidth;

        private bool splitterClicked;
    }
}
