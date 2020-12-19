![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/03/Summary.jpg)

# Basic example refactoring: code-behind and CSS isolation

The below **README.md** file contains a transcript of the descriptions that can be found in the checklist for each of the exercise\`s points built into the Blazor WebAssembly application on-line ([link](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/)). You can directly go to that descriptions by navigating to any of those links:
 - [separate @code block from ContainerComponent component by creating code-behind file](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo03/checklist/1),
 - [move duplicated CSS styles from general purpose static file to CSS-behind file for the ContainerComponent Razor component](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo03/checklist/2),
 - [create a component base class, move the common logic out of the RateComponent](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo03/checklist/3),
 - [validate and update inner state of the RateComponent on parameter value change](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo03/checklist/4).

### Separate @code block from ContainerComponent component by creating code-behind file

While any **C#** logic can be placed inside a `@code { }` block of a Razor component to be mixed up with **HTML** markup of the component, **C#** code can be also defined in separate file as long as:

- it is marked to be a `partial class`,
- is named exactly the same as the `*.razor` file,
- if the Razor component inherits and/or implements any base class/interface, the code-behind class also has to inherits/implements them,
- it is defined within the same namespace as a Razor component is (be default `*.razor` file lives in the namespace defined by its assembly name and component's directory tree unless the `@namespace` directive is defined inside the `_Imports.razor` file ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#namespaces)).

The file itself is not required to be named after the Razor component but it might be helpful as Visual Studio IDE automatically nests files that share same prefix like shown below:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/03/img/1.jpg)

Define `ContainerComponent.razor.cs` file and move the content of the `./Start/ContainerComponent.razor` file to its code-behind file:

```
@using Capgemini.Net.Blazor.Components.SvgIcons
@using SvgIcons.Base

<!-- ... ..>

@code {

}
```

```
namespace Capgemini.Net.Blazor.Components.Demo03.Start
{
    public partial class ContainerComponent
    {
        private int maxRate = 6;

        private string[] icons = new[] { "fa-star", "fa-grin-stars", "fa-angry", "fa-sun" };

        private int iconIndex = 0;
    }
}
```

The `@code { }` can be deleted as it is empty. If there is some code left in both Razor component file and its code-behind file, both code chunks will be merged (as normally partial class would - [read more](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#partial-types)).

As the logic was moved to the code-behind `ContainerComponent.razor.cs` file, Visual Studio IDE can give code recommendations (or other refactoring):

- make `icons` private array `readonly` - that array is not a subject to change,
- convert private `maxRate` and `iconIndex` to properties, renaming them in the process.
- **__(optionally)__** move a logic from `OnClick` event handlers (i.e. `@(() => MaxRate = Math.Max(2, MaxRate - 1))` can be simplified to `@(() => MaxRate -= 1)` with the logic to be handle by the **setter** of the `MaxRate` property).
- **__(optionally)__** the [indexer](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers) can be introduced to gracefully access the `private readonly` `icons` array.

Final `ContainerComponent.razor.cs` Razor component's code-behind partial class might look like this:

```
using System;

namespace Capgemini.Net.Blazor.Components.Demo03.Start
{
    public partial class ContainerComponent
    {
        private int maxRate = 6;
        private int iconIndex;

        private readonly string[] icons = {
            "fa-star",
            "fa-grin-stars",
            "fa-angry",
            "fa-sun"
        };

        public string this[int i]
        {
            get => icons[i];
            set => icons[i] = value;
        }

        public int MaxRate
        {
            get => maxRate;
            set => maxRate = Math.Max(2, value);
        }

        public int IconIndex
        {
            get => iconIndex;
            set => iconIndex = value < 0
                ? icons.Length - 1
                : value >= icons.Length
                    ? 0
                    : value;
        }
    }
}
```

... and after moving the logic away from the Razor component's markup file `ContainerComponent.razor` to the code-behind **setters**, event handlers for `MinusIcon` and `PlusIcon` Razor nested components can be simplified:

```
@using Capgemini.Net.Blazor.Components.SvgIcons
@using SvgIcons.Base

