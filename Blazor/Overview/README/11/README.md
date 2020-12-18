### Replace HTML markup for the icons with a component, use event callbacks to use base class logic

Add new Razor component named `RateIconComponent`:

```
<i class="@(IsActive ? ActiveCss : InActiveCss) @(Icon) cursor-pointer"
    alt="@IconRate"
    @onclick="@(() => ValueChanged.InvokeAsync(IconRate))"
    @onmouseover="@(() => OnFocusChanged.InvokeAsync(true))"
    @onmouseout="@(() => OnFocusChanged.InvokeAsync(false))"></i>

@code {
    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public int IconRate { get; set; }

    [Parameter]
    public int Value { get; set; }

    [Parameter]
    public int FocusedValue { get; set; }

    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    [Parameter]
    public bool IsFocused { get; set; }

    [Parameter]
    public EventCallback<bool> OnFocusChanged { get; set; }

    [Parameter]
    public string ActiveCss { get; set; } = "fas";

    [Parameter]
    public string InActiveCss { get; set; } = "far";

    private bool IsActive => (IsFocused && IconRate <= FocusedValue)
        || (!IsFocused && IconRate <= Value);
}
```

The markup and Blazor event handlers used for the component are the same as for the original markup inside the `RateComponent`:

```
@inherits RateComponentBaseWithTask

@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <div class="icon-rate-container">
        @foreach (int i in Enumerable.Range(Product.MinRate - 1, Product.MaxRate - Product.MinRate + 1))
        {
            <i class="@(IsActive(i) ? ACTIVE_STYLE : INACTIVE_STYLE) @(Icon) cursor-pointer"
               @onclick="SetRate"
               @onmouseover="@(() => ShowRate(i))"
               @onmouseout=RevertRate></i>
        }
    </div>
</div>

<!-- ... -->

@code {
	// ...
}
```

`RateIconComponent` does not inherits from the `RateComponentBaseWithTask` class which holds the context variables for the above code to work:

- `SetRate` method sets the current rate of the product based on whatever icon was clicked while being focused,
- `ShowRate(i)` sets the temporary rate (`private int tempRate`) based on the icon's index `i`. Once `SetRate` is invoked, `tempRate` value is assigned to the selected rate (can be get by the public `Rate` property).

`RateIconComponent` needs to notify the `RateComponent` whenever it is focused to simulate the `ShowRate(i)`. `RateIconComponent` uses `EventCallback<bool> OnFocusChanged` parameter to provide that and it is called on both `@onmouseover` and `@onmouseout` (to also notify other `RateIconComponent`s about focus state whenever the icon is not hovered anymore):

- `@onmouseover="@(() => OnFocusChanged.InvokeAsync(true))"`
- `@onmouseout="@(() => OnFocusChanged.InvokeAsync(false))"`

**Note:** `RateIconComponent`s needs to know the shared focus state as they either:

- mark their icons as active if any other icon with greater `IconRate` is focused (despite the actual rate selected),
- mark their icon as active if their own `IconRate` are smaller or equal than the selected rate (if non of the rate icons are focused).

Use the component to replace the `<i>` tag used inside the `RateComponent`:

