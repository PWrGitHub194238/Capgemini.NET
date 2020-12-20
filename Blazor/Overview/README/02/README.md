![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/02/summary.jpg)

# Rate range & icon customization: component parameters

The below **README.md** file contains a transcript of the descriptions that can be found in the checklist for each of the exercise\`s points built into the Blazor WebAssembly application on-line ([link](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/)). You can directly go to that descriptions by navigating to any of those links:
 - [edit a container component's @code block to handle parameter value changes](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo02/checklist/1),
 - [write a basic HTML markup with in-line lambda expressions to control values for max rate and currently selected icon parameters](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo02/checklist/2),
 - [omit a usage of fully qualified names for Razor component tags](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo02/checklist/3),
 - [use a custom components in place of basic HTML <button> tag for decreasing/increasing parameters' values](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo02/checklist/4),
 - [add parameter properties to MinusIcon and PlusIcon components to make their look and feel match the overall theme](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo02/checklist/5),
 - [add parameter properties to Rate component, pass the selected values form parent component](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo02/checklist/6).

### Edit a container component's @code block to handle parameter value changes

Edit a file `./Start/ContainerComponent.razor` by adding `maxRate` and `iconIndex` fields to the `@code { }` section. Those variables will be used to store the selection of both max rate and icon class for the `RateComponent` that was created in **demo01**.

```
@code {
    private int maxRate = 6;

    private int iconIndex = 0;
 }
```

`RateComponent` utilizes the free set of [Font Awesome](https://fontawesome.com)'s classes to display different icons like [fa-star](https://fontawesome.com/icons/star?style=regular). For instance we can use the given set of icons associated with given classes:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/02/img/1.jpg)

Add an array with all given icon classes to the  `@code { }`, after the `maxRate` and `iconIndex` fields:

```
@code {
    private int maxRate = 6;

    private int iconIndex = 0;

    private string[] icons = new[] { "fa-star", "fa-grin-stars", "fa-angry", "fa-sun" };
}
```

### Write a basic HTML markup with in-line lambda expressions to control values for max rate and currently selected icon parameters

Add the following code snipped to the top of the `./Start/ContainerComponent.razor` file, before the `@code { }` section added previously:

```
<div class="demo02__container_wrapper">
    <span class="demo02__label">Max rate</span>
    <div class="demo02__selector">
        <button @onclick="@(() => maxRate = Math.Max(2, maxRate - 1))">-</button>
            <strong>@maxRate</strong>
        <button @onclick="@(() => maxRate += 1)">+</button>
    </div>

    <span class="demo02__label">Icon</span>
    <div class="demo02__selector">
        <button @onclick="@(() => iconIndex = iconIndex == 0 ? icons.Length - 1 : iconIndex - 1)">-</button>
            <i class="far @icons[iconIndex]"></i>
        <button @onclick="@(() => iconIndex = (iconIndex + 1) % icons.Length)">+</button>
    </div>

    <div class="demo02__container">
        <Capgemini.Net.Blazor.Components.Demo02.Start.RateComponent />
    </div>
</div>
```

Besides the standard **HTML** markup with a custom styles that are already defined to make the markup to render properly, several lambda expressions were written for the `@onclick` handlers of the buttons that will control both `maxRate` and `iconIndex` field values (`+` will increase a given value, `-` will decrease it):

- `@(() => maxRate = Math.Max(2, maxRate - 1))` (line `4`) delegate allows the value of `maxRate` to be decreased each time `-` button is clicked (but not less that `2`),
- `@(() => maxRate += 1)` (line `6`) delegate allows the value of `maxRate` to be increased endlessly,
- `@(() => iconIndex = iconIndex == 0 ? icons.Length - 1 : iconIndex - 1)` (line `11`) delegate decreases the index to be used in conjunction with `icons` array not allowing the value of `iconIndex` to be out of bound of that array,
- `@(() => iconIndex = (iconIndex + 1) % icons.Length)` (line `13`) delegate increases the index each time the `+` button is clicked.

In addition to those delegates, the other Razor component specific syntax is a usage of `RateComponent` as a [nested component](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#use-components) of `ContainerComponent` with a fully qualified name (`Capgemini.Net.Blazor.Components.Demo02.Start.RateComponent`) which can be simplified.

### Omit a usage of fully qualified names for Razor component tags

By default, to add the nested component to other component, the full qualify name has to be given. This behavior is exactly the same as for other **.NET** types. In fact all Razor components are compiled and represented as partial classes ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#partial-class-support)). By adding a `@using` [directive](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#using) with the namespace of the component's class, it is possible to use only component's name as **HTML** tag ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#namespaces)):

```
@using Capgemini.Net.Blazor.Components.Demo02.Start

// ...

<div class="demo02__container">
    <RateComponent />
</div>
```

It is also possible to move all namespaces to a separate file to not have to define them for each ***.razor** file. Blazor defines the `_Imports.razor` file by [convention](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#namespace) which content (i.e. using statements) will be added by the framework to the top of each ***.razor** file within the directory of the `_Imports.razor`. Each directory can have its own `_Imports.razor` file. Add the `@using` statement to the `./_Imports.razor` file:

```
@namespace Capgemini.Net.Blazor.Components.Demo02

@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Rendering
@using Microsoft.AspNetCore.Components.Web.Extensions.Head

@using Capgemini.Net.Blazor.Components.Tile
@using Capgemini.Net.Blazor.Components.Tile.Base

