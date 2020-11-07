using Capgemini.Net.Blazor.Components.SvgIcons.Base;
using Microsoft.AspNetCore.Components;
using System;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public partial class IntSelector
    {
        [Parameter]
        public string Label { get; set; } = default!;

        [Parameter]
        public int Value { get; set; }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        [Parameter]
        public RenderFragment<string> ChildContent { get; set; } = default!;

        [Parameter]
        public Func<int, string> StringValue { get; set; } = value => value.ToString();

        [Parameter]
        public IconTheme Theme { get; set; } = IconTheme.DARK;
    }
}
