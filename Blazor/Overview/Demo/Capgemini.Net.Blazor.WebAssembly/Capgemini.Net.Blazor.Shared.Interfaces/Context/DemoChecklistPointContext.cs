using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Shared.Interfaces.Context
{
    public class DemoChecklistPointContext
    {
        public string Name { get; init; } = default!;

        public int Order { get; init; }

        public bool IsDone { get; set; }

        public string Label { get; init; } = default!;

        public RenderFragment Content { get; set; } = default!;
    }
}