```
@inherits RateComponentBaseWithTask

@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <div class="icon-rate-container">
        @foreach (int i in Enumerable.Range(Product.MinRate, Product.MaxRate - Product.MinRate + 1))
        {
            <RateIconComponent
                Icon="@Icon"
                IconRate="@i" 
                Value="@Rate"
                FocusedValue="@tempRate"
                ValueChanged="@(async (selectedRate) => { Rate = selectedRate; await SetRate(); })"
                IsFocused="@focus"
                OnFocusChanged="@((isFocused) => { focus = isFocused; tempRate = i; })" />
        }
    </div>
</div>

<div class="current-rate-container">
    @CurrentRate(CurrentRateContext)
</div>

<div class="average-rate-container">
    @AvgRate(AverageRateContext)
</div>

<div class="max-rate-container">
    @MaxRate(Product.MaxRate)
</div>

@code {
    [Parameter]
    public IRateableProduct Product { get; set; } = default!;

    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public RenderFragment<AverageRateContext> CurrentRate { get; set; } = context => __builder =>
    {
    @if (@context.AvgRate > 0)
        {
            <DefaultAverageRateComponent
                MinRate="@context.MinRate"
                AvgRate="@context.AvgRate"
                MaxRate="@context.MaxRate" />
        }
        else
        {
            @: ---
        }
    };

    [Parameter]
    public RenderFragment<AverageRateContext> AvgRate { get; set; } = context => __builder =>
    {
        <DefaultAverageRateComponent 
            MinRate="@context.MinRate"
            AvgRate="@context.AvgRate"
            MaxRate="@context.MaxRate" />
    };

    [Parameter]
    public RenderFragment<int> MaxRate { get; set; } = context => __builder =>
    {
        @context
    };

    [Parameter]
    public EventCallback<int> ProductRated { get; set; } = default!;

    private int CurrentRateInt => Product.CurrentRate ?? 0;

    private bool focus;
    private int tempRate = 0;

    private AverageRateContext CurrentRateContext => new AverageRateContext()
    {
        MinRate = Product.MinRate,
        AvgRate = (decimal)CurrentRateInt,
        MaxRate = Product.MaxRate,
    };

    private AverageRateContext AverageRateContext => new AverageRateContext()
    {
        MinRate = Product.MinRate,
        AvgRate = Product.AverageRate,
        MaxRate = Product.MaxRate,
    };

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        if (parameters.GetValueOrDefault<RenderFragment<AverageRateContext>>(nameof(CurrentRate)) is null)
        {
            Logger.LogInformation(Properties.Resources.LOG_INFO_DEFAULT_RENDER_FRAGMENT, nameof(CurrentRate));
        }

        if (parameters.GetValueOrDefault<RenderFragment<AverageRateContext>>(nameof(AvgRate)) is null)
        {
            Logger.LogInformation(Properties.Resources.LOG_INFO_DEFAULT_RENDER_FRAGMENT, nameof(AvgRate));
        }

        if (parameters.GetValueOrDefault<RenderFragment<int>>(nameof(MaxRate)) is null)
        {
            Logger.LogInformation(Properties.Resources.LOG_INFO_DEFAULT_RENDER_FRAGMENT, nameof(MaxRate));
        }
    }

    protected override void OnParametersSet()
    {
        Rate = CurrentRateInt;
    }

    protected override async Task SetRate()
    {
        await base.SetRate();
        await ProductRated.InvokeAsync(Rate);
    }
}
```

Each of the `RateIconComponent`s receive an `Icon` and `IconRate` (based on loop index).

`Value` parameter and `ValueChanged` event callback is used to react on rate icon click. If clicked, `ValueChanged` is called, providing the selected rate, `Rate` (base class property) is updated and the method to notify the `ContainerComponent` about the change is called (`ProductRated`).

On any mouse in/out event each of the generated `RateIconComponent`s call `OnFocusChanged`. `OnFocusChanged` updates both shared `focus` (line `76`) and `tempRate` (line `77`) fields to provide their updated values for `IsFocused` and `FocusedValue` parameters of each of the `RateIconComponent`s.

### Replace the base class logic with context class
    
Remove the `RateComponentBaseWithTask` inheritance from the `RateComponent`.

- Remove the reference to the `RateComponentBaseWithTask.Rate` property. Use the `CurrentRateInt` of the `RateComponent` instead.
- Add the setter for the `CurrentRateInt` as the `ValueChanged` even callback is setting the value of the replaced property (`ValueChanged="@(async (selectedRate) => { CurrentRateInt = selectedRate; await SetRate(); })"`) and currently it is read-only (`private int CurrentRateInt => Product.CurrentRate ?? 0;`). The logic from the `SetRate` method can be used:

```
private int CurrentRateInt
{
	get => Product.CurrentRate ?? 0;
	set => ProductRated.InvokeAsync(value);
}
```

- Remove remaining methods that uses the removed `base` class:
  - `OnParametersSet`,
  - `SetRate`.
- Remove the `SetRate` usage from the `ValueChanged` lambda expression.

