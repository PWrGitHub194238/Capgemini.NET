using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo11
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
                    Name = "Add RateIcon component",
                    Order = 1,
                    IsDone = false,
                    Label = "Replace HTML markup for the icons with a component, use event callbacks to use base class logic",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "RateContext",
                    Order = 2,
                    IsDone = false,
                    Label = "Replace the base class logic with context class",
                    Content = PointContext2!
                },
                new DemoChecklistPointContext
                {
                    Name = "Add custom data binding",
                    Order = 3,
                    IsDone = false,
                    Label = "Simplify the logic for RateIconComponent handled by its parameters and event callbacks",
                    Content = PointContext3!
                },
                new DemoChecklistPointContext
                {
                    Name = "Extract on focus events",
                    Order = 4,
                    IsDone = false,
                    Label = "Reduce the complexity of the callbacks, wrap all rate icons with a tag to handle on focus events",
                    Content = PointContext4!
                }
            }
        };
    }
}
