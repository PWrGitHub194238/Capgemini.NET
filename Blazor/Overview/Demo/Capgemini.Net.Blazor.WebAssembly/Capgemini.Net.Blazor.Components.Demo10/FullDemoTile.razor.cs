using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo10
{
    [Route(DemoTile.Href)]
    [Route(DemoTile.Href + "/checklist")]
    [Route(DemoTile.Href + "/checklist/{*pageRoute}")]
    [Route(DemoTile.Href + "/{id:int}")]
    [Route(DemoTile.Href + "/{id:int}/{*pageRoute}")]
    public partial class FullDemoTile : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public string PageRoute { get; set; } = default!;

        internal static DemoChecklistContext Context => new DemoChecklistContext
        {
            Name = typeof(FullDemoTile).FullName!,
            Points = new Collection<DemoChecklistPointContext>()
            {
                new DemoChecklistPointContext
                {
                    Name = "Log default RenderFragment usage",
                    Order = 1,
                    IsDone = false,
                    Label = "Log information about default render fragment usage",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Override SetRate",
                    Order = 2,
                    IsDone = false,
                    Label = "Set the current rate on rate click to update the context object",
                    Content = PointContext2!
                },
                new DemoChecklistPointContext
                {
                    Name = "Custom event callback",
                    Order = 3,
                    IsDone = false,
                    Label = "Notify API about the change, fetch new set of data and refresh component",
                    Content = PointContext3!
                },
                new DemoChecklistPointContext
                {
                    Name = "Custom event callback",
                    Order = 4,
                    IsDone = false,
                    Label = "Provide initial rate value for the rate component, redirect to random product on initialization",
                    Content = PointContext4!
                },
                new DemoChecklistPointContext
                {
                    Name = "Custom event callback",
                    Order = 5,
                    IsDone = false,
                    Label = "Provide a proper rate of the product to be displayed on product change",
                    Content = PointContext5!
                }
            }
        };
    }
}
