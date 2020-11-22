using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo12
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
                    Name = "Custom cascading component",
                    Order = 1,
                    IsDone = false,
                    Label = "Create custom cascading component to provide the context for the rate icons",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Use RenderFragment for rate icons",
                    Order = 2,
                    IsDone = false,
                    Label = "Enable to use any type of icons for rate component",
                    Content = PointContext2!
                },
                new DemoChecklistPointContext
                {
                    Name = "Usage of StateHasChanged",
                    Order = 3,
                    IsDone = false,
                    Label = "Fix change state detection to render icons on any icon focus/blur",
                    Content = PointContext3!
                }
            }
        };
    }
}