```
@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <div class="icon-rate-container">
        @foreach (int i in Enumerable.Range(Product.MinRate, Product.MaxRate - Product.MinRate + 1))
        {
            <RateIconComponent
                Icon="@Icon"
                IconRate="@i" 
                Value="@CurrentRateInt"
                FocusedValue="@tempRate"
                ValueChanged="@((selectedRate) => CurrentRateInt = selectedRate)"
                IsFocused="@focus"
                OnFocusChanged="@((isFocused) => { focus = isFocused; tempRate = i; })" />
        }
    </div>
</div>

<div class="current-rate-container">
    @CurrentRate(CurrentRateContext)
</div>

<div class="average-rate-container">
    @AvgRate(AverageRateContext)
</div>

<div class="max-rate-container">
    @MaxRate(Product.MaxRate)
</div>

@code {
    [Parameter]
    public IRateableProduct Product { get; set; } = default!;

    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public RenderFragment<AverageRateContext> CurrentRate { get; set; } = context => __builder =>
    {
    @if (@context.AvgRate > 0)
        {
            <DefaultAverageRateComponent
                MinRate="@context.MinRate"
                AvgRate="@context.AvgRate"
                MaxRate="@context.MaxRate" />
        }
        else
        {
            @: ---
        }
    };

    [Parameter]
    public RenderFragment<AverageRateContext> AvgRate { get; set; } = context => __builder =>
    {
        <DefaultAverageRateComponent 
            MinRate="@context.MinRate"
            AvgRate="@context.AvgRate"
            MaxRate="@context.MaxRate" />
    };

    [Parameter]
    public RenderFragment<int> MaxRate { get; set; } = context => __builder =>
    {
        @context
    };

    [Parameter]
    public EventCallback<int> ProductRated { get; set; } = default!;

    private int CurrentRateInt
    {
        get => Product.CurrentRate ?? 0;
        set => ProductRated.InvokeAsync(value);
    }

    private bool focus;
    private int tempRate = 0;

    private AverageRateContext CurrentRateContext => new AverageRateContext()
    {
        MinRate = Product.MinRate,
        AvgRate = (decimal)CurrentRateInt,
        MaxRate = Product.MaxRate,
    };

    private AverageRateContext AverageRateContext => new AverageRateContext()
    {
        MinRate = Product.MinRate,
        AvgRate = Product.AverageRate,
        MaxRate = Product.MaxRate,
    };

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        if (parameters.GetValueOrDefault<RenderFragment<AverageRateContext>>(nameof(CurrentRate)) is null)
        {
            Logger.LogInformation(Properties.Resources.LOG_INFO_DEFAULT_RENDER_FRAGMENT, nameof(CurrentRate));
        }

        if (parameters.GetValueOrDefault<RenderFragment<AverageRateContext>>(nameof(AvgRate)) is null)
        {
            Logger.LogInformation(Properties.Resources.LOG_INFO_DEFAULT_RENDER_FRAGMENT, nameof(AvgRate));
        }

        if (parameters.GetValueOrDefault<RenderFragment<int>>(nameof(MaxRate)) is null)
        {
            Logger.LogInformation(Properties.Resources.LOG_INFO_DEFAULT_RENDER_FRAGMENT, nameof(MaxRate));
        }
    }
}
```

**Note:** lambda expression for `ValueChanged` event is no longer asynchronous.

**Note:** with the `CurrentRateInt` setter calling `EventCVallback<int>` directly and passing the value to be set (`ProductRated.InvokeAsync(value)`), the `RateComponent` is no longer used to set the rate of the product (as `OnParametersSet` did but is was removed) - it is just passing the value to its parent component to update the product with the **API** call - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0#bind-across-more-than-two-components).

Replace the `focus` and `tempRate` fields with the `RateContext` class:

```
namespace Capgemini.Net.Blazor.Components.Demo11.Start
{
    public class RateContext
    {
        public bool IsFocused { get; set; }

        public int FocusedRateValue { get; set; }
    }
}
```

```
@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <div class="icon-rate-container">
        @foreach (int i in Enumerable.Range(Product.MinRate, Product.MaxRate - Product.MinRate + 1))
        {
            <RateIconComponent
                Icon="@Icon"
                IconRate="@i" 
                Value="@CurrentRateInt"
                FocusedValue="@(RateContext.FocusedRateValue)"
                ValueChanged="@((selectedRate) => CurrentRateInt = selectedRate)"
                IsFocused="@(RateContext.IsFocused)"
                OnFocusChanged="@((isFocused) => { RateContext.IsFocused = isFocused; RateContext.FocusedRateValue = i; })" />
        }
    </div>
</div>

<div class="current-rate-container">
    @CurrentRate(CurrentRateContext)
</div>

<div class="average-rate-container">
    @AvgRate(AverageRateContext)
</div>

<div class="max-rate-container">
    @MaxRate(Product.MaxRate)
</div>

@code {
	// ...

    private int CurrentRateInt
    {
        get => Product.CurrentRate ?? 0;
        set => ProductRated.InvokeAsync(value);
    }

    private RateContext RateContext = new RateContext();

	// ...
}
```

