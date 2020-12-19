using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo09
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
                    Name = "Review changes",
                    Order = 1,
                    IsDone = false,
                    Label = "Review changes in initial component",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Add HttpClient to fetch the data",
                    Order = 2,
                    IsDone = false,
                    Label = "Call the API to fetch rate data for a product, handle HttpClient's exceptions",
                    Content = PointContext2!
                },
                new DemoChecklistPointContext
                {
                    Name = "Use LoadPlaceholder",
                    Order = 3,
                    IsDone = false,
                    Label = "Handle incomplete asynchronous actions at render",
                    Content = PointContext3!
                },
                new DemoChecklistPointContext
                {
                    Name = "Use NavigationManager",
                    Order = 4,
                    IsDone = false,
                    Label = "Use NavigationManager to fetch different products dynamically",
                    Content = PointContext4!
                },
                new DemoChecklistPointContext
                {
                    Name = "RenderFragment default values",
                    Order = 5,
                    IsDone = false,
                    Label = "Provide default content for unused render fragments",
                    Content = PointContext5!
                }
            }
        };
    }
}
