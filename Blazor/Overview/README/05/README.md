![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/05/summary.jpg)

# Built-in components: using Blazor forms to modify custom model for a component

The below **README.md** file contains a transcript of the descriptions that can be found in the checklist for each of the exercise\`s points built into the Blazor WebAssembly application on-line ([link](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/)). You can directly go to that descriptions by navigating to any of those links:
 - [add additional parameter for component representing average rate](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo05/checklist/1),
 - [group rate parameters to form context model for the component](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo05/checklist/2),
 - [replace raw HTML markup for context model properties modification with custom components](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo05/checklist/3),
 - [replace custom parameter value selectors with built-in form component](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo05/checklist/4),
 - [replace built-in input components with custom wrap components to add styles for inputs and custom <option> tag markup](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo05/checklist/5).

### Add additional parameter for component representing average rate

Open `./Start/RateComponent.razor` and introduce a new property `AvgRate` decorated with the `Parameter` attribute (with `Name = "AvgRate"` which will be used as `Name` value for `CascadingValue` component):

```
@inherits RateComponentBase

<!-- ... -->

@code {
    [CascadingParameter(Name = "MaxRate")]
    public int MaxRate { get; set; } = 5;

    [CascadingParameter(Name = "AvgRate")]
    public int AvgRate { get; set; } = 3;

    [CascadingParameter]
    public string Icon { get; set; } = "fa-star";
}
```

Modify the **HTML** markup of the same file (`./Start/RateComponent.razor`) and supply additional **CSS** styles (add new file `./Start/RateComponent.razor.scss` and compile it to generate `./Start/RateComponent.razor.css`):

```
@inherits RateComponentBase

<div class="rate-container">
    <div class="icon-rate-container">
        @for (int i = 0; i < @MaxRate; i += 1)
        {
            int index = i;
            <i class="@(IsActive(i) ? ACTIVE_STYLE : INACTIVE_STYLE) @Icon cursor-pointer"
               @onclick="SetRate"
               @onmouseover="@(() => ShowRate(index))"
               @onmouseout=RevertRate></i>
        }
    </div>
    <div class="average-rate-container">
        @AvgRate
    </div>
</div>

@code {
    // ...
}
```

```
.rate-container {
    display: flex;
    flex-direction: column;
    align-items: center;

    .icon-rate-container {
        display: flex;
    }

    .average-rate-container {
        display: flex;
        align-items: center;
        font-family: 'Ubuntu';
        font-weight: bold;
        font-size: large;

        &:before {
            content: 'Average rate: ';
            font-family: 'Ubuntu';
            font-weight: bold;
            text-transform: uppercase;
            font-size: larger;
            padding-right: 15px;
        }
    }
}
```

Open the `./Start/ContainerComponent.razor.cs` file to provide a property with a basic logic for the additional `AvgRate` parameter. `ContainerComponent` will be using it as the other two properties (`MaxRate`, `IconIndex`) to control how the `RateComponent` (provided as a `ChildContent`) is being rendered:

```
// ...

private int maxRate = 6;
private int avgRate = 3;
private int iconIndex;

// ...

public int MaxRate
{
    get => maxRate;
    set => maxRate = Math.Max(2, value);
}