### Simplify the logic for RateIconComponent handled by its parameters and event callbacks

As the `RateIconComponent` is using its `Value` parameter and `ValueChanged` event callback both to get/set the same `CurrentRateInt` parameter, it can be simplified - a [data binding](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0) Blazor mechanism can be used.

Replace the `RateIconComponent` markup in the `RateComponent`:

```
@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <div class="icon-rate-container">
        @foreach (int i in Enumerable.Range(Product.MinRate, Product.MaxRate - Product.MinRate + 1))
        {
            <RateIconComponent
                Icon="@Icon"
                IconRate="@i" 
                @bind-Value="@CurrentRateInt"
                FocusedValue="@(RateContext.FocusedRateValue)"
                IsFocused="@(RateContext.IsFocused)"
                OnFocusChanged="@((isFocused) => { RateContext.IsFocused = isFocused; RateContext.FocusedRateValue = i; })" />
        }
    </div>
</div>

<!-- ... -->
```

Data binding allows the binded value to be updated to the value that the component pushes by the `ValueChanged` parameter of the `EventCallback` type. To be able to use a custom data binding syntax for a component, it has to:

- define the parameter of a `TValue` type to enable the component to receive a value from the field or property (e.g. `Value="@CurrentRateInt"`):

```
<!-- ... -->

[Parameter]
public int Value { get; set; }

<!-- ... -->
```

- define the parameter of the `EventCallback<TValue>` type, named by the input parameter with a `Changed` suffix:

```
<!-- ... -->

    [Parameter]
    public EventCallback<int> Value`Changed` { get; set; }

<!-- ... -->
```

**Note:** generic type for the `ValueChanged` parameter has to be the same as the `Value` parameter is - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0#binding-with-component-parameters).

To bind across more than two components, components is chain can invoke another `EventCallback` with another binded property setter ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0#bind-across-more-than-two-components)):

```
<!-- ... -->

    <RateIconComponent
        Icon="@Icon"
        IconRate="@i" 
        @bind-Value="@CurrentRateInt"
        FocusedValue="@(RateContext.FocusedRateValue)"
        IsFocused="@(RateContext.IsFocused)"
        OnFocusChanged="@((isFocused) => { RateContext.IsFocused = isFocused; RateContext.FocusedRateValue = i; })" />

<!-- ... -->
```

```
// ...

private int CurrentRateInt
{
	get => Product.CurrentRate ?? 0;
	set => ProductRated.InvokeAsync(value);
}

// ...
```