<div class="demo03__container_wrapper">
    <span class="demo03__label">Max rate</span>
    <div class="demo03__selector">
        <MinusIcon Theme="IconTheme.DARK" `OnClick="@(() => MaxRate -= 1)"` />
        <strong>@MaxRate</strong>
        <PlusIcon Theme="IconTheme.DARK" `OnClick="@(() => MaxRate += 1)"` />
    </div>

    <span class="demo03__label">Icon</span>
    <div class="demo03__selector">
        <MinusIcon Theme="IconTheme.DARK"` OnClick="@(() => IconIndex -= 1)"` />
        <i class="far @this[iconIndex]"></i>
        <PlusIcon Theme="IconTheme.DARK" `OnClick="@(() => IconIndex += 1)"` />
    </div>

    <div class="demo03__container">
        <RateComponent MaxRate="@MaxRate" Icon="@this[IconIndex]" />
    </div>
</div>
```

**Note:** as both partial classes: `ContainerComponent.razor` (auto-generated) and `ContainerComponent.razor.cs` will be merged as one, Razor component can access all fields of the merged class, including `private` fields. In that case [indexer](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers) is not needed.

**Note:** although the **setter** for `IconIndex` property of the `ContainerComponent.razor.cs` file looks complicated, there is no possibility to simplify it by using [pattern matching](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#pattern-matching-enhancements) as `icons.length` is not a constant.

### Move duplicated CSS styles from general purpose static file to CSS-behind file for the ContainerComponent Razor component

Including static resources like **CSS** in Blazor can be done in several ways:

- by adding static file to a `wwwroot` directory that is containing `index.html` file (for `WebAssembly` - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/templates?view=aspnetcore-5.0#blazor-project-structure)), for example linking the static `wwwroot/css/main.css` **CSS** file in `index.html`:

```
<!DOCTYPE html>
<html>

    <head>
        <!-- ... -->
        <link href="css/main.css" rel="stylesheet" />
        <!-- ... -->
    </head>

    <!-- ... -->
```

- by adding static file to a `wwwroot` directory of any [Razor components class library](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/class-libraries?view=aspnetcore-5.0&tabs=visual-studio) - during the compilation all static files that were added to that `wwwroot` directory will be available at the `_content/{ASSEMBLY NAME}/{RELATIVE FILE PATH}` path (i.e. for `Capgemini.Net.Blazor.Components.Demo03` assembly and `wwwroot\css\styles.css` file the path would be `_content/Capgemini.Net.Blazor.Components.Demo03/css/styles.css`):

```
<!DOCTYPE html>
<html>

    <head>
        <!-- ... -->
        <link href="_content/Capgemini.Net.Blazor.Components.Demo03/css/styles.css" rel="stylesheet" />
        <!-- ... -->
    </head>

    <!-- ... -->
```

- by adding static file to a `wwwroot` directory of any [Razor components class library](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/class-libraries?view=aspnetcore-5.0&tabs=visual-studio) and utilizing the `<Link />` component from `Microsoft.AspNetCore.Components.Web.Extensions` namespace (see [NuGet package](https://www.nuget.org/packages/Microsoft.AspNetCore.Components.Web.Extensions)) directly into Razor component's markup:

```
<!-- ... -->
<Link href="_content/Capgemini.Net.Blazor.Components.Demo03/css/styles.css" rel="stylesheet" />
<!-- ... -->
```

- by utilizing [Blazor CSS isolation](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-5.0).

[Blazor CSS isolation](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-5.0) enables **CSS** files to be nested in the same manner as code-behind ***.cs** files for Razor components.

Create the `ContainerComponent.razor.scss` file in the same directory as `ContainerComponent.razor` Razor component is (along with the `ContainerComponent.razor.cs` partial code-behind class).

**Note:** ***.scss** has to be compiled to ***.css** file as Blazor does not support **CSS** preprocessors - to enable ***.scss** compilation [Web Essentials](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebEssentials2019) extension for Visual Studio can be installed.

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/03/img/2.jpg)

During the compilation process Blazor will bundle all nested **cascade sheets** into `{ASSEMBLY NAME}.styles.css` file. For it to be used, the given file has to be linked in the `index.html` if it wasn't provided by the default project's template:

```
<!DOCTYPE html>
<html>

    <head>
        <!-- ... -->
        <link href="Capgemini.Net.Blazor.WebAssembly.Client.styles.css" rel="stylesheet" />
        <!-- ... -->
    </head>

    <!-- ... -->
