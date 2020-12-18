### Log information about default render fragment usage

Update the `_Imports.razor` file and add a additional import for the `ILogger<TValue>` type:

```
@namespace Capgemini.Net.Blazor.Components.Demo10

@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Rendering
@using Microsoft.AspNetCore.Components.Web.Extensions.Head
@using Microsoft.Extensions.Logging

@using Capgemini.Net.Blazor.Components.Tile
@using Capgemini.Net.Blazor.Components.Tile.Base
@using Capgemini.Net.Blazor.Components.Demo

@using Capgemini.Net.Blazor.Components.Demo.Interfaces
@using Capgemini.Net.Blazor.Components.Demo.Models

@using Capgemini.Net.Blazor.Components.LoadPlaceholder
```

Use the `SetParametersAsync` lifecycle method to determinate the set of parameters that was provided to the `RateComponent` component ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#before-parameters-are-set)):

```
@inherits RateComponentBase

@inject ILogger<RateComponent> Logger

<!-- ... -->

@code {
	// ...

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

Parameter of type `ParameterView` provides the component with the information about set of parameters that were passed to it by the parent's markup (using the properties of the component marked with `[Parameter]` or `[CascadingParameter]`). `SetParametersAsync` method is the first of the lifecycle methods to be executed, before values for the properties are set.

### Set the current rate on rate click to update the context object

Clicking on the rate icon doesn't update `IRateableProduct Product` context object (`SetRate` method bounded to the `@onclick` directive only updates the internal state of the `RateComponentBase` class which `RateComponent` inherits). To change the current `Product.CurrentRate` property on rate icon click, override the `SetRate` method:

```
@inherits RateComponentBase

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

    protected override void SetRate()
    {
        base.SetRate();
        Product.CurrentRate = Rate;
    }
}
```

To be able to override the `SetRate` method, mark it as `virtual` in the base class:

```
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public class RateComponentBase : ComponentBase
    {
        public static readonly string ACTIVE_STYLE = "fas";

        public static readonly string INACTIVE_STYLE = "far";

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

        protected `virtual` void SetRate() => rate = tempRate;

        protected void ShowRate(int index) => tempRate = index;

        protected void RevertRate() => tempRate = rate;

        protected bool IsActive(int index) => index <= tempRate;
    }
}
```

### Notify API about the change, fetch new set of data and refresh component

Overriding the `SetRate` with:

```
protected override void SetRate()
{
    base.SetRate();
    Product.CurrentRate = Rate;
}
```

enables the component to update its markup:

- `SetRate` method is called on the `click` **DOM** event (`@onclick="SetRate"`),
- `base.SetRate()` updates the inner state of the component to update the `Rate` property,
- `Product.CurrentRate` is updated with the current `Rate` (updated in `base.SetRate()`),
- as a **DOM** event was called, component will be rerendered,
- in the process, the `@CurrentRate` render fragment will be invoked with `CurrentRateContext` property (`@CurrentRate(CurrentRateContext)`),
- in order to invoke the render fragment, `CurrentRateContext` will be recalculated on get and will include the change of the `Product.CurrentRate`,
- component will display a new markup for `@CurrentRate` render fragment with updated value.

As soon as the component will receive a different set of parameters (i.e. by routing to a product with different ID), selected rate would be lost. To make the selection be stored in the persistent storage, **API** has to be called upon rate change.

Provide an asynchronous method for the `ContainerComponent` that will be used to call the **API** to send and store new values on the product rate change:

```
using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo10.Start
{
    public partial class ContainerComponent
    {
		// ...

        protected override async Task OnParametersSetAsync()
        {
			// ...
        }

        public async Task ChangeProductRate(int productRate)
        {
            // API logic to be added
        }
    }
}
```

Add a [component event](https://blazor-university.com/components/component-events) to `RateComponent`:

```
@inherits RateComponentBase

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

    [Parameter]
    public Action<int> ProductRated { get; set; } = default!;

	// ...

    protected override void SetRate()
    {
        base.SetRate();
        ProductRated.Invoke(Rate);
    }
}
```

An ordinary parameter for the component was added with a type of an `Action<int>`. `ProductRated` accepts a delegate that receives one parameter of type `int` and return no value. Delegate that is provided by the parent as a value of that parameter is then invoked inside the `SetRate` method with the current rate of the selected product, instead of assigning it directly to the object's property (`Product.CurrentRate = Rate;`). That will make the parent component to be notified each time user invokes the `SetRate` by clicking any of the icons (`@onclick="SetRate"`), providing the parent with the value that was selected.

**Note:** the type of the `ProductRated` can be any of the [delegate](https://docs.microsoft.com/en-us/dotnet/api/system.delegate?view=net-5.0) types i.e. `Action` or `Func` with any number of generic types.

Provide a value for the `ProductRated` parameter. Edit the `WrapperComponent` Razor component with the following markup:

```
<ContainerComponent `@ref="ContainerComponentRef"`>
    <ChildContent Context="product">
        <RateComponent Product="@product" `ProductRated="@(async (rate) => await ChangeProductRate(rate))"`>
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
}
```

`ProductRated` defines the async method that will in turn call the `ChangeProductRate` method, defined in the `ContainerComponent`. `ChangeProductRate` will trigger the **API** to save the given `productRate`.

**Note:** as `RateComponent` is provided to the `ContainerComponent` as `RenderFragment<IRateableProduct> ChildContent`, it is impossible for the `ContainerComponent` to determinate that its render fragment triggers the delegate - the component that provides the delegate needs to pass the call, referring the `ContainerComponent`'s method. Any component can be capture by the reference, assigning the name of the field/property as a value of the `@ref` [build-in](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#ref) Blazor directive (line `1`) - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#capture-references-to-components).

**Note:** `@ref` directive can be used for any tag, including standard **HTML** tags. Value of the directive needs to refer the field/property of the type:
- same as Blazor component which `@ref` refers to (i.e. `<ContainerComponent>` -> `private ContainerComponent? ContainerComponentRef`),
- `ElementReference` to any other tag (which can be used to provide `HTMLElement` as an argument for the <strng>JavaScript</strng> call - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-javascript-from-dotnet?view=aspnetcore-5.0#capture-references-to-elements)).

**Note:** element that is obtained by the `@ref` directive is available as soon as the `OnAfterRender` is called ([see example](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#detect-when-the-app-is-prerendering)).

Define the body of the `ChangeProductRate` method that the `WrapperComponent` is calling by the reference to the `ContainerComponent` (`ContainerComponentRef.ChangeProductRate`). Update the `ContainerComponent.razor.cs` file:

```
using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo10.Start
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public ILogger<ContainerComponent> Logger { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        [CascadingParameter(Name = "Id")]
        public int ProductId { get; set; }

        public string Endpoint => $"products/{ProductId}";

        public string RateEndpoint => $"{Endpoint}/rate";

        private IRateableProduct? product;

        private string loadingMessage = "Loading product";

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var rateableProductViewModel = await HttpClient
                    .GetFromJsonAsync<RateableProductViewModel>(Endpoint);

                if (rateableProductViewModel is not null)
                {
                    product = new RateableProductAdapter(rateableProductViewModel);
                }
            }
            catch (HttpRequestException e1)
            {
                Logger.LogError(
                    e1,
                    Properties.Resources.LOG_ERROR_CANNOT_FETCH,
                    Endpoint);
            }
            catch (NotSupportedException e2)
            {
                Logger.LogError(
                    e2,
                    Properties.Resources.LOG_ERROR_https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage?view=net-5.0_INVALID_FORMAT,
                    Endpoint);
            }
            finally
            {
                if (product is null)
                {
                    loadingMessage = "Loading error";
                }
            }
        }

        public async Task ChangeProductRate(int productRate)
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(RateEndpoint, productRate);

            if (response.IsSuccessStatusCode)
            {
                var rateableProductViewModel = await response.Content.ReadFromJsonAsync<RateableProductViewModel>();

                if (rateableProductViewModel is not null)
                {
                    product = new RateableProductAdapter(rateableProductViewModel);
                    StateHasChanged();
                }
            }
        }
    }
}
```

The following chain of events will be triggered on user's click on one of the rate icons in the `RateComponent`:

- `SetRate` method is called on the `click` **DOM** event (`@onclick="SetRate"`),
- `base.SetRate()` updates the inner state of the component to update the `Rate` property,
- `ProductRated` delegate of type `Action<int>` is invoked (`ProductRated.Invoke(Rate)`) and the delegate defined in the `WrapperComponent` is triggered (`ProductRated="@(async (rate) => await ChangeProductRate(rate))"`).
- Asynchronous `ChangeProductRate` method will use captured reference to the `ContainerComponent` to call its `ChangeProductRate` method (lines `70-84`).
- If the rate of the product changes, injected `HttpClient` will use the [helper](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-5.0) method in order to pass the new rate to the **API** endpoint (defined in the line `29`). In [response](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage?view=net-5.0), **API** will send back the JSON-formatted body with updated `RateableProductViewModel` that can be converted to the type of the `product` field.
- `StateHasChanged()` will notify the framework to rerender the component and its child content ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#state-changes)).

**Note:** by replacing the delegate type that was used for the `ProductRated` parameter to the Blazor-specific `EventCallback`/`EventCallback<TValue>`, the `StateHasChanged()` call can be omitted - `StateHasChanged` would called automatically during the process for those type of events - [read more](https://blazor-university.com/components/component-events).

Update the type of the `ProductRated` parameter of the `RateComponent`:

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
    public `EventCallback<int>` ProductRated { get; set; } = default!;

    private int CurrentRateInt => Product.CurrentRate ?? 0;

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

    protected override async Task SetRate()
    {
        await base.SetRate();
        await ProductRated.InvokeAsync(Rate);
    }
}
```

