using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo07
{
    [Route(DemoTile.Href)]
    [Route(DemoTile.Href + "/{*pageRoute}")]
    public partial class FullDemoTile : ComponentBase
    {
        [Parameter]
        public string PageRoute { get; set; } = default!;

        internal static DemoChecklistContext Context => new DemoChecklistContext
        {
            Name = typeof(FullDemoTile).FullName!,
            Points = new Collection<DemoChecklistPointContext>()
            {
                new DemoChecklistPointContext
                {
                    Name = "Move avg rate outside",
                    Order = 1,
                    IsDone = false,
                    Label = "Separate the average counter from the rating component",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Add RenderFragment for AvgRate",
                    Order = 2,
                    IsDone = false,
                    Label = "Replace default average rate markup with replaceable render fragment",
                    Content = PointContext2!
                }
            }
        };
    }
}