```

Add the given content to the nested `ContainerComponent.razor.scss` file:

```
.demo__container_wrapper {
    display: flex;
    flex-direction: column;
    justify-content: space-evenly;
    align-items: center;
    width: 200px;

    .demo__label {
        font-family: 'Ubuntu';
        font-weight: bold;
        text-transform: uppercase;
        font-size: larger;
    }

    .demo__selector {
        display: flex;
        justify-content: space-between;
        align-items: center;
        width: 100%;
        margin-bottom: 25px;
    }

    .demo__container {
        display: flex;
    }
}
```

and replace all class names in `ContainerComponent.razor` Razor component:

- `demo03__container_wrapper` -> `demo__container_wrapper`,
- `demo03__label` -> `demo__label`,
- `demo03__selector` -> `demo__selector`,
- `demo03__container` -> `demo__container`,

```
@using Capgemini.Net.Blazor.Components.SvgIcons
@using SvgIcons.Base

<div `class="demo__container_wrapper"`>
    <span `class="demo__label"`>Max rate</span>
    <div `class="demo__selector"`>
        <MinusIcon Theme="IconTheme.DARK" OnClick="@(() => MaxRate -= 1)" />
        <strong>@MaxRate</strong>
        <PlusIcon Theme="IconTheme.DARK" OnClick="@(() => MaxRate += 1)" />
    </div>

    <span `class="demo__label"`>Icon</span>
    <div `class="demo__selector"`>
        <MinusIcon Theme="IconTheme.DARK" OnClick="@(() => IconIndex -= 1)" />
        <i class="far @this[iconIndex]"></i>
        <PlusIcon Theme="IconTheme.DARK" OnClick="@(() => IconIndex += 1)" />
    </div>

    <div `class="demo__container"`>
        <RateComponent MaxRate="@MaxRate" Icon="@this[IconIndex]" />
    </div>
</div>
```

**Note:** The `ContainerComponent.razor.scss` has to be compiled manually for the first time:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/03/img/3.jpg)

### Create a component base class, move the common logic out of the RateComponent

To separate the `RateComponent` Razor component's logic further, create the `RateComponentBase` class in `Capgemini.Net.Blazor.Components.Demo` Razor component library and move all the code from `ContainerComponent.razor.cs` file that modifies inner state of the component to `RateComponentBase`:

```
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public class RateComponentBase : ComponentBase
    {
        public static `readonly` string ACTIVE_STYLE = "fas";

        public static `readonly` string INACTIVE_STYLE = "far";

        private int rate = 0;

        private int tempRate = 0;

        protected int Rate
        {
            get => rate + 1;
            set
            {
                rate = value - 1;
                tempRate = rate;
            }
        }

        protected void SetRate() => rate = tempRate;

        protected void ShowRate(int index) => tempRate = index;

        protected void RevertRate() => tempRate = rate;

        protected bool IsActive(int index) => index <= tempRate;
    }
}
```

Several improvements were made to the copied code alongside (see highlighted lines):

- `7-9`: both static fields were marked as `readonly` as they never should be changed,
- `15-23`: hides the fact that the `RateComponent` Razor component's markup is generating icons with zero-based loop `@for (int i = 0; i < @MaxRate; i += 1)`. It also makes more consistent that the `tempRate` has to be set to the same value as selected (not just by clicking a icon which implies that `ShowRate(int index)` will also be called on mouse over with same `index` as selected `rate`).
- Access modifiers had been changed.

Make created base class available to the `RateComponent` by:

- adding a reference to the `Capgemini.Net.Blazor.Components.Demo` assembly:

```
<Project Sdk="Microsoft.NET.Sdk.Razor">
    ...

    <ItemGroup>
        ...
        <ProjectReference Include="..\Capgemini.Net.Blazor.Components.Demo\Capgemini.Net.Blazor.Components.Demo.csproj" />
        ...
    </ItemGroup>
</Project>
```

- adding `Capgemini.Net.Blazor.Components.Demo` namespace to be used globally within `Capgemini.Net.Blazor.Components.Demo03` assembly by adding `@using` directive to the `_Imports.razor` file:

```
@namespace Capgemini.Net.Blazor.Components.Demo03

// ...

@using Capgemini.Net.Blazor.Components.Demo
```

To make the `RateComponent` to inherit from some other class use `@inherits` directive ([read more](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#inherits)). As the namespace of the `RateComponentBase` class was added to `_Imports.razor` there is no need to specify classes fully qualified name. Class name only can be used:

```
@inherits RateComponentBase

@for (int i = 0; i < @MaxRate; i += 1)
<!-- ... -->
```

As `RateComponentBase` class handle the logic for the `RateComponent`, nearly all of the **C#** defined within the `@code { }` block can be deleted, leaving only properties that are parameters specific for the component's markup:

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
    [Parameter]
    public int MaxRate { get; set; } = 5;

    [Parameter]
    public string Icon { get; set; } = "fa-star";
}
```