**Note:** Blazor `EventCallback<TValue>` is asynchronous which force the `SetRate` to also be asynchronous so the base class for the component was changed (line `1`). `RateComponentBaseWithTask` (from the `Capgemini.Net.Blazor.Components.Demo` namespace) provides the method with valid signature.

Remove the `StateHasChanged` from the `ContainerComponent` markup as it is no longer needed:

```
using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo10.Start
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public ILogger<ContainerComponent> Logger { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        [CascadingParameter(Name = "Id")]
        public int ProductId { get; set; }

        public string Endpoint => $"products/{ProductId}";

        public string RateEndpoint => $"{Endpoint}/rate";

        private IRateableProduct? product;

        private string loadingMessage = "Loading product";

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var rateableProductViewModel = await HttpClient
                    .GetFromJsonAsync<RateableProductViewModel>(Endpoint);

                if (rateableProductViewModel is not null)
                {
                    product = new RateableProductAdapter(rateableProductViewModel);
                }
            }
            catch (HttpRequestException e1)
            {
                Logger.LogError(
                    e1,
                    Properties.Resources.LOG_ERROR_CANNOT_FETCH,
                    Endpoint);
            }
            catch (NotSupportedException e2)
            {
                Logger.LogError(
                    e2,
                    Properties.Resources.LOG_ERROR_https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage?view=net-5.0_INVALID_FORMAT,
                    Endpoint);
            }
            finally
            {
                if (product is null)
                {
                    loadingMessage = "Loading error";
                }
            }
        }

        public async Task ChangeProductRate(int productRate)
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(RateEndpoint, productRate);

            if (response.IsSuccessStatusCode)
            {
                var rateableProductViewModel = await response.Content.ReadFromJsonAsync<RateableProductViewModel>();

                if (rateableProductViewModel is not null)
                {
                    product = new RateableProductAdapter(rateableProductViewModel);
                }
            }
        }
    }
}

```

