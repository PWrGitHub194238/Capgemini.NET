![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/04/summary.jpg)

# Improving components' re-usability: cascading values and render fragment

The below **README.md** file contains a transcript of the descriptions that can be found in the checklist for each of the exercise\`s points built into the Blazor WebAssembly application on-line ([link](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/)). You can directly go to that descriptions by navigating to any of those links:
 - [allow Container Razor component to receive any markup as a child](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo04/checklist/1),
 - [make custom child markup to receive loosely coupled parameters from the context of the container component](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo04/checklist/2).

### Allow Container Razor component to receive any markup as a child

Add a parameter of the `RenderFragment` type to the `ContainerComponent.razor.cs` file (code-behind file for the Razor component):

```
using Microsoft.AspNetCore.Components;
using System;

namespace Capgemini.Net.Blazor.Components.Demo04.Start
{
    public partial class ContainerComponent
    {
        // ...

        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;

        public string this[int i]
        {
            get => icons[i];
            set => icons[i] = value;
        }

        // ...
```

Change the given markup of `ContainerComponent.razor` Razor component (line `4`):

```
<!-- ... -->

<div class="demo__container">
	<RateComponent MaxRate="@MaxRate" Icon="@this[IconIndex]" />
</div>
```

- Remove the `<RateComponent MaxRate="@MaxRate" Icon="@this[IconIndex]" />` component's markup
- and replace it with a parameter that was added previously: `@ChildContent`:

```
<!-- ... -->

<div class="demo__container">
    @ChildContent
</div>
```

That change makes a `ContainerComponent` unrelated to any specific implementation of a component that inherits from the `RateComponentBase` base class, allowing `ContainerComponent` to receive any markup assigned as a value of the `ChildContent` variable.

Type of the parameter `ChildContent` - `RenderFragment` represents a chunk of Razor component's markup. By convention a property which is:

- marked with an attribute `[Parameter]`,
- named as `ChildContent`

represents any markup that is placed as a component's content ([child content](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#child-content)). This enables to customize a part of the Razor component without accessing and overriding its own code. Most of the [Razor components class libraries](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/class-libraries?view=aspnetcore-5.0&tabs=visual-studio) that enable some degree of customization utilize one or more [render fragments](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.renderfragment?view=aspnetcore-5.0) (or [generic render fragments](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.renderfragment-1?view=aspnetcore-5.0) - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-parameters)). Below example shows possible content of the `WrapperComponent.razor` Razor component to be used:

```
<ContainerComponent>
    <X />
</ContainerComponent>
```

where Razor component `X` can be replaced by:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/04/img/1.jpg)

`RenderFragment` allows the child content of the component to be a mixture of **C#** and Razor markup as well:

```
<ContainerComponent>
    @switch (new Random().Next(0, 2))
    {
        case 0:
            <RateComponent />
            break;
        case 1:
            <AlternativeRateComponent />
            break;
        case 2:
            <Alternative2RateComponent />
            break;
        default:
            break;
    }
</ContainerComponent>
```

Lines `2-15` will make the `WrapperComponent` to display different components based on the pseudo-random value without modifying the content of the `WrapperComponent`. Replace the content of the `WrapperComponent.razor` file with the markup below:

```
<ContainerComponent>
    <RateComponent />
</ContainerComponent>
```

**Note:** child content of a component that is assigned as a value of the `ChildContent` internally is treated as a [lambda statement](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#statement-lambdas) that will be capturing any variables that was used to generate the expression with all its drawbacks - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#child-content).

### Make custom child markup to receive loosely coupled parameters from the context of the container component

By replacing `<RateComponent MaxRate="@MaxRate" Icon="@this[IconIndex]" />` markup with the `@ChildContent` parameter makes the information about `RateComponent`'s parameter values to be lost. Changing the values on the form within the `ContainerComponent` no longer affects the `RateComponent` properties and the component won't rerender or change its parameter values.

Both `MaxRate` and `Icon` can be passed to the given component by utilizing parameters with `CascadingParameter` attribute and `CascadingValue` Razor component. In this model, the component which renders the `RenderFragment` parameter creates a context around the `RenderFragment`, providing any needed values in a form of parameters that can be observable. Any markup/component to be put within that context can subscribe to those parameters by decorating its own parameters with the `CascadingParameter` attribute instead of `[Parameter]` - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters?view=aspnetcore-5.0).

Modify the content of the `ContainerComponent` to define two nested `CascadingValue` Razor components (one for each primitive type parameter):

```
@using Capgemini.Net.Blazor.Components.SvgIcons
@using SvgIcons.Base

<div class="demo__container_wrapper">
    <span class="demo__label">Max rate</span>
    <div class="demo__selector">
        <MinusIcon Theme="IconTheme.DARK" OnClick="@(() => MaxRate -= 1)" />
        <strong>@MaxRate</strong>
        <PlusIcon Theme="IconTheme.DARK" OnClick="@(() => MaxRate += 1)" />
    </div>

