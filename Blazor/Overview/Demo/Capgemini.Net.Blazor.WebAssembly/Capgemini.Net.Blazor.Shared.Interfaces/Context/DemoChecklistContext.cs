using System.Collections.Generic;

namespace Capgemini.Net.Blazor.Shared.Interfaces.Context
{
    public class DemoChecklistContext
    {
        public string Name { get; init; } = default!;

        public ICollection<DemoChecklistPointContext> Points { get; set; } = default!;
    }
}