### Provide initial rate value for the rate component, redirect to random product on initialization

Replace the content of the `ContainerComponent.razor.cs` file with a given markup:

```
using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo10.Start
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public ILogger<ContainerComponent> Logger { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        [CascadingParameter(Name = "Id")]
        public int ProductId { get; set; }

        public string Endpoint => $"products/{ProductId}";

        public string RateEndpoint => $"{Endpoint}/rate";

        private IRateableProduct? product;

        private string loadingMessage = "Loading product";

        protected override async Task OnInitializedAsync()
        {
            if (!HasProductIdParameterInRoute())
            {
                var numberOfProducts = await GetFromJsonAsync<int?>("products/count");

                if (numberOfProducts.HasValue)
                {
                    NavigationManager.NavigateTo(
                        GetRouteWithRandomProductId(numberOfProducts.Value));
                }
            }

            bool HasProductIdParameterInRoute() => Regex.IsMatch(NavigationManager.Uri, @"/\d+$");

            string GetRouteWithRandomProductId(int numberOfProducts) 
                => $"{NavigationManager.Uri}/{new Random().Next(1, numberOfProducts)}";
        }

        protected override async Task OnParametersSetAsync()
        {
            var rateableProductViewModel = await GetFromJsonAsync<RateableProductViewModel>(Endpoint);

            if (rateableProductViewModel is not null)
            {
                product = new RateableProductAdapter(rateableProductViewModel);
            }

            if (product is null)
            {
                loadingMessage = "Loading error";
            }
        }

        public async Task ChangeProductRate(int productRate)
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(RateEndpoint, productRate);

            if (response.IsSuccessStatusCode)
            {
                var rateableProductViewModel = await response.Content.ReadFromJsonAsync<RateableProductViewModel>();

                if (rateableProductViewModel is not null)
                {
                    product = new RateableProductAdapter(rateableProductViewModel);
                }
            }
        }

        private async Task<TValue?> GetFromJsonAsync<TValue>(string endpoint)
        {
            try
            {
                TValue? result = await HttpClient
                    .GetFromJsonAsync<TValue>(endpoint);
                return result;
            }
            catch (HttpRequestException e1)
            {
                Logger.LogError(
                    e1,
                    Properties.Resources.LOG_ERROR_CANNOT_FETCH,
                    Endpoint);
            }
            catch (NotSupportedException e2)
            {
                Logger.LogError(
                    e2,
                    Properties.Resources.LOG_ERROR_https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage?view=net-5.0_INVALID_FORMAT,
                    Endpoint);
            }

            return default;
        }
    }
}
```

