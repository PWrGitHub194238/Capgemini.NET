using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo10
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
                    Order = 3,
                    IsDone = false,
                    Label = "Provide initial rate value for the rate component, redirect to random product on initialization",
                    Content = PointContext4!
                }
            }
        };
    }
}
