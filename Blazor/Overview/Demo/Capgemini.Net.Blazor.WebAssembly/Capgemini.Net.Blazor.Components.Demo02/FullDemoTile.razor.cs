using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo02
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
                    Name = "Creation of container component",
                    Order = 1,
                    IsDone = false,
                    Label = "Edit a container component's @code block to handle parameter value changes",
                    Content = PointContext1!
                },
                new DemoChecklistPointContext
                {
                    Name = "Create basic inputs to edit component parameters",
                    Order = 2,
                    IsDone = false,
                    Label = "Write a basic HTML markup with in-line lambda expressions to control values for max rate and currently selected icon parameters",
                    Content = PointContext2!
                },
                new DemoChecklistPointContext
                {
                    Name = "Adding namespaces to __Imports file",
                    Order = 3,
                    IsDone = false,
                    Label = "Omit a usage of fully qualified names for Razor component tags",
                    Content = PointContext3!
                },
                new DemoChecklistPointContext
                {
                    Name = "Use custom components for increase and decrease buttons",
                    Order = 4,
                    IsDone = false,
                    Label = "Use a custom components in place of basic HTML <button> tag for decreasing/increasing parameters' values",
                    Content = PointContext4!
                },
                new DemoChecklistPointContext
                {
                    Name = "Add parameters to child component to fix style",
                    Order = 5,
                    IsDone = false,
                    Label = "Add parameter properties to MinusIcon, PlusIcon components to make their look and feel match the overall theme",
                    Content = PointContext5!
                },
                new DemoChecklistPointContext
                {
                    Name = "Add parameters to child component and bind them",
                    Order = 6,
                    IsDone = false,
                    Label = "Add parameter properties to Rate component, pass the selected values form parent component",
                    Content = PointContext6!
                },
            }
        };
    }
}
