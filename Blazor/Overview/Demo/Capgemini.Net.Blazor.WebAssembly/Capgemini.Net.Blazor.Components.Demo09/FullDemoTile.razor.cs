using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo09
{
    [Route(DemoTile.Href + "/{id:int?}")]
    public partial class FullDemoTile
    {
        [Parameter]
        public int Id { get; set; }

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
                    Order = 3,
                    IsDone = false,
                    Label = "Use NavigationManager to fetch different products dynamically",
                    Content = PointContext4!
                },
                new DemoChecklistPointContext
                {
                    Name = "RenderFragment default values",
                    Order = 3,
                    IsDone = false,
                    Label = "Provide default content for unused render fragments",
                    Content = PointContext5!
                }
            }
        };
    }
}
