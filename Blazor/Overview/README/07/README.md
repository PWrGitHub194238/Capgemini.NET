![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/07/Summary.jpg)

# Multiple component fragments: customization of the component by replacing child content elements

The below **README.md** file contains a transcript of the descriptions that can be found in the checklist for each of the exercise\`s points built into the Blazor WebAssembly application on-line ([link](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/)). You can directly go to that descriptions by navigating to any of those links:
 - [separate the average counter from the rating component](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo07/checklist/1),
 - [Replace default average rate markup with replaceable render fragment](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo07/checklist/2).

### Separate the average counter from the rating component

Move the given markup from the `RateComponent` to the `ContainerComponent.razor` file:

```
<div class="average-rate-container">
	@(RateContext.AvgRate)
</div>
```

```
@inherits RateComponentBase

<div class="rate-container">
    <div class="icon-rate-container">
        @for (int i = 0; i < @RateContext.MaxRate; i += 1)
        {
            int index = i;
            <i class="@(IsActive(i) ? ACTIVE_STYLE : INACTIVE_STYLE) @(RateContext.Icon) cursor-pointer"
               @onclick="SetRate"
               @onmouseover="@(() => ShowRate(index))"
               @onmouseout=RevertRate></i>
        }
    </div>
</div>

@code {
    [Parameter]
    public RateContext RateContext { get; set; } = default!;
}
```

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

    <div class="demo__container">
        @ChildContent(rateContext)
    </div>

    <div class="average-rate-container">
        @(rateContext.AvgRate)
    </div>
</div>
```

Note that the child content of the `<div class="average-rate-container">` was changed from `@(RateContext.AvgRate)` to `@(rateContext.AvgRate)` (line `29`).

As the markup that was moved between components is using a `average-rate-container` **CSS** class, it has to be moved as well. Delete the **CSS** class from the `RateComponent.razor.scss` file, leaving only `rate-container` and `icon-rate-container` class definitions and add deleted classes to the styles of the `ContainerComponent`):

```
.rate-container {
    display: flex;
    flex-direction: column;
    align-items: center;

    .icon-rate-container {
        display: flex;
    }
}
```

```
.demo__container_wrapper {
	/* ... */

    .demo__container {
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

### Replace default average rate markup with replaceable render fragment

Add the second `RenderFragment` to the code-behind file for the `ContainerComponent`:

```
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo07.Start
{
    public partial class ContainerComponent
    {
		// ...

        [Parameter]
        public RenderFragment<RateContext> ChildContent { get; set; } = default!;

        [Parameter]
        public RenderFragment AvgRate { get; set; } = default!;
    }
}
```

Replace the markup of the Razor file for that component (`ContainerComponent.razor`) to reflect the code-behind changes to display a new `RenderFragment`, instead of displaying the `rateContext.AvgRate` value:

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
		<!-- ... -->
    </EditForm>

    <div class="demo__container">
        @ChildContent(rateContext)
    </div>

    <div class="average-rate-container">
        @AvgRate
    </div>
</div>
```

Additional `RenderFragment` enables a user of the `ContainerComponent` to define a way to display the average rate independently of the `RateComponent` (or any other markup/component provided for `ChildContent`). That enables the child content of the `ContainerComponent` to be defined in the `WrapperComponent.razor` file:

```
<ContainerComponent>
    <ChildContent Context="rateContext">
        <RateComponent RateContext="@rateContext" />
    </ChildContent>
    <AvgRate>
        <DefaultAverageRateComponent />
    </AvgRate>
</ContainerComponent>
```