- JSON helper `HttpClient.GetFromJsonAsync<TValue>` was extracted to the `GetFromJsonAsync<TValue>` method to not have to duplicate error handling related to the **API** calls (lines `88-112`).
- `OnParametersSetAsync` lifecycle method was changed to reflect the replacement of the `HttpClient.GetFromJsonAsync<TValue>` with `GetFromJsonAsync<TValue>`.
- `NavigationManager` was injected to make the component able to redirect from the default route if the `id` of the product is not given (lines `22-23`).
- `OnInitializedAsync` lifecycle method will cause the route to be changed if no `ProductId` was captured from the route (lines `39-56`):
  - `HasProductIdParameterInRoute` [local function](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions) checks if the current route contains a `{id:int?}` route parameter, which is then passed by the `CascadingValue` to the `ContainerComponent`, allowing it to fetch the product of the given `ProductId`.
  - If the `{id:int?}` is not present in the route, call to the **API** `products/count` is made to receive the number of products in the database.
  - If the number of products is returned from the **API**, `NavigationManager` will make a route to change with a random product id added to the route.
  - Navigating to the route with a `{id:int?}` will cause the `ProductId` parameter to change (as the cascading value changed - `[CascadingParameter(Name = "Id")]`).
  - `OnParametersSetAsync` will be called upon the `[CascadingParameter(Name = "Id")]` change.
  - `OnParametersSetAsync` lifecycle method will call the **API** to fetch a product with the new `ProductId` and update the `product`.

Once the `ProductId` is selected randomly on component's initialization, markup for the `ContainerComponent` need to be updated to render as much `<ProductButton ProductId="@productId" />` components as the number of products in the database is. Update the `ContainerComponent.razor.cs` file to store the number of component fetched from the **API** and use it to update the loop range of `ContainerComponent` (lines `19-25` in `ContainerComponent.razor` file):

