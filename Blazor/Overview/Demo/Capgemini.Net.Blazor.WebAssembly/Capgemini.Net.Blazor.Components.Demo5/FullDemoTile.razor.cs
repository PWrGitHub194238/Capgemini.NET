using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo5
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
                    Name = "Add AvgRate",
                    Order = 1,
                    IsDone = false,
                    Label = "Add additional parameter for component representing average rate",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Add RateContext",
                    Order = 2,
                    IsDone = false,
                    Label = "Group rate parameters to form context model for the component",
                    Content = PointContext2!
                },
                new DemoChecklistPointContext
                {
                    Name = "Introduce IntSelector component",
                    Order = 3,
                    IsDone = false,
                    Label = "Replace raw HTML markup for context model properties modification with custom components",
                    Content = PointContext3!
                },
                new DemoChecklistPointContext
                {
                    Name = "Introduce EditForm component",
                    Order = 4,
                    IsDone = false,
                    Label = "Replace custom parameter value selectors with build-in form component",
                    Content = PointContext4!
                },
                new DemoChecklistPointContext
                {
                    Name = "Style build-in inputs",
                    Order = 5,
                    IsDone = false,
                    Label = "Replace build-in input components with custom wrap components to add styles for inputs and custom <option> tag markup",
                    Content = PointContext5!
                }
            }
        };
    }
}