**Note:** if the component defines only one parameter of type `RenderFragment`/`RenderFragment<TValue>`, the `<ChildContent>` tag can be omitted as long as the property for `RenderFragment`/`RenderFragment<TValue>` has a name of `ChildComponent` ([by convention](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#child-content)).

```
<ContainerComponent>
    <!-- <ChildContent> -->
        <RateComponent RateContext="@context" />
    <!-- </ChildContent> -->
</ContainerComponent>
```

In case of [multiple](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters?view=aspnetcore-5.0#tabset-example) render fragment parameters defined (or if there is a need to rename the default `@context` for `RenderFragment<TValue> ChildContent` parameter - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-context-parameters)), for each render fragment the tag with the same name as render fragment it represents has to be defined explicitly (i.e. `<ChildContent>` for `ChildContent` parameter of type `RenderFragment<RateContext>` and `<AvgRate>` for `AvgRate` property):

- to provide a clear separation of multiple render fragments - each child markup of the `<X>` tag is treated as a content for the `RenderFragment`/`RenderFragment<TValue>` parameter named `X`,
- to make the default `@context` for each of the parameters of type `RenderFragment<TValue>` to be clearly distinguished by name.

To provide a context for the `AvgRate` render fragment, change the type of the `AvgRate` property to the generic version of `RenderFragment`:

```
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo07.Start
{
    public partial class ContainerComponent
    {
        // ...

        [Parameter]
        public `RenderFragment<AverageRateContext>` AvgRate { get; set; } = default!;
    }
}
```

Create a new class named `AverageRateContext`. All components that are going to be defined within the `<AvgRate>` and `</AvgRate>` tag pair will be able to use that class (for example to provide values for their parameters):

```
namespace Capgemini.Net.Blazor.Components.Demo07.Start
{
    public class AverageRateContext
    {
        public int MinRate { get; set; }

        public int AvgRate { get; set; }

        public int MaxRate { get; set; }
    }
}
```

Provide a default instance of that class inside the code-behind `ContainerComponent.razor.cs` file to serve as a context object for the defined `AvgRate` render fragment parameter (one which accepts `AverageRateContext` type as a generic parameter):

```
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo07.Start
{
    public partial class ContainerComponent
    {
        private readonly RateContext rateContext = new RateContext();

        private AverageRateContext AverageRateContext => new AverageRateContext()
        {
	        MinRate = 1,
	        AvgRate = rateContext.AvgRate,
	        MaxRate = rateContext.MaxRate,
        };

        [Parameter]
        public RenderFragment<RateContext> ChildContent { get; set; } = default!;

        [Parameter]
        public RenderFragment<AverageRateContext> AvgRate { get; set; } = default!;
    }
}
```

Note that both `AvgRate` and `MaxRate` properties of the `AverageRateContext` object can be received from the `rateContext` filed. Add a parameter for the generic `@AvgRate` render fragment invocation inside the `ContainerComponent`'s markup:

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

    <div class="demo__container">
        @ChildContent(rateContext)
    </div>

    <div class="average-rate-container">
        @AvgRate`(AverageRateContext)`
    </div>
</div>
```

Change the markup of the `WrapperComponent.razor` file to use the new `@context` variable for the `<AvgRate>` part, to make the child component receive its parameters from the `ContainerComponent` and react if one of those were changed by the parent:

```
<ContainerComponent>
    <ChildContent Context="rateContext">
        <RateComponent RateContext="@rateContext" />
    </ChildContent>
    <AvgRate Context="avgContext">
        <DefaultAverageRateComponent
            MinRate="@(avgContext.MinRate)"
            AvgRate="@(avgContext.AvgRate)"
            MaxRate="@(avgContext.MaxRate)" />
    </AvgRate>
</ContainerComponent>
```

**Note:** it is also possible to not provide a custom name for either of the `Context` parameters - the distinction of the `ChildContent`'s `@context` variable and the `@context` for `AvgRate ` will be handled implicitly:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/07/img/1.jpg)

**Note:** it is not possible to omit the wrapping tags for the `ChildContent` render fragments if multiple render fragments are defined bu the component. Doing so will cause the compilation-time exception to be thrown:

- `Unrecognized child content inside component 'ContainerComponent'. The component 'ContainerComponent' accepts child content through the following top-level items: 'ChildContent', 'AvgRate'`

If `ContainerComponent` defines multiple render fragments, only tags with names of its render fragments are allowed. It is possible to not define all of its render fragments but for each that were not defined, parent component has to handle it by either not displaying it (checking for its nullability) or by providing a default markup ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#define-reusable-renderfragments-in-code), [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/advanced-scenarios?view=aspnetcore-5.0#manual-rendertreebuilder-logic)).

**Note:** in oppose to the `RateComponent`, `DefaultAverageRateComponent` defines its parameters as multiple primitives, not a complex object. This is a performance tradeoff:

- by providing only the primitive types as parameters, framework can determinate on its own whenever it should skip the rendering process for the component and its sub-components (by determinate if none of these parameter values have changed). This is not possible for mutable objects - in that case component rerenders if the parameter complex object may have changed ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#avoid-unnecessary-rendering-of-component-subtrees)).
- The more parameters the component receives (the more properties of the component is marked either with `[Parameter]` or `[CascadingParameter]` - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#before-parameters-are-set)), the more expensive it will be to handle that component (`SetParametersAsync` lifecycle method would need to look for values for all parameters which is done by the reflection - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#implement-setparametersasync-manually)). However changing the number of parameters will make noticeable impact on performance only in extreme cases when the given component is being overused - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#dont-receive-too-many-parameters).

Now as the `ContainerComponent` is able to receive `AvgRate` in addition to the `ChildContent` render fragment, the number of combination for the below markup grown:

```
<ContainerComponent>
    <ChildContent>
        <X/>
    </ChildContent>
    <AvgRate>
        <Y />
    </AvgRate>
</ContainerComponent>
```

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/07/img/2.jpg)


![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/07/img/3.jpg)


![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/07/img/4.jpg)