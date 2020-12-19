![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/12/Summary.jpg)

# Cascading components: generate child content for rate icons as render fragments on runtime

The below **README.md** file contains a transcript of the descriptions that can be found in the checklist for each of the exercise\`s points built into the Blazor WebAssembly application on-line ([link](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/)). You can directly go to that descriptions by navigating to any of those links:
 - [create custom cascading component to provide the context for the rate icons](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo12/checklist/1),
 - [enable to use any type of icons for rate component](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo12/checklist/2),
 - [fix change state detection to render icons on any icon focus/blur](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo12/checklist/3).

### Create custom cascading component to provide the context for the rate icons

`CascadingValue` provides a `ChildContent` parameter of type `RenderFragment`. To wrap the logic related to the `RateContext` class and remove it from the `RateComponent`, create a `CascadingRateContext.razor` file:

```
<div class="icon-rate-container"
    @onmouseover="@(() => RateContext.IsFocused = true)"
    @onmouseout="@(() => RateContext.IsFocused = false)"
>
    <CascadingValue Value="@RateContext" `ChildContent="@ChildContent"` IsFixed="true" />
</div>

@code {
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    private RateContext RateContext = new RateContext();
}
```

Update the markup of the `RateComponent.razor` file:

```
@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <CascadingRateContext>
        @foreach (int i in Enumerable.Range(Product.MinRate, Product.MaxRate - Product.MinRate + 1))
        {
            <RateIconComponent Icon="@Icon" IconRate="@i" @bind-Value="@CurrentRateInt" />
        }
    </CascadingRateContext>
</div>

<!-- ... -->
```

**Note:** if the cascading value requires some additional logic, having `Cascading{CLASS}` components to provide a cascading value for the object of type `{CLASS}` is also used by the build-in Blazor components i.e. [CascadingAuthenticationState](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.authorization.cascadingauthenticationstate?view=aspnetcore-5.0).

Create a `CascadingRateContext.razor.scss` file, to provide a definition for a **CSS** class that is used by the `CascadingRateContext`:

```
.icon-rate-container {
    display: flex;
}
```

### Enable to use any type of icons for rate component

Blazor does not support defining render fragments for a components at runtime. To overcome this limitation a [this cascading value](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#ensure-cascading-parameters-are-fixed) can be used ([see example](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters?view=aspnetcore-5.0#tabset-example)). This allows to pass a parent component that is wrapped by the `<CascadingValue Value="this">` syntax. Any markup/component that wants to use the value, needs to define the `CascadingParameter` of the proper type. Modify the `RateComponent` to replace `RateIconComponent` components with additional `RenderFragment` and wrap it with the `CascadingValue`:

```
@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <CascadingValue Value="this">
        <CascadingRateContext>
            @ChildContent(Product)
        </CascadingRateContext>
    </CascadingValue>
</div>

<!-- ... -->

@code {
    [Parameter]
    public IRateableProduct Product { get; set; } = default!;

    [Parameter]
    public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

	// ...
}
```

Add the following parameter to the `RateIconComponent` which will be used as a part of the `ChildContent` render fragment for `RateComponent`.

```
<i class="@(IsActive ? ActiveCss : InActiveCss) @(Icon) cursor-pointer"
   alt="@IconRate"
   @onclick="@(() => RateComponent.CurrentRateInt = IconRate)"
   @onmouseover="@(() => OnMouseOver())"
   @onmouseout="@(() => OnMouseOut())"></i>