**Note:** to make Razor component to inherit from other class, the base class of that other class either has to implement `IComponent` interface or derive from `ComponentBase` (which implements`IComponent` - [read more](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.componentbase?view=aspnetcore-5.0)). All Razor components derive/implement one of the above.

### Validate and update inner state of the RateComponent on parameter value change

In case the `MaxRate` decreases, currently selected value has to be updated accordingly - it should never be possible for the `RateComponent` to be in state when `Rate` property value is greater than `MaxRate`.

To ensure that, the [Blazor lifecycle](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0) method can be used.

Razor component allows to hook custom logic to its lifecycle in several points in time:

- `SetParametersAsync` - first lifecycle method to be run. By default it sets parameters supplied by the component's parent and decide which branch the component's lifecycle should go next ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#before-parameters-are-set)).
- If `SetParametersAsync` determinates that the Razor component was not initialized yet (it was ran from the first time) then the given lifecycle methods would be triggered:
  - `OnInitialized`/`OnInitializedAsync` are invoked when the component is initialized that is after having its parameters received from parent component (in `SetParametersAsync`). It can be used to perform some initialization logic that would normally be run by the constructor - it would be ran one-time only ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#component-initialization-methods)).
<br />**Note:** upon this step no **HTML** markup in rendered and no render-related data is available ([see example](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#detect-when-the-app-is-prerendering)).
<br />**Note:** `OnInitialized` is executed just before `OnInitializedAsync` and Razor component rerenders before the `OnInitializedAsync` is awaited if any `async` work is given. If not, the `OnParametersSet` will be called.
  - `OnParametersSet`/`OnParametersSetAsync` - it can be used to manually control the decision should a given component has to be refreshed (to be render again) - by default, components rerender if the parameter values changed (for primitive types) or may have changed (for example, if they are mutable objects - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#avoid-unnecessary-rendering-of-component-subtrees)).
<br />**Note:** upon this step no **HTML** markup in rendered and no render-related data is available.
<br />**Note:** `OnParametersSet` is executed just before `OnParametersSetAsync` and Razor component rerenders before the `OnParametersSetAsync` is awaited. Either way, the `OnAfterRenderAsync` will be called next.
- If `SetParametersAsync` determinates that the Razor component was already initialized (the `OnInitialized` was executed) then the given lifecycle methods would be triggered:
  - `OnParametersSet`/`OnParametersSetAsync` - it can be used to manually control the decision should a given component has to be refreshed (to be render again) - by default, components rerender if the parameter values changed (for primitive types) or may have changed (for example, if they are mutable objects - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#avoid-unnecessary-rendering-of-component-subtrees)).
<br />**Note:** upon this step no **HTML** markup in rendered and no render-related data is available.
<br />**Note:** `OnParametersSet` is executed just before `OnParametersSetAsync` and Razor component rerenders before the `OnParametersSetAsync` is awaited. Either way, the `OnAfterRenderAsync` will be called next.
- `OnAfterRender`/`OnAfterRenderAsync` - it can be used to trigger the <string>DOM</string>-specific logic (i.e. run **JavaScript** code).
<br />**Note:** `OnAfterRender` is executed just before `OnAfterRenderAsync`. Razor component won't rerender by default at this step.
<br />**Note:** a `firstRender` can be used inside the method to determinate if this is the first time the Razor component's **DOM** is rendered.

- `ShouldRender` - can be overloaded to decide if the entire Razor component should be rerendered (with nested components within). The method is called each time a state of the Razor component changes (or the `StateHasChanged` has been called manually) - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#suppress-ui-refreshing).
<br />**Note:** the method won't get called on the first time the Razor component is rendering.

Create a code-behind `RateComponent.razor.cs` file and use `OnParametersSet` lifecycle method to update the `Rate` parameter after each time it is changed from outside:

```
using Capgemini.Net.Blazor.Components.Demo;

namespace Capgemini.Net.Blazor.Components.Demo03.Start
{
    public partial class RateComponent : RateComponentBase
    {
        protected override void OnParametersSet()
        {
            if (Rate >= MaxRate)
            {
                Rate = MaxRate;
            }
        }
    }
}
```

After `OnParametersSet` and `OnParametersSetAsync` is executed, `StateHasChanged` would be called and a Razor component would be rerendered with conditionally new value `Rate`.