@using Capgemini.Net.Blazor.Components.Demo02.Start
```

**Note:** adding `@using Capgemini.Net.Blazor.Components.Demo02.Start` to `_Imports.razor` in scenario where there are multiple classes with the same name that are differing only by a namespace (`./Start/RateComponent.razor` and `./End/RateComponent.razor`) will make impossible to call `Capgemini.Net.Blazor.Components.Demo02.Start.RateComponent` with component's name only.

**Note:** if no namespace for the nested component is provided, the same namespace of its container component will be used.

### Use a custom components in place of basic HTML <button> tag for decreasing/increasing parameters' values

Add `Capgemini.Net.Blazor.Components.SvgIcons` dependency ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/class-libraries?view=aspnetcore-5.0&tabs=visual-studio#consume-a-library-component)) to the `Capgemini.Net.Blazor.Components.Demo02` project in order to be able to use two custom components:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/02/img/2.jpg)

Add the default namespace for added project to `RateComponent` (line `1`) and replace the default  buttons with the new components (lines `6`, `8`, `13`, `15`):

```
@using Capgemini.Net.Blazor.Components.SvgIcons

<div class="demo02__container_wrapper">
    <span class="demo02__label">Max rate</span>
    <div class="demo02__selector">
        <MinusIcon OnClick="@(() => maxRate = Math.Max(2, maxRate - 1))" />
            <strong>@maxRate</strong>
        <PlusIcon OnClick="@(() => maxRate += 1)" />
    </div>

    <span class="demo02__label">Icon</span>
    <div class="demo02__selector">
        <MinusIcon OnClick="@(() => iconIndex = iconIndex == 0 ? icons.Length - 1 : iconIndex - 1)" />
            <i class="far @icons[iconIndex]"></i>
        <PlusIcon OnClick="@(() => iconIndex = (iconIndex + 1) % icons.Length)" />
    </div>

    <div class="demo02__container">
        <RateComponent />
    </div>
</div>
```

Same as a default `<button>` **HTML** tag can be used with `@onclick` Razor component specific markup for event handler, the custom components `MinusIcon` and `PlusIcon` allow to specify the delegate as a value for `OnClick` event handler which is internally specified by both of these components.

### Add parameter properties to MinusIcon and PlusIcon components to make their look and feel match the overall theme

Both `MinusIcon` and `PlusIcon` also supports additional parameters like:


- `Height`/`Width` which will define the size for the components:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/02/img/3.jpg)

- `Theme` which will define the coloring for the components:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/02/img/4.jpg)

By using the `Theme` parameter, set its value to `IconTheme.DARK` for all `MinusIcon` and `PlusIcon` to make their look and feel match the overall solution. `IconTheme` lives inside the `SvgIcons.Base` namespace:

```
@using Capgemini.Net.Blazor.Components.SvgIcons
@using SvgIcons.Base

<div class="demo02__container_wrapper">
    <span class="demo02__label">Max rate</span>
    <div class="demo02__selector">
        <MinusIcon
            Theme="IconTheme.DARK"
            OnClick="@(() => maxRate = Math.Max(2, maxRate - 1))" />
        <strong>@maxRate</strong>
        <PlusIcon
            Theme="IconTheme.DARK"
            OnClick="@(() => maxRate += 1)" />
    </div>

    <span class="demo02__label">Icon</span>
    <div class="demo02__selector">
        <MinusIcon
            Theme="IconTheme.DARK"
            OnClick="@(() => iconIndex = iconIndex == 0 ? icons.Length - 1 : iconIndex - 1)" />
        <i class="far @icons[iconIndex]"></i>
        <PlusIcon
            Theme="IconTheme.DARK"
            OnClick="@(() => iconIndex = (iconIndex + 1) % icons.Length)" />
    </div>

    <div class="demo02__container">
        <RateComponent />
    </div>
</div>

@code {
    private int maxRate = 6;

    private int iconIndex = 0;

    private string[] icons = new[] { "fa-star", "fa-grin-stars", "fa-angry", "fa-sun" };
}
```

### Add parameter properties to Rate component, pass the selected values form parent component

To make use of the `maxRate` and `iconIndex` and make `RateComponent` to react on any of those fields' value change, `RateComponent` has to define [parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#component-parameters) for both entries. Open the `./Start/RateComponent.razor` file and add `MaxRate` and `Icon` properties to the `@code { }` block:

```
@code {
    [Parameter]
    public int MaxRate { get; set; } = 5;

    [Parameter]
    public string Icon { get; set; } = "fa-star";

    // ...
}
```

The `Parameter` attribute allows a property to be treated as a **DOM** attribute for the component tag which defines those properties. In order to pass the values from the `ContainerComponent`, change the `<RateComponent />` as follows:

```
<RateComponent `MaxRate="@maxRate" Icon="@icons[iconIndex]"` />
```

Lastly, to make changes take effect, the actual **HTML** markup of the `RateComponent` has do be changed as follows:

```
@for (int i = 0; i < `@MaxRate`; i += 1)
{
    int index = i;
    <i class="@(IsActive(i) ? ACTIVE_STYLE : INACTIVE_STYLE) `@Icon` cursor-pointer"
        @onclick="SetRate"
        @onmouseover="@(() => ShowRate(index))"
        @onmouseout=RevertRate></i>
}
```

Changing the loop range from `{ 0, ..., 4 }` to `{ 0, ..., @MaxRate - 1 }`, providing the `<i>` tag `class` with the `@Icon` variable in place of the fixed `fa-star` (using one-way binding), the component's content would be re-rendered each time any of the properties, decorated with the `Parameter` attribute, changes.