@code {

    [CascadingParameter]
    public RateComponent RateComponent { get; set; } = default!;

    [CascadingParameter]
    public RateContext Context { get; set; } = default!;

    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public int IconRate { get; set; }

    private int Value => RateComponent.CurrentRateInt;

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

Update the visibility of the `CurrentRateInt` property of the `RateComponent` as it is used by lines `3` (as a lambda expression to assign a new rate `@onclick`) and `21`.

```
// ...

`public` int CurrentRateInt
{
	get => Product.CurrentRate ?? 0;
	set => ProductRated.InvokeAsync(value);
}

// ...
```

As the default content for icons of the `RateComponent` was replaced by the `ChildContent` render fragment, provide the same markup as a default value for `ChildContent` (in case `RateComponent` won't be provided by the custom child content, this default markup will be used):

```
@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <CascadingValue Value="this">
        <CascadingRateContext>
            @ChildContent(Product)
        </CascadingRateContext>
    </CascadingValue>
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
    public RenderFragment<IRateableProduct> ChildContent { get; set; } = context => __builder =>
    {
        @foreach (int i in Enumerable.Range(context.MinRate, context.MaxRate - context.MinRate + 1))
        {
            <RateIconComponent IconRate="@i" />
        }
    };

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

    public int CurrentRateInt
    {
        get => Product.CurrentRate ?? 0;
        set => ProductRated.InvokeAsync(value);
    }

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

To provide a different set of icons, add a custom child markup for the `RateComponent` that is being used as a `ChildContent` of the `ContainerComponent` inside the `WrapperComponent`. Wrap it with the `<ChildContent>` tag, providing a `Product` variable as a context:

```
<ContainerComponent @ref="ContainerComponentRef">
    <ChildContent Context="product">
        <RateComponent Product="@product" ProductRated="@(async (rate) => await ChangeProductRate(rate))">
            <ChildContent Context="Product">
                @foreach (int i in Enumerable.Range(Product.MinRate, Product.MaxRate - Product.MinRate + 1))
                {
                    <RateIconComponent
                        IconRate="@i"
                        Icon="@GetRateIcon(@i, Product.MaxRate - Product.MinRate)" />
                }
            </ChildContent>
            <AvgRate Context="avgContext">
                <DefaultAverageRateComponent 
                    MinRate="@(avgContext.MinRate)"
                    AvgRate="@(avgContext.AvgRate)"
                    MaxRate="@(avgContext.MaxRate)" />
            </AvgRate>
        </RateComponent>
    </ChildContent>
</ContainerComponent>

@code {
    private ContainerComponent? ContainerComponentRef;

    public async Task ChangeProductRate(int productRate)
    {
        if (ContainerComponentRef is not null)
        {
            await ContainerComponentRef.ChangeProductRate(productRate);
        }
    }

    private static string GetRateIcon(int productRate, int rateRange) => productRate switch
    {
        int rate when rate <= rateRange * 0.2 * 1 => "fa-angry",
        int rate when rateRange * 0.2 * 1 < rate
            && rate <= rateRange * 0.2 * 2 => "fa-sad-tear",
        int rate when rateRange * 0.2 * 2 < rate 
            && rate <= rateRange * 0.2 * 3 => "fa-meh-blank",
        int rate when rateRange * 0.2 * 3 < rate 
            && rate <= rateRange * 0.2 * 4 => "fa-smile-beam",
        int rate when rateRange * 0.2 * 4 < rate 
            && rate <= rateRange * 0.2 * 5 => "fa-grin-stars",
        _ => "fa-grin-stars",
    };
}
```

The custom method was provided to select the icon based on the product's rate each icon is representing.

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/12/img/1.jpg)

### Fix change state detection to render icons on any icon focus/blur

To make the `RateComponent` to rerender itself upon focus change of any `RateIconComponent`, use the `RateComponent` cascading parameter to call `StateHasChanged` method, which will force a component that calls it to rerender ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#state-changes)):

```
<i class="@(IsActive ? ActiveCss : InActiveCss) @(Icon) cursor-pointer"
   alt="@IconRate"
   @onclick="@(() => RateComponent.CurrentRateInt = IconRate)"
   @onmouseover="@(() => OnMouseOver())"
   @onmouseout="@(() => OnMouseOut())"></i>

@code {
    [CascadingParameter]
    public RateComponent RateComponent { get; set; } = default!;

    [CascadingParameter]
    public RateContext Context { get; set; } = default!;

    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public int IconRate { get; set; }

    private int Value => RateComponent.CurrentRateInt;

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
        RateComponent.RateIconComponentFocusHasChanged();
    }

    private void OnMouseOut()
    {
        Context.IsFocused = false;
        RateComponent.RateIconComponentFocusHasChanged();
    }
}
```

Create the `RateIconComponentFocusHasChanged` method in `RateComponent.razor` file:

```
@inject ILogger<RateComponent> Logger

<div class="rate-container">
    <CascadingValue Value="this">
        <CascadingRateContext>
            @ChildContent(Product)
        </CascadingRateContext>
    </CascadingValue>
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
    public RenderFragment<IRateableProduct> ChildContent { get; set; } = context => __builder =>
    {
        @foreach (int i in Enumerable.Range(context.MinRate, context.MaxRate - context.MinRate + 1))
        {
            <RateIconComponent IconRate="@i" />
        }
    };

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

    public int CurrentRateInt
    {
        get => Product.CurrentRate ?? 0;
        set => ProductRated.InvokeAsync(value);
    }

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

    public void RateIconComponentFocusHasChanged() => StateHasChanged();
}
```

**Note:** replacing the order of the cascading components from:

- `<CascadingValue Value="this">`,
- `<CascadingRateContext>` (a `ChildContent`)

to:

- `<CascadingRateContext>`,
- `<CascadingValue Value="this">`

and removing the body of the `RateIconComponentFocusHasChanged` method will make the component to behave exactly the same, even without calling `StateHasChanged`.

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/12/img/2.jpg)

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/12/img/3.jpg)

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/12/img/4.jpg)

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/12/img/5.jpg)