**Note:** binded value can be displayed with the custom format by using the [format](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0#format-strings) directive attribute.

**Note:** `CascadingValue` with a mutable object can be used to allow the component to work with the cascading object in both ways as well.

In order to introduce data bindings for

- `FocusedRateValue` property of the `RateContext` object (`FocusedValue="@(RateContext.FocusedRateValue)"`),
- `IsFocused` property of the `RateContext` object (`IsFocused="@(RateContext.IsFocused)"`),

`OnFocusChanged` event callback has to be split as it is used to update both properties in a single call (`OnFocusChanged="@((isFocused) => { RateContext.IsFocused = isFocused; RateContext.FocusedRateValue = i; })"`). In fact, the `RateIconComponent` is invoking that event callback for two different reasons and with different values to be returned:

- `true` if the `@onmouseover` event for a rate icon is triggered to notify the parent component to change the shared `RateContext.IsFocused` to `true` (`@onmouseover="@(() => OnFocusChanged.InvokeAsync(true))"`),
- `false` if the `@onmouseout` event is triggered (`@onmouseout="@(() => OnFocusChanged.InvokeAsync(false))"`) which will set the `RateContext.IsFocused` to `false`.

Update the `RateIconComponent` markup to invoke different event callbacks on those events and the way the `RateComponent` is using them:

```
<i class="@(IsActive ? ActiveCss : InActiveCss) @(Icon) cursor-pointer"
    alt="@IconRate"
    @onclick="@(() => ValueChanged.InvokeAsync(IconRate))"
    @onmouseover="@(() => OnMouseOver())"
    @onmouseout="@(() => OnMouseOut())"></i>

@code {
    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public int IconRate { get; set; }

    [Parameter]
    public int Value { get; set; }

    [Parameter]
    public int FocusedValue { get; set; }

    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    [Parameter]
    public bool IsFocused { get; set; }

    [Parameter]
    public EventCallback<int> OnRateIconFocused { get; set; }

    [Parameter]
    public EventCallback<bool> OnRateIconFocusChanged { get; set; }

    [Parameter]
    public string ActiveCss { get; set; } = "fas";

    [Parameter]
    public string InActiveCss { get; set; } = "far";

    private bool IsActive => (IsFocused && IconRate <= FocusedValue)
        || (!IsFocused && IconRate <= Value);

    private async Task OnMouseOver()
    {
        await OnRateIconFocused.InvokeAsync(IconRate);
        await OnRateIconFocusChanged.InvokeAsync(true);
    }

    private async Task OnMouseOut()
    {
        await OnRateIconFocusChanged.InvokeAsync(false);
    }
}
```

```
<!-- ... -->

<RateIconComponent
	Icon="@Icon"
	IconRate="@i" 
	@bind-Value="@CurrentRateInt"
	FocusedValue="@(RateContext.FocusedRateValue)"
	IsFocused="@(RateContext.IsFocused)"
	OnRateIconFocused="@((rateOnFocus) => RateContext.FocusedRateValue = rateOnFocus)"
	OnRateIconFocusChanged="@((isFocused) => RateContext.IsFocused = isFocused)"/>

<!-- ... -->
```

Now both pairs of parameters can be simplified with the data binding:


- `FocusedValue` parameter which is receiving a value from the `@(RateContext.FocusedRateValue)` and `OnRateIconFocused` event callback which is setting it,
- `IsFocused` parameter which is receiving a value from the `@(RateContext.IsFocused)` and `OnRateIconFocusChanged` event callback which is setting it.

Replace the content of the `RateComponent.razor` file as follows:

```
@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <div class="icon-rate-container">
        @foreach (int i in Enumerable.Range(Product.MinRate, Product.MaxRate - Product.MinRate + 1))
        {
            <RateIconComponent
                Icon="@Icon"
                IconRate="@i" 
                @bind-Value="@CurrentRateInt"
                @bind-IsFocused="@(RateContext.IsFocused)"
                @bind-IsFocused:event="OnRateIconFocusChanged"
                @bind-FocusedValue="@(RateContext.FocusedRateValue)"
                @bind-FocusedValue:event="OnRateIconFocused"/>
        }
    </div>
</div>

<div class="current-rate-container">
    @CurrentRate(CurrentRateContext)
</div>

<div class="average-rate-container">
    @AvgRate(AverageRateContext)
</div>

<div class="max-rate-container">
    @MaxRate(Product.MaxRate)
</div>

@code {
    [Parameter]
    public IRateableProduct Product { get; set; } = default!;

    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public RenderFragment<AverageRateContext> CurrentRate { get; set; } = context => __builder =>
    {
    @if (@context.AvgRate > 0)
        {
            <DefaultAverageRateComponent
                MinRate="@context.MinRate"
                AvgRate="@context.AvgRate"
                MaxRate="@context.MaxRate" />
        }
        else
        {
            @: ---
        }
    };

    [Parameter]
    public RenderFragment<AverageRateContext> AvgRate { get; set; } = context => __builder =>
    {
        <DefaultAverageRateComponent 
            MinRate="@context.MinRate"
            AvgRate="@context.AvgRate"
            MaxRate="@context.MaxRate" />
    };

    [Parameter]
    public RenderFragment<int> MaxRate { get; set; } = context => __builder =>
    {
        @context
    };

    [Parameter]
    public EventCallback<int> ProductRated { get; set; } = default!;

    private int CurrentRateInt
    {
        get => Product.CurrentRate ?? 0;
        set => ProductRated.InvokeAsync(value);
    }

    private RateContext RateContext = new RateContext();

    private AverageRateContext CurrentRateContext => new AverageRateContext()
    {
        MinRate = Product.MinRate,
        AvgRate = (decimal)CurrentRateInt,
        MaxRate = Product.MaxRate,
    };

    private AverageRateContext AverageRateContext => new AverageRateContext()
    {
        MinRate = Product.MinRate,
        AvgRate = Product.AverageRate,
        MaxRate = Product.MaxRate,
    };

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        if (parameters.GetValueOrDefault<RenderFragment<AverageRateContext>>(nameof(CurrentRate)) is null)
        {
            Logger.LogInformation(Properties.Resources.LOG_INFO_DEFAULT_RENDER_FRAGMENT, nameof(CurrentRate));
        }

        if (parameters.GetValueOrDefault<RenderFragment<AverageRateContext>>(nameof(AvgRate)) is null)
        {
            Logger.LogInformation(Properties.Resources.LOG_INFO_DEFAULT_RENDER_FRAGMENT, nameof(AvgRate));
        }

        if (parameters.GetValueOrDefault<RenderFragment<int>>(nameof(MaxRate)) is null)
        {
            Logger.LogInformation(Properties.Resources.LOG_INFO_DEFAULT_RENDER_FRAGMENT, nameof(MaxRate));
        }
    }
}
```

**Note:** by convention, to use the custom data binding syntax in a form of `@bind-{PROPERTY OR FIELD}`, the parameter of type `EventCallback<TValue>` has to have the name of `{PROPERTY OR FIELD}Changed`. In order to use a differently named event callback, an [event](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0) directive attribute can be used with a name of the event callback to be used as a value.

### Reduce the complexity of the callbacks, wrap all rate icons with a tag to handle on focus events

Managing the state of the `RateContext` object never was a responsibility of the `RateIconComponent`. Introduction of the `RateIconComponent` makes it possible to take the advantage of the fact, that each component is able to hold its own state (like the rate each of the given icons represent, without a need to share that data within a base class of the `ContainerComponent`) but also force each of those components to be involved in such tasks as:

- notifying the parent component that the user hovered/blurred the icon, to make the parent component to notify all other icons about it (in order to make them change the way they are calculating their inner state 
- should the icons they are displaying be active or not),
- notifying the parent component that the user clicked on an icon, to make the parent component do the same with the `RateContext.FocusedRateValue` as with `RateContext.IsFocused` property.

Both properties should be related to the `RateComponent` and be only shared with all the `RateIconComponent`s. `CascadingValue` component can be used to provide that kind of behavior.

Change the markup of the `RateComponent.razor` file to remove data bindings, wrap all the icons with a custom `@onmouseover` and `@onmouseout` events and provide a `RateContext` object for the icons:

```
@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <div class="icon-rate-container" `@onmouseover="@(() => RateContext.IsFocused = true)" @onmouseout="@(() => RateContext.IsFocused = false)"`>
        <CascadingValue Value="@RateContext" IsFixed="true">
            @foreach (int i in Enumerable.Range(Product.MinRate, Product.MaxRate - Product.MinRate + 1))
            {
                <RateIconComponent Icon="@Icon" IconRate="@i" @bind-Value="@CurrentRateInt"/>
            }
        </CascadingValue>
    </div>
</div>

<!-- ... -->
```

That way, most of the settings for the `RateIconComponent` can be simplified and much less interaction is needed:

```
<i class="@(IsActive ? ActiveCss : InActiveCss) @(Icon) cursor-pointer"
    alt="@IconRate"
    @onclick="@(() => ValueChanged.InvokeAsync(IconRate))"
    @onmouseover="@(() => OnMouseOver())"
    @onmouseout="@(() => OnMouseOut())"></i>

@code {
    [CascadingParameter]
    public RateContext Context { get; set; } = default!;

    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public int IconRate { get; set; }

    [Parameter]
    public int Value { get; set; }

    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    [Parameter]
    public string ActiveCss { get; set; } = "fas";

    [Parameter]
    public string InActiveCss { get; set; } = "far";

    private bool IsActive => (Context.IsFocused && IconRate <= Context.FocusedRateValue)
        || (!Context.IsFocused && IconRate <= Value);

    private void OnMouseOver()
    {
        Context.IsFocused = true;
        Context.FocusedRateValue = IconRate;
    }

    private void OnMouseOut() => Context.IsFocused = false;
}
```

**Note:** `IsFixed="true"` is used for the `CascadingValue` that is providing the `RateContext` object, which renders the `CascadingValue` performance the same as the ordinary `Parameter` ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#ensure-cascading-parameters-are-fixed)). `IsFixed="true"` can be used for both primitive and complex types but fixed primitive types cannot be changed (as they are passed as values, not reference).