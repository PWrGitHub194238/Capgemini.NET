using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo1
{
    [Route(DemoTile.Href)]
    public partial class FullDemoTile
    {
        internal DemoChecklistContext Context => new DemoChecklistContext
        {
            Name = typeof(FullDemoTile).FullName!,
            Points = new Collection<DemoChecklistPointContext>()
            {
                new DemoChecklistPointContext
                {
                    Name = "Install Ty",
                    Order = 1,
                    IsDone = false,
                    Label = "Label 1",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "2",
                    Order = 2,
                    IsDone = false,
                    Label = "Label 2",
                    Content = PointContext2!
                }
            }
        };
    }
}