    <span class="demo__label">Icon</span>
    <div class="demo__selector">
        <MinusIcon Theme="IconTheme.DARK" OnClick="@(() => IconIndex -= 1)" />
        <i class="far @this[iconIndex]"></i>
        <PlusIcon Theme="IconTheme.DARK" OnClick="@(() => IconIndex += 1)" />
    </div>

    <CascadingValue Name="MaxRate" Value="@MaxRate">
        <CascadingValue Value="@this[iconIndex]">
            <div class="demo__container">
                @ChildContent
            </div>
        </CascadingValue>
    </CascadingValue>
</div>
```

Lines `19-20`, `24-25` define cascading parameters available to be used in `@ChildContent`. Razor components that want to use any of those variables have to decorate their parameters with `CascadingParameter` attribute (the property name does not have to match).

`CascadingValue` component can define cascading parameters in two ways:

- by `Type` - any component that defines a parameter of a type `T` decorated with `[CascadingParameter]` will receive the reference that wrapping `CascadingValue` supplies (if its `Value` type is also `T`). If the component's parameter is of type `U` which `T` extends or implements, the parameter will also be supplies - type of the target property must allow the assignment ([read more](https://docs.microsoft.com/en-us/dotnet/api/system.type.isassignablefrom?view=net-5.0)). See below example where `Foo` extends `Bar` and `Foo` is provided by the `CascadingValue` to the `@ChildContent`. Component provided to this `RenderFragment` is subscribing for any `CascadingParameter` of type `Bar` and will accept the `Foo` object that was provided.

```
// ...
public class Foo : Bar
{
    public int Property { get; set; }
}
// ...
```

```
<CascadingValue TValue="Foo" Value="@obj">
    @ChildContent
</CascadingValue>

@code {
    private Foo obj = new Foo();
}
```

```
@CascadateObject.Property

@code {
    [CascadingParameter]
    public Bar CascadateObject { get; set; }
}
```

**Note:** if multiple `CascadingValue` components are defined each as a child of the previous one and then descendant component defines the cascading parameter of type `T`, the reference to the variable that is defined by the nearest ancestor will be used (if type of two or more values supplied by `CascadingValue` components can be assigned to the component's property).

- By `Name` - if specified, the parameter value will be supplied by the closest `CascadingValue` ancestor that supplies a value with this name. If no name is specified, then descendant components will receive the value based on the type of value they are requesting (or any type that extends or implements that type).
<br />**Note:** defining an optional `Name` property for a `CascadingParameter` attribute will make the decorated property to be matched only by the given name, not by its type. Same applies to the `CascadingValue` component - if it provides the value for its `Name` parameter then the value it supplies won't be matched against any descendant component parameters that don't provide the `Name` parameter to the `CascadingParameter` attribute.
<br />**Note:** if multiple `CascadingValue` components are defined each as a child of the previous one and then descendant component defines the cascading parameter with the matching name, the reference to the variable that is defined by the nearest parent will be used (if there are two or more `CascadingValue` components that have `Name` parameter defined with the requested name).

Change the attributes for both `MaxRate` and `Icon` parameters inside the `RateComponent` (lines `13` and `16`):

```
@inherits RateComponentBase

@for (int i = 0; i < @MaxRate; i += 1)
{
    int index = i;
    <i class="@(IsActive(i) ? ACTIVE_STYLE : INACTIVE_STYLE) @Icon cursor-pointer"
       @onclick="SetRate"
       @onmouseover="@(() => ShowRate(index))"
       @onmouseout=RevertRate></i>
}

@code {
    [CascadingParameter(Name = "MaxRate")]
    public int MaxRate { get; set; } = 5;

    [CascadingParameter]
    public string Icon { get; set; } = "fa-star";
}
```

Changing attribute from `[Parameter]` to `[CascadingParameter(Name = "MaxRate")]` will make the component to subscribe for changes of the value that was supplied by the nearest `CascadingValue` ancestor that defines its name to be equal. Changing attribute from `[Parameter]` to `[CascadingParameter]` for the `Icon` property will subscribe for value changes of the variable of type `string` supplied by the nearest `CascadingValue` ancestor.