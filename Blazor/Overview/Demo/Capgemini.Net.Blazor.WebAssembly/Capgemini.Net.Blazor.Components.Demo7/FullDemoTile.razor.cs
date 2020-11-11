using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo7
{
    [Route(DemoTile.Href)]
    public partial class FullDemoTile
    {
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
