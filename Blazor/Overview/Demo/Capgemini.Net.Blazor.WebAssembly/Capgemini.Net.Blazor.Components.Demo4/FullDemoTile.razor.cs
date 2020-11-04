using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo4
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
                    Name = "Adding ChildContent parameter of type RenderFragment",
                    Order = 1,
                    IsDone = false,
                    Label = "Allow Container Razor component to receive any markup as a child",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Use CascadingValue component to provide context-oriented values",
                    Order = 2,
                    IsDone = false,
                    Label = "Make custom child markup to receive loosely coupled parameters from the context of the container component",
                    Content = PointContext2!
                }
            }
        };
    }
}