public int AvgRate
{
    get => avgRate;
    set => avgRate = Math.Max(1, Math.Min(MaxRate, value));
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

// ...
```

Open the Razor component for the code-behind class shown above and add a markup for another property (`ContainerComponent.razor` - lines `12-17`). To enable `ContainerComponent` to pass `AvgRate` value, add additional nested layer of cascading values to provide the that property for the `RateComponent` (lines `27` and `33`):

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

    <span class="demo__label">Avg rate</span>
    <div class="demo__selector">
        <MinusIcon Theme="IconTheme.DARK" OnClick="@(() => AvgRate -= 1)" />
        <strong>@AvgRate</strong>
        <PlusIcon Theme="IconTheme.DARK" OnClick="@(() => AvgRate += 1)" />
    </div>

    <span class="demo__label">Icon</span>
    <div class="demo__selector">
        <MinusIcon Theme="IconTheme.DARK" OnClick="@(() => IconIndex -= 1)" />
        <i class="far @this[iconIndex]"></i>
        <PlusIcon Theme="IconTheme.DARK" OnClick="@(() => IconIndex += 1)" />
    </div>

    <CascadingValue Name="MaxRate" Value="@MaxRate">
        <CascadingValue Name="AvgRate" Value="@AvgRate">
            <CascadingValue Value="@this[iconIndex]">
                <div class="demo__container">
                    @ChildContent
                </div>
            </CascadingValue>
        </CascadingValue>
    </CascadingValue>
</div>
```

**Note:** Any markup provided by the `@ChildContent` will be able to use any of the provided cascading values:

- `@MaxRate` by the given name (`[CascadingParameter(Name = "MaxRate")]`),
- `@AvgRate` by the given name (`[CascadingParameter(Name = "AvgRate")]`),
- `@this[iconIndex]` by the type - `string` in this case (`[CascadingParameter]`).

`RateComponent` is provided as the `@ChildContent` render fragment parameter by the `WrapperComponent` in this case.

### Group rate parameters to form context model for the component

Having multiple `CascadingValue` components may have substantial impact of the application performance (unless a variable that is supplied by the component can be marked as fixed - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#ensure-cascading-parameters-are-fixed)). To avoid that kind of situation one can group multiple variables (that each one is supplied by one `CascadingValue`) into a separated context class. By doing so, it won't only make the **HTML** markup less nested and simpler to read and maintain but it also enables the supplied variable to be a fixed reference rather than set of primitive types. Therefore change the given lines of the `ContainerComponent.razor` file:

```
<!-- ... -->

    <CascadingValue Name="MaxRate" Value="@MaxRate">
        <CascadingValue Name="AvgRate" Value="@AvgRate">
            <CascadingValue Value="@this[iconIndex]">
                <div class="demo__container">
                    @ChildContent
                </div>
            </CascadingValue>
        </CascadingValue>
    </CascadingValue>
</div>
```

with a single `CascadingValue` component that will provide an object with three inner properties with desired values:

```
<!-- ... -->

    <CascadingValue Value="@rateContext">
        <div class="demo__container">
            @ChildContent
        </div>
    </CascadingValue>
</div>
```

Create an empty public `RateContext` class in the `End` directory. Add a `readonly` field named `RateContext` with type of that class to the `ContainerComponent.razor.cs` code-behind file:

```
namespace Capgemini.Net.Blazor.Components.Demo05.Start
{
    public class RateContext
    {
    }
}
```

```
using Microsoft.AspNetCore.Components;
using System;

namespace Capgemini.Net.Blazor.Components.Demo05.Start
{
    public partial class ContainerComponent
    {
        private readonly RateContext rateContext = new RateContext();

        // ...
    }
}
```

**Note:** using `CascadingValue` with a reference to `*Context` class often leads to rely on the supplied value type. It may safe a lit of debugging time in case the name of the `CascadingValue` was changed accidentally (either in `<CascadingValue Name="**SuppliedValueName**" ...>` or `[CascadingParameter(Name = "**SuppliedValueName**")]` part) or start to collide with any other `CascadingValue` which was named the same (and based on whenever that `CascadingValue` is an ancestor/descendant of another `CascadingValue`, one or another supplied value will be used). Such `CascadingValue` might be used as a part of the 3<sup>ed</sup> party library. Using `CascadingValue` that is based on the complex type solves both of these problems.

**Note:** marking `CascadingValue` that supplies a complex object reference as fixed (`<CascadingValue Value="@rateContext" IsFixed="true">`) makes components not be rerendered on that complex object state change - [read more](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.cascadingvalue-1.isfixed?view=aspnetcore-5.0#Microsoft_AspNetCore_Components_CascadingValue_1_IsFixed). To make the component to be rerender it either has to:

- have a value of any of its primitive property marked with `[Parameter]` changed by a parent component ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#avoid-unnecessary-rendering-of-component-subtrees)),
- have complex type `[Parameter]` (not `[CascadingParameter]`) supplied by its parent ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#after-parameters-are-set)) or
- trigger either of its events ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/event-handling?view=aspnetcore-5.0#eventcallback)).

Regardless of whether the component will be rerenered or not, its property marked with `[CascadingParameter]` refers to the same address in memory so it will reflect any changes.

Refactor the `ContainerComponent.razor.cs` file:

- move all `private` primitive fields to the `RateContext` class (lines `7-16`),
- move `MaxRate`, `AvgRate` and `IconIndex` properties to the `RateContext` class (lines `20-40`),
- remove the indexer and add a `getter` for the icon (line `18`):

```
using System;

namespace Capgemini.Net.Blazor.Components.Demo05.Start
{
    public class RateContext
    {
        private int maxRate = 6;
        private int avgRate = 3;
        private int iconIndex;

        public static readonly string[] Icons = {
            "fa-star",
            "fa-grin-stars",
            "fa-angry",
            "fa-sun"
        };

        public string Icon => Icons[IconIndex];

        public int MaxRate
        {
            get => maxRate;
            set => maxRate = Math.Max(2, value);
        }

        public int AvgRate
        {
            get => avgRate;
            set => avgRate = Math.Max(1, Math.Min(MaxRate, value));
        }

        public int IconIndex
        {
            get => iconIndex;
            set => iconIndex = value < 0
                ? Icons.Length - 1
                : value >= Icons.Length
                ? 0
                : value;
        }
    }
}
```

The content of the code-behind of the `ContainerComponent.razor` should have only its parameter properties and a reference to the `RateContext`:

```
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo05.Start
{
    public partial class ContainerComponent
    {
        private readonly RateContext rateContext = new RateContext();

        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;
    }
}
```

Update the main markup of the `ContainerComponent` to refer the properties of the new `rateContext` field:

```
@using Capgemini.Net.Blazor.Components.SvgIcons
@using SvgIcons.Base

<div class="demo__container_wrapper">
    <span class="demo__label">Max rate</span>
    <div class="demo__selector">
        <MinusIcon Theme="IconTheme.DARK" OnClick="@(() => `rateContext.MaxRate` -= 1)" />
        <strong>`@(rateContext.MaxRate)`</strong>
        <PlusIcon Theme="IconTheme.DARK" OnClick="@(() => `rateContext.MaxRate` += 1)" />
    </div>

    <span class="demo__label">Avg rate</span>
    <div class="demo__selector">
        <MinusIcon Theme="IconTheme.DARK" OnClick="@(() => `rateContext.AvgRate` -= 1)" />
        <strong>`@(rateContext.AvgRate)`</strong>
        <PlusIcon Theme="IconTheme.DARK" OnClick="@(() => `rateContext.AvgRate` += 1)" />
    </div>

    <span class="demo__label">Icon</span>
    <div class="demo__selector">
        <MinusIcon Theme="IconTheme.DARK" OnClick="@(() => `rateContext.IconIndex` -= 1)" />
        <i class="far `@(rateContext.Icon)`"></i>
        <PlusIcon Theme="IconTheme.DARK" OnClick="@(() => `rateContext.IconIndex` += 1)" />
    </div>

    <CascadingValue Value="@rateContext">
        <div class="demo__container">
            @ChildContent
        </div>
    </CascadingValue>
</div>
```

Lastly, change the `RateComponent.razor` and `RateComponent.razor.cs` files to use the cascade value that is supplied by the `ContainerComponent`:

```
@inherits RateComponentBase

<div class="rate-container">
    <div class="icon-rate-container">
        @for (int i = 0; i < `@RateContext.MaxRate`; i += 1)
        {
            int index = i;
            <i class="@(IsActive(i) ? ACTIVE_STYLE : INACTIVE_STYLE) `@(RateContext.Icon)` cursor-pointer"
               @onclick="SetRate"
               @onmouseover="@(() => ShowRate(index))"
               @onmouseout=RevertRate></i>
        }
    </div>
    <div class="average-rate-container">
        `@(RateContext.AvgRate)`
    </div>
</div>

@code {
    [CascadingParameter]
    public RateContext RateContext { get; set; } = default!;
}
```

```
using Capgemini.Net.Blazor.Components.Demo;

namespace Capgemini.Net.Blazor.Components.Demo05.Start
{
    public partial class RateComponent : RateComponentBase
    {
        protected override void OnParametersSet()
        {
            if (Rate >= `RateContext.MaxRate`)
            {
                Rate = `RateContext.MaxRate`;
            }
        }
    }
}
```

### Replace raw HTML markup for context model properties modification with custom components

A content of the `ContainerComponent.razor` starts to get duplicated. Each property of the `RateContext` class needs its own markup in the component to enable a user to change it:

- `Max rate`

```
<span class="demo__label">Max rate</span>
<div class="demo__selector">
	<MinusIcon Theme="IconTheme.DARK" OnClick="@(() => rateContext.MaxRate -= 1)" />
	<strong>@(rateContext.MaxRate)</strong>
	<PlusIcon Theme="IconTheme.DARK" OnClick="@(() => rateContext.MaxRate += 1)" />
</div>
```

- `Avg rate`

```
<span class="demo__label">Avg rate</span>
<div class="demo__selector">
	<MinusIcon Theme="IconTheme.DARK" OnClick="@(() => rateContext.AvgRate -= 1)" />
	<strong>@(rateContext.AvgRate)</strong>
	<PlusIcon Theme="IconTheme.DARK" OnClick="@(() => rateContext.AvgRate += 1)" />
</div>
```

- `Icon`

```
<span class="demo__label">Icon</span>
<div class="demo__selector">
	<MinusIcon Theme="IconTheme.DARK" OnClick="@(() => rateContext.IconIndex -= 1)" />
	<i class="far @(rateContext.Icon)"></i>
	<PlusIcon Theme="IconTheme.DARK" OnClick="@(() => rateContext.IconIndex += 1)" />
</div>
```

Change the content of the `ContainerComponent`:

```
@using SvgIcons.Base

<div class="demo__container_wrapper">
    <IntSelector Label="Max rate" @bind-Value="@(rateContext.MaxRate)" Theme="IconTheme.DARK">
        <strong>@context</strong>
    </IntSelector>

    <IntSelector Label="Avg rate" @bind-Value="@(rateContext.AvgRate)" Theme="IconTheme.DARK">
        <strong>@context</strong>
    </IntSelector>

    <IntSelector Label="Icon" @bind-Value="@(rateContext.IconIndex)" Theme="IconTheme.DARK" StringValue="@((_) => rateContext.Icon)">
        <i class="far @context"></i>
    </IntSelector>

    <CascadingValue Value="@rateContext">
        <div class="demo__container">
            @ChildContent
        </div>
    </CascadingValue>
</div>
```

`IntSelector` is a component from `Capgemini.Net.Blazor.Components.Demo` assembly that defines:

- simple `Label` parameter that will internally generate the `<span class="demo__label">@Label</span>` markup:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/05/img/1.jpg)

- [Data binding](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0) for `rateContext` properties `MaxRate`, `AvgRate` and `IconIndex`. Data binding will be more covered in **./demo11**. At this point, that would be enough to say that thanks to the power of data binding, the inner state of the `IntSelector` component (which is represented by the parameter `Value`) will be in sync with the binded value. So whenever `Value` changes, `rateContext.MaxRate` also changes and vice-versa (`@bind-Value="@(rateContext.MaxRate)"`):

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/05/img/2.jpg)

- Simple `Theme` parameter to decide which color both arrow icons should have:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/05/img/3.jpg)