```
using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo10.Start
{
    public partial class ContainerComponent
    {
		// ...

        private int? numberOfProducts;

        protected override async Task OnInitializedAsync()
        {
            if (!HasProductIdParameterInRoute())
            {
                numberOfProducts = await GetFromJsonAsync<int?>("products/count");

                if (numberOfProducts.HasValue)
                {
                    NavigationManager.NavigateTo(
                        GetRouteWithRandomProductId(numberOfProducts.Value));
                }
            }

            bool HasProductIdParameterInRoute() => Regex.IsMatch(NavigationManager.Uri, @"/\d+$");

            string GetRouteWithRandomProductId(int numberOfProducts) 
                => $"{NavigationManager.Uri}/{new Random().Next(1, numberOfProducts)}";
        }

		// ...
    }
}
```

```
<div class="demo__container_wrapper">
    @if (product is null)
    {
        <LoadPlaceholder>
            <Message>
                <p>@loadingMessage</p>
            </Message>
        </LoadPlaceholder>
    }
    else
    {
        <div class="demo__container">
            @ChildContent(product)
        </div>
    }
</div>

<div class="navigation-bar">
    @if (numberOfProducts.HasValue)
    {
        @foreach (int productId in Enumerable.Range(1, numberOfProducts.Value))
        {
            <ProductButton ProductId="@productId" />
        }
    }
</div>
```

To avoid having the `<ProductButton ProductId="@productId" />` buttons to be rendered only if the `HasProductIdParameterInRoute` method returns `true`, replace the line order of the `OnInitializedAsync` within the `ContainerComponent` code-behind file:

```
using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo10.Start
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public ILogger<ContainerComponent> Logger { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        [CascadingParameter(Name = "Id")]
        public int ProductId { get; set; }

        public string Endpoint => $"products/{ProductId}";

        public string RateEndpoint => $"{Endpoint}/rate";

        private IRateableProduct? product;

        private string loadingMessage = "Loading product";

        private int? numberOfProducts;

        protected override async Task OnInitializedAsync()
        {
            numberOfProducts = await GetFromJsonAsync<int?>("products/count");

            if (numberOfProducts.HasValue && !HasProductIdParameterInRoute())
            {
                NavigationManager.NavigateTo(
                    GetRouteWithRandomProductId(numberOfProducts.Value));
            }

            bool HasProductIdParameterInRoute() => Regex.IsMatch(NavigationManager.Uri, @"/\d+$");

            string GetRouteWithRandomProductId(int numberOfProducts) 
                => $"{NavigationManager.Uri}/{new Random().Next(1, numberOfProducts)}";
        }

        protected override async Task OnParametersSetAsync()
        {
            var rateableProductViewModel = await GetFromJsonAsync<RateableProductViewModel>(Endpoint);

            if (rateableProductViewModel is not null)
            {
                product = new RateableProductAdapter(rateableProductViewModel);
            }

            if (product is null)
            {
                loadingMessage = "Loading error";
            }
        }

        public async Task ChangeProductRate(int productRate)
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(RateEndpoint, productRate);

            if (response.IsSuccessStatusCode)
            {
                var rateableProductViewModel = await response.Content.ReadFromJsonAsync<RateableProductViewModel>();

                if (rateableProductViewModel is not null)
                {
                    product = new RateableProductAdapter(rateableProductViewModel);
                }
            }
        }

        private async Task<TValue?> GetFromJsonAsync<TValue>(string endpoint)
        {
            try
            {
                TValue? result = await HttpClient
                    .GetFromJsonAsync<TValue>(endpoint);
                return result;
            }
            catch (HttpRequestException e1)
            {
                Logger.LogError(
                    e1,
                    Properties.Resources.LOG_ERROR_CANNOT_FETCH,
                    Endpoint);
            }
            catch (NotSupportedException e2)
            {
                Logger.LogError(
                    e2,
                    Properties.Resources.LOG_ERROR_https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage?view=net-5.0_INVALID_FORMAT,
                    Endpoint);
            }

            return default;
        }
    }
}
```

### Provide a proper rate of the product to be displayed on product change

Update the `RateComponent` by adding the `OnParametersSet` lifecycle method to update the `Rate` base class property to display accurate number of icons selected, based on the `CurrentRate` property of a `Product` that was provided by the **API** (form `ContainerComponent`):

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