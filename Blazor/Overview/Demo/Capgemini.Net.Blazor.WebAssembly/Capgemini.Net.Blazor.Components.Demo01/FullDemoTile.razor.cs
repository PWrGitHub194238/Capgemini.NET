using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo01
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
                    Name = "Copy HTML markup",
                    Order = 1,
                    IsDone = false,
                    Label = "Copy HTML markup with already provided JavaScript calls",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Migrate TypeScript code",
                    Order = 2,
                    IsDone = false,
                    Label = "Migrate TypeScript code directly into Razor Component",
                    Content = PointContext2!
                },
                new DemoChecklistPointContext
                {
                    Name = "Change DOM event handlers",
                    Order = 3,
                    IsDone = false,
                    Label = "Change DOM event handlers to Blazor event handlers",
                    Content = PointContext3!
                },
                new DemoChecklistPointContext
                {
                    Name = "Updating class attribute value with one-way binding",
                    Order = 4,
                    IsDone = false,
                    Label = "Use one-way binding to dynamically update class attribute value",
                    Content = PointContext4!
                },
                new DemoChecklistPointContext
                {
                    Name = "Fixing lambda value capturing",
                    Order = 5,
                    IsDone = false,
                    Label = "Deal with lambda variable capturing, causing each rate icon to represent max rate",
                    Content = PointContext5!
                }
            }
        };
    }
}
