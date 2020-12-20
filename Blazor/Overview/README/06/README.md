![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/06/summary.jpg)

# Generic render fragment: give a child content a context of the parent

The below **README.md** file contains a transcript of the descriptions that can be found in the checklist for each of the exercise\`s points built into the Blazor WebAssembly application on-line ([link](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/)). You can directly go to that descriptions by navigating to any of those links:
 - [add a payload to the render fragment to pass a context object](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo06/checklist/1),
 - [pass context object to the child component](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo06/checklist/2).

### Add a payload to the render fragment to pass a context object

As stated in **./demo04**, a usage of `CascadingValue<TValue>` component wraps a subtree of the component hierarchy and supplies a single value to all components within that subtree ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters?view=aspnetcore-5.0)). Even if this is a simple way to provide any kind of primitive values or complex objects of a type `TValue` to any descendant component as it provides great deal of flexibility, it also makes it very easy to be overused. As mentioned [here](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#ensure-cascading-parameters-are-fixed), it can easily lead to poor performance of the application as dealing with `[CascadingParameter]` is more expensive that using a standard `[Parameter]`.

In cases when the cascading object should be provided by the parent component without having a child component either to know  the name of the `CascadingValue` that was used nor the type of the object that the `CascadingValue` is supplying (for example 3<sup>ed</sup> party library might providing a cascading value of a given type and other 3<sup>ed</sup> party library libraries that wish to use it have no way of knowing that type), the `RenderFragment` with `TValue` [generic type](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.renderfragment-1?view=aspnetcore-5.0) can be used ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#razor-templates) or [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-parameters)).

Replace the `RenderFragment ChildContent` parameter within `./Start/ContainerComponent.razor.cs` code-behind file for the `RenderFragment` with a generic type:

```
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo06.Start
{
    public partial class ContainerComponent
    {
        private readonly RateContext rateContext = new RateContext();

        [Parameter]
        public `RenderFragment<RateContext>` ChildContent { get; set; } = default!;
    }
}
```

Remove the `CascadingValue` from the `ContainerComponent` and provide a `rateContext` variable as a parameter for the generic `@ChildContent` invocation (if not provided, then the variable `@ChildContent` will be rendered as `@(ChildContent.ToString())`):

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
		<!-- ... -->
    </EditForm>

    <div class="demo__container">
        @ChildContent(rateContext)
    </div>
</div>
```

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/06/img/1.jpg)

Remove the `CascadingParameter` attribute that decorates the `RateContext` property in the `RateComponent.razor` file as it is not needed anymore:

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
    <div class="average-rate-container">
        @(RateContext.AvgRate)
    </div>
</div>

@code {
    public RateContext RateContext { get; set; } = default!;
}
```

**Note:** at this point code execution will result in a runtime `NullReferenceException` - as the `RateContext` for `RateComponent` is no longer has its value supplied by some `CascadingValue` and it also not supplied in any other way (property is not marked with `Parameter` attribute), the `RateContext` is always `null` which cause the `OnParametersSet()` lifecycle method to throw.

### Pass context object to the child component

To supply the value for the `RateContext` property of the `RateComponent`, open `RateComponent` Razor file and make the property to be a parameter. That will allow to use the name of the property as a **HTML** attribute (`<RateComponent RateContext="..." />`):

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
    <div class="average-rate-container">
        @(RateContext.AvgRate)
    </div>
</div>

@code {
    [Parameter]
    public RateContext RateContext { get; set; } = default!;
}
```

Open the `./Start/WrapperComponent.razor` file and supply the given markup for the component:

```
<ContainerComponent>
    <RateComponent `RateContext="@context"` />
</ContainerComponent>
```

Using a generic `RenderFragment` to supply a child content markup for the component provides a `@context` variable. This is a default name for the same object that was provided as a parameter to invoke the `RenderFragment` from the parent component's markup (i.e. `@ChildContent(rateContext)`). It can also be renamed by assigning a value to the `Context` attribute ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-context-parameters)):

```
<ContainerComponent>
    <ChildContent `Context="rateContext"`>
        <RateComponent RateContext="@rateContext" />
    </ChildContent>
</ContainerComponent>
```

**Note:** in order to rename the default `@context` variable, the tag named after the `RenderFragment<TValue>` parameter of the parent component has to be given explicitly ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-parameters)). If the `@context` variable doesn't need to be renamed and the parent component introduces only one parameter of type `RenderFragment` or `RenderFragment<TValue>`, the markup of `<ChildContent>` and `</ChildContent>` can be omitted (if a component defines a single property of `RenderFragment` or `RenderFragment<TValue>`, it has to be named `ChildContent` by convention so the additional markup can be inferred - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#child-content)).