- `StringValue` parameter that is a simple predicate accepting `int` value and returning the `string` to be displayed (`@(_) => rateContext.Icon` transforms `rateContext.IconIndex` to `Icons[IconIndex]`). That value then is provided for child content within `IntSelector` tags:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/05/img/4.jpg)

when the ` GetAvgRateLabel(avgRate)` is defined as:

```
private static string GetAvgRateLabel(int agvRate) => agvRate switch
{
	int rate when rate is < 3 => $"{agvRate} (poor)",
	int rate when rate is >= 3 and < 5 => $"{agvRate} (good)",
	_ => $"{agvRate} (excellent)",
};
```

- It also defines a special variable called `@context` which is a part of the [Blazor templated components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0) (such as `IntSelector`). That also would be covered more in depth in the next demo (**./demo06**). For `IntSelector` the `@context` is defined in a way that provides a value returned by the `StringValue` predicate. Then it can be used by `@ChildContent` of the component (which in this case is `<i class="far @context"></i>` and the `@context` value itself is defined with `StringValue="@((_) => rateContext.Icon)"`):

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/05/img/5.jpg)

### Replace custom parameter value selectors with built-in form component

Although `IntSelector` component made code smaller and less complex by wrapping details about handling the state of the `RateContext` class properties, it has the following limitations:

- it supports only one primitive type of value - `int`. For more complex cases the `StringValue` delegate can be used but it is still based on the single `int` property.
- It assumes that any given `int` property will be increased/decreased by `1`.
- It doesn't support any kind of cross-field validation - simple property validation can be achieved by defining more complex logic inside a `setter` of the property bounded to the `Value` parameter of the component.

To solve those problems, a [EditForm](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.editform?view=aspnetcore-5.0) component can be used along with other [build-in Razor components](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#built-in-forms-components) like:

- `InputNumber<TValue>`,
- `InputSelect<TValue>`.

Replace the markup for all custom input fields with build-in components:

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
        <span class="demo__label">Max rate</span>
        <InputNumber class="demo__selector" @bind-Value="@rateContext.MaxRate" />

        <span class="demo__label">Avg rate</span>
        <InputNumber class="demo__selector" @bind-Value="@rateContext.AvgRate" />

        <span class="demo__label">Icon</span>
        <InputSelect class="demo__selector" @bind-Value="rateContext.IconIndex">
            @for(int i = 0; i < RateContext.Icons.Length; i += 1)
            {
                <option value="@i">
                    @(RateContext.Icons[i])
                </option>
            }
        </InputSelect>
    </EditForm>

    <CascadingValue Value="@rateContext">
        <div class="demo__container">
            @ChildContent
        </div>
    </CascadingValue>
</div>
```

To make use of build-in `EditForm`, `InputNumber` and `InputSelect` components, add `Microsoft.AspNetCore.Components.Forms` namespace to the `_Imports.razor` file:

```
@namespace Capgemini.Net.Blazor.Components.Demo05

@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Rendering
@using Microsoft.AspNetCore.Components.Web.Extensions.Head
@using Microsoft.AspNetCore.Components.Forms

@using Capgemini.Net.Blazor.Components.Tile
@using Capgemini.Net.Blazor.Components.Tile.Base
@using Capgemini.Net.Blazor.Components.Demo
```

The snippet code above gathers three input fields under one common [edit context](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.editcontext?view=aspnetcore-5.0). It allows to define forms in the way known from previous **ASP.NET** frameworks. For instance it enables validation based on `RateContext` [validation attributes](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#validation-attributes) like `[Required]` or `[Range]` (which will be introduced in **./demo08**).

All `Input*` components rely on data binding to modify model's properties on any input change.

**Note:** introducing standard `<input>` markup shows that the validation based on property `setters` is not prohibiting user to select invalid values. In case the invalid value was entered, firstly it will be accepted as an input. Due to input's value change, `setter` of the binded property will be triggered (e.i. `@bind-Value="@rateContext.MaxRate"`) which will assign a value to the inner private field of the model. Due to the invalid value received by the `setter`, it will assign a different value (after validation) to the field which in turn will trigger component to be rendered again (as the given property is two-way binded to the `Input*` component's `Value` parameter). Lastly, changed value will be displayed to the user. To create a proper validation, [validation attributes](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#validation-attributes) have to be used. Alternatively, custom validation can be implemented either within a custom component that inherits from `Input*` (to prevent user to input invalid values) or by `OnSubmit` event handler of an `EditForm` component - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#built-in-forms-components).

### Replace built-in input components with custom wrap components to add styles for inputs and custom <option> tag markup

Note that it is no longer possible to preview the icon user is selecting from the input. Replace the inner content of the `<option>` markup from `@(RateContext.Icons[i])` to display the preview of the icon and a class name that is used to display it:

```
<!-- ... -->
@for(int i = 0; i < RateContext.Icons.Length; i += 1)
{
    string icon = RateContext.Icons[i];
    <option value="@i">
        <div class="fa-cap-option">
	        <i class="far @icon" /> - <strong>@icon</strong>
        </div>
    </option>
}
<!-- ... -->
```

One major flow of this solution is that **HTML** specification doesn't allow any additional markup between `<option>` and `</option>` ([read more](https://html.spec.whatwg.org/multipage/form-elements.html#the-option-element)) so the usage of [Font Awesome markup classes](https://fontawesome.com/icons/star?style=solid) is not possible. Browser will strip away all additional markup form the following code, leaving only raw text.

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/05/img/6.jpg)

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/05/img/7.jpg)

Replace the content of the `ContainerComponent` for the last time to make it use a custom components which provide:

- custom styling for `Input*` components,
- default style and markup for a label of the given input within the component,
- support for any custom markup to be placed between `<option>` and `</option>` tags.

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
        <CapInput Label="Max rate" InputName="@nameof(rateContext.MaxRate)">
            <InputNumber @bind-Value="@rateContext.MaxRate" />
        </CapInput>

        <CapInput Label="Avg rate" InputName="@nameof(rateContext.AvgRate)">
            <InputNumber @bind-Value="@rateContext.AvgRate" />
        </CapInput>

        <CapSelect Label="Icon" @bind-Value="rateContext.IconIndex" ParseValue="@((option) => RateContext.Icons[option])">
            @for (int i = 0; i < RateContext.Icons.Length; i += 1)
            {
                string icon = RateContext.Icons[i];
                <CapOption Key="@i.ToString()" Value="@i">
                    <div class="fa-cap-option">
                        <i class="far @icon" /> - <strong>@icon</strong>
                    </div>
                </CapOption>
            }
        </CapSelect>
    </EditForm>

    <CascadingValue Value="@rateContext">
        <div class="demo__container">
            @ChildContent
        </div>
    </CascadingValue>
</div>
```

Same as `IntSelector` component that was used earlier, both `CapInput` and `CapSelect` components use `RenderFragment` parameter named `ChildContent` to define their inner content as shown below. `CapInput` child content is used to provide any of the build-in `Input*` components. See the actual component's markup:

```
<div class="field-container">
    <span class="field-label">
        @Label
    </span>
    <span style="width: 100%;"></span>
    <div class="field">
        @ChildContent
        <label for="@InputName">@Label</label>
    </div>
</div>
```

Highlighted `@ChildContent` is replaced by the `<InputNumber @bind-Value="@rateContext.MaxRate" />` / `<InputNumber @bind-Value="@rateContext.AvgRate" />`.

`CapSelect` component works in the same way, allowing to define `CapOption` component in place of the standard `<option>` tag ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters?view=aspnetcore-5.0#tabset-example) for a similar example). `(option) => RateContext.Icons[option]` lambda expression is used to provide a display value for any option selected (instead of showing the raw selected value that binds to the input - `rateContext.IconIndex`).