using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo08
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
                    Name = "Build-in validation attributes",
                    Order = 1,
                    IsDone = false,
                    Label = "Use validation attributes for the form inputs instead of custom logic in setters",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Custom Input components",
                    Order = 2,
                    IsDone = false,
                    Label = "Prevent invalid values to be used, provide custom input controls",
                    Content = PointContext2!
                },
                new DemoChecklistPointContext
                {
                    Name = "Use of the EditContext for validation",
                    Order = 3,
                    IsDone = false,
                    Label = "Use edit form context to provide cross-field value correctness",
                    Content = PointContext3!
                },
                new DemoChecklistPointContext
                {
                    Name = "Overriding TryParseValueFromString",
                    Order = 4,
                    IsDone = false,
                    Label = "Change the behavior of the number inputs to forbids invalid values to be set",
                    Content = PointContext4!
                },
            }
        };
    }
}
