using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo03
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
                    Name = "Create code-behind instead of code block",
                    Order = 1,
                    IsDone = false,
                    Label = "Separate @code block from ContainerComponent component by creating code-behind file",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Move duplicated CSS styles",
                    Order = 2,
                    IsDone = false,
                    Label = "Move duplicated CSS styles from general purpose static file to CSS-behind file for the ContainerComponent Razor component",
                    Content = PointContext2!
                },
                new DemoChecklistPointContext
                {
                    Name = "Create a base class for RateComponent",
                    Order = 3,
                    IsDone = false,
                    Label = "Create a component base class, move the common logic out of the RateComponent",
                    Content = PointContext3!
                },
                new DemoChecklistPointContext
                {
                    Name = "Update tempRate on MaxRate parameter change",
                    Order = 4,
                    IsDone = false,
                    Label = "Validate and update inner state of the RateComponent on parameter value change",
                    Content = PointContext4!
                }
            }
        };
    }
}
