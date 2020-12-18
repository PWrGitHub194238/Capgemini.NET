### Review changes in initial component

The flowing changes was done to `./Start/*` files in order to:

- prepare the solution to rely on the data not from the form but one that is provided by the **API** with the following definition:
- <strong style="color: #249c47;">GET</strong>: [/products/count](http://localhost:5002/api/products/count) - will return the number of products in the `(localdb)\\MSSQLLocalDB\BlazorDemo` **data base**:
<br />**Response:**

```
9
```
- <strong style="color: #249c47;">GET</strong>: [/products/{productId}](http://localhost:5002/api/products/1) - will return an object of the `RateableProductViewModel` type, representing details about product with the ID of `productId`:
<br />**Response:**

```
{
   "name":"Soccer Ball",
   "description":"FIFA-approved size and weight",
   "price":19.50,
   "category":{
      "name":"Soccer"
   },
   "currentRate":2,
   "averageRate":3.41,
   "minRate":1,
   "maxRate":5
}
```
- <strong style="color: #ffb400;">POST</strong>: [/products/{productId}/rate](http://localhost:5002/api/products/1/rate) - will add a `rate` for the product with the ID of `productId`:
<br />**Body:**

```
{rate}
```

**Response:**

```
{
   "name":"Soccer Ball",
   "description":"FIFA-approved size and weight",
   "price":19.50,
   "category":{
      "name":"Soccer"
   },
   "currentRate":2,
   "averageRate":3.39080459770115,
   "minRate":1,
   "maxRate":5
}
```

- use the common `IRateable`, `IRateRange` and `IRateableProduct` interfaces:

```
namespace Capgemini.Net.Blazor.Components.Demo.Interfaces
{
    public interface IRateable
    {
        int? CurrentRate { get; set; }

        decimal AverageRate { get; set; }
    }
}
```

```
namespace Capgemini.Net.Blazor.Components.Demo.Interfaces
{
    public interface IRateRange
    {
        int MinRate { get; set; }

        int MaxRate { get; set; }
    }
}
```

```
namespace Capgemini.Net.Blazor.Components.Demo.Interfaces
{
    public interface IRateableProduct : IRateable, IRateRange
    {
    }
}
```

Additional changes for the already existing files were made changes was made:

- `ContainerComponent.razor` was severely simplified:
  - `EditForm` with its content was removed - all data would be provided by the **API**, not by the user from the form.
  - `@AvgRate` render fragment was moved back from `ContainerComponent` to `RateComponent`.

```
<div class="demo__container_wrapper">
	@ChildContent(product)
</div>
```

- `ContainerComponent.razor.cs` was simplified to reflect markup changes:
  - `rateContext` of the `RateContext` type was changed to `IRateableProduct`,
  - generic type for the `ChildContent` render fragment was changed to `IRateableProduct`,
  - `@AvgRate` render fragment was removed,
  - `AverageRateContext` context field for `@AvgRate` was removed.

```
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Capgemini.Net.Blazor.Components.Demo.Models;
using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components.Demo09.Start
{
    public partial class ContainerComponent
    {
        private readonly IRateableProduct product = new RateableProduct()
        {
            MinRate = 2,
            AverageRate = 3,
            MaxRate = 6,
            CurrentRate = 3,
        };

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;
    }
}
```

`ContainerComponent.razor.scss` was also simplified, leaving only related **CSS** classes.

- `RateComponent.razor` was changed to:
  - support the `@AvgRate` render fragment with a default `AverageRateContext` property to provide a context for the render fragment,
  - support new context type - `RateContext` class was removed in favor of `IRateableProduct`.
  - Markup of the component was updated to use the new context class,
  - `Icon` parameter was added as the icon is no longer a part of the component's context class - `IRateableProduct`.

```
@inherits RateComponentBase

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

<div class="average-rate-container">
    @AvgRate(AverageRateContext)
</div>

@code {
    [Parameter]
    public IRateableProduct Product { get; set; } = default!;

    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public RenderFragment<AverageRateContext> AvgRate { get; set; } = default!;

    private AverageRateContext AverageRateContext => new AverageRateContext()
    {
        MinRate = Product.MinRate,
        AvgRate = Product.AverageRate,
        MaxRate = Product.MaxRate,
    };
}
```

`RateComponent.razor.cs` code-behind class was removed, `RateComponent.razor.scss` simplified.

**Note:** by replacing a default `@for (int i = 0; i < @RateContext.MaxRate; i += 1)` loop with the `Enumerable` range there is no longer need for storing the variable locally inside the loop to handle lambda expressions usage of the `i` variable ([see example](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#child-content)).

- `WrapperComponent.razor` file's markup was change to render component with its new markup and nested child content for both `ContainerComponent` (`<ChildContent>` and `RateComponent` (`<AvgRate >`) components:

```
<ContainerComponent>
    <ChildContent Context="product">
        <RateComponent Product="@product">
            <AvgRate Context="avgContext">
                <DefaultAverageRateComponent 
                    MinRate="@(avgContext.MinRate)"
                    AvgRate="@(avgContext.AvgRate)"
                    MaxRate="@(avgContext.MaxRate)" />
            </AvgRate>
        </RateComponent>
    </ChildContent>
</ContainerComponent>
```

- `_Imports.razor` file was modified to use additional namespaces:

```
@namespace Capgemini.Net.Blazor.Components.Demo09

@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Rendering
@using Microsoft.AspNetCore.Components.Web.Extensions.Head

@using Capgemini.Net.Blazor.Components.Tile
@using Capgemini.Net.Blazor.Components.Tile.Base
@using Capgemini.Net.Blazor.Components.Demo

@using Capgemini.Net.Blazor.Components.Demo.Interfaces
@using Capgemini.Net.Blazor.Components.Demo.Models
```

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/09/img/1.jpg)

### Call the API to fetch rate data for a product, handle HttpClient's exceptions

Modify the content of the `ContainerComponent.razor.cs` to use the [HttpClient](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-5.0">Dependency Injection</a> to provide the <a href="@https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0) for the component to enable it to use external resources (`HttpClient` is one of many services provided by the Blazor by default - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-5.0#default-services)):

```
using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Capgemini.Net.Blazor.Components.Demo.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo09.Start
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        private IRateableProduct product = new RateableProduct();

        protected override async Task OnInitializedAsync()
        {
            var rateableProductViewModel = await HttpClient
                .GetFromJsonAsync<RateableProductViewModel>("products/1");

            if (rateableProductViewModel is not null)
            {
                product = new RateableProduct()
                {
                    MinRate = rateableProductViewModel.MinRate,
                    AverageRate = rateableProductViewModel.AverageRate,
                    MaxRate = rateableProductViewModel.MaxRate,
                    CurrentRate = rateableProductViewModel.CurrentRate,
                };
            }
        }
    }
}
```

Updated code will call the `products/1` **API** endpoint for the fixed `productId` ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0)). The component will try to resolve the call after the component is initialized ([OnInitializedAsync](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#component-initialization-methods) lifecycle method is used). After data was successfully fetched, the **API** model is used to populate the context object for the `ChildContent` render fragment (lines `28-34`).

To simplify the process of handling data conversion from the **API**, the `GetFromJsonAsync<TValue>` [extension method](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.getfromjsonasync?view=net-5.0) can be used - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0#httpclient-and-json-helpers).

**Note:** by default, the provided `HttpClient` will use the application's **URL** as a `BaseAddress` ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0#add-the-httpclient-service)). To enable the [injected](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-5.0#request-a-service-in-a-component) `HttpClient` to use a different **base URL**, the `Program.cs` in the main `Capgemini.Net.Blazor.WebAssembly.Client` project had to be modified ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-5.0#add-services-to-an-app)):

```
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Capgemini.Net.Blazor.Components.Services;
using Capgemini.Net.Blazor.Shared.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Capgemini.Net.Blazor.WebAssembly.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => 
                new HttpClient { BaseAddress = new Uri("http://localhost:5002/api/") });

            builder.Services.AddSingleton<Components.Services.Interfaces.IJSInteropService, JSInteropService>();
            builder.Services.AddSingleton<Components.Splitter.Services.Interfaces.IJSInteropService, Components.Splitter.Services.JSInteropService>();
            builder.Services.AddSingleton<Components.Tile.Services.Interfaces.IJSInteropService, Components.Tile.Services.JSInteropService>();

            builder.Services.AddScoped<ICheckboxSideNavService, CheckboxSideNavService>();

            await builder.Build().RunAsync();
        }
    }
}
```

The API project is configured to use the `5002` port and the `/api` suffix. If the `products/1` is called by the injected `HttpClient`, then the request to the `@http://localhost:5002/api/products/1` endpoint will be made.

Wrap the **API** call with a `try-catch` block to prevent runtime exceptions to be thrown - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0#handle-errors). Handle at least two type of exceptions:

- `NotSupportedException` which might be called if the **API** response with a non-JSON data type ([read more](https://docs.microsoft.com/en-us/dotnet/api/system.notsupportedexception?view=net-5.0)),
- `HttpRequestException` - a base class for exceptions thrown by the `HttpClient` (see an [example](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0#examples)).

```
using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Capgemini.Net.Blazor.Components.Demo.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo09.Start
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public ILogger<ContainerComponent> Logger { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        private IRateableProduct product = new RateableProduct();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var rateableProductViewModel = await HttpClient
                    .GetFromJsonAsync<RateableProductViewModel>("products/1");

                if (rateableProductViewModel is not null)
                {
                    product = new RateableProduct()
                    {
                        MinRate = rateableProductViewModel.MinRate,
                        AverageRate = rateableProductViewModel.AverageRate,
                        MaxRate = rateableProductViewModel.MaxRate,
                        CurrentRate = rateableProductViewModel.CurrentRate,
                    };
                }
            }
            catch (HttpRequestException e1)
            {
                Logger.LogError(
                    e1,
                    Properties.Resources.LOG_ERROR_CANNOT_FETCH,
                    "products/1");
            }
            catch (NotSupportedException e2)
            {
                Logger.LogError(
                    e2,
                    Properties.Resources.LOG_ERROR_RESPONSE_INVALID_FORMAT,
                    "products/1");
            }
        }
    }
}
```

`ILogger<ContainerComponent>` was provided by the Dependency Injection Container to gracefully print messages ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/logging?view=aspnetcore-5.0#log-in-razor-components)).

### Handle incomplete asynchronous actions at render

To prevent the application to start throwing runtime exception because of null reference before the **API** returns and assigns the value, the default instance implementing `IRateableProduct` was provided:

```
using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Capgemini.Net.Blazor.Components.Demo.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo09.Start
{
    public partial class ContainerComponent
    {
        // ...

        private IRateableProduct product = new RateableProduct();

        protected override async Task OnInitializedAsync()
        {
            var rateableProductViewModel = await HttpClient
                .GetFromJsonAsync<RateableProductViewModel>("products/1");

            if (rateableProductViewModel is not null)
            {
                product = new RateableProduct()
                {
                    MinRate = rateableProductViewModel.MinRate,
                    AverageRate = rateableProductViewModel.AverageRate,
                    MaxRate = rateableProductViewModel.MaxRate,
                    CurrentRate = rateableProductViewModel.CurrentRate,
                };
            }
        }
    }
}
```

Until the **API** call is resolved, `new RateableProduct()` is passed to the `RateComponent`:

```
<div class="demo__container_wrapper">
    @ChildContent(product)
</div>
```

Change the type of the `product` field to be nullable type:

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

namespace Capgemini.Net.Blazor.Components.Demo09.Start
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public ILogger<ContainerComponent> Logger { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        private `IRateableProduct?` product;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var rateableProductViewModel = await HttpClient
                    .GetFromJsonAsync<RateableProductViewModel>("products/1");

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
                    "products/1");
            }
            catch (NotSupportedException e2)
            {
                Logger.LogError(
                    e2,
                    Properties.Resources.LOG_ERROR_RESPONSE_INVALID_FORMAT,
                    "products/1");
            }
        }
    }
}
```

Line `35` uses `RateableProductAdapter` class to implicitly cast `RateableProductViewModel` to `IRateableProduct` interface.

Provide the additional markup for the `ContainerComponent` to handle the nullability of the context object ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#handle-incomplete-async-actions-at-render)):

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/09/img/2.jpg)

```
<div class="demo__container_wrapper">
    @if (product is null)
    {
        <LoadPlaceholder>
            <Message>
                <p>Loading product</p>
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
```

Before the **API** returns the `product`, Blazor won't render its main content (`RateComponent`). `LoadPlaceholder` (from the `Capgemini.Net.Blazor.Components.LoadPlaceholder` assembly) is used instead. Once the **API** returns and the `product` is reassigned, the `@ChildContent(product)` is rendered with the object returned from the **API**. `@ChildContent(product)` is defined as `RateComponent` in the markup of the `WrapperComponent.razor` file:

```
<ContainerComponent>
    <ChildContent Context="`product`">
        <RateComponent `Product="@product"`>
            <AvgRate Context="avgContext">
                <DefaultAverageRateComponent 
                    MinRate="@(avgContext.MinRate)"
                    AvgRate="@(avgContext.AvgRate)"
                    MaxRate="@(avgContext.MaxRate)" />
            </AvgRate>
        </RateComponent>
    </ChildContent>
</ContainerComponent>
```

Add the `loadingMessage` field to notify the user if an error occurred during the **API** call (by default if **API** throws an error in return it will be captured by the code, `product` won't be updated and the `LoadPlaceholder` will never be hidden):

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
```

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

namespace Capgemini.Net.Blazor.Components.Demo09.Start
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public ILogger<ContainerComponent> Logger { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        private IRateableProduct? product;

        private string loadingMessage = "Loading product";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var rateableProductViewModel = await HttpClient
                    .GetFromJsonAsync<RateableProductViewModel>("products/1");

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
                    "products/1");
            }
            catch (NotSupportedException e2)
            {
                Logger.LogError(
                    e2,
                    Properties.Resources.LOG_ERROR_RESPONSE_INVALID_FORMAT,
                    "products/1");
            }
            finally
            {
                if (product is null)
                {
                    loadingMessage = "Loading error";
                }
            }
        }
    }
}
```

### Use NavigationManager to fetch different products dynamically

Change the fixed **API** endpoint in `ContainerComponent.razor.cs` file to receive the `id` cascading parameter:

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

namespace Capgemini.Net.Blazor.Components.Demo09.Start
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

        private IRateableProduct? product;

        private string loadingMessage = "Loading product";

        protected override async Task OnInitializedAsync()
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
                    Properties.Resources.LOG_ERROR_RESPONSE_INVALID_FORMAT,
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
    }
}
```

`Id` cascading parameter is provided by the parent component of the `WrapperComponent`:

```
<Title Value=".NET Community Blazor Introduction | Demo 9"></Title>

@if (Context is not null)
{
    <CascadingValue Name="Id" Value="@Id">
        <FullTileContainer ChecklistContext="@Context">
            <DemoEndPoint>
                <Capgemini.Net.Blazor.Components.Demo09.End.WrapperComponent />
            </DemoEndPoint>
            <DemoStartPoint>
                <Capgemini.Net.Blazor.Components.Demo09.Start.WrapperComponent />
            </DemoStartPoint>
        </FullTileContainer>
    </CascadingValue>
}

@code {
	// ...
}
```

This parent component defines a **route** for the component to be displayed if the **URL** specified by the [Route](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.routeattribute?view=aspnetcore-5.0) attribute is selected ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#route-templates)):

- [Route(DemoTile.Href + "/{id:int?}")]

The route is defining the [route parameter](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#route-parameters) `id` with the additional [route constraint](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#route-constraints). The parameter can be binded with the property marked as a `[Parameter]` of the same name (route parameters are [case insensitive](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-3.1#route-parameters)) and type (lines `10-11`):

```
using Capgemini.Net.Blazor.Shared.Interfaces.Context;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Capgemini.Net.Blazor.Components.Demo09
{
    [Route(DemoTile.Href + "/{id:int?}")]
    public partial class FullDemoTile
    {
        [Parameter]
        public int Id { get; set; }
		
		// ...
	}
}
```

**Note:** Blazor has to explicitly define parameter type with the route constants. If the type of the `parameter` that was matched by its name won't match, the `InvalidOperationException` will be thrown.

**Note:** routes can be either defined by the usage of the [attribute routes](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#ar) or `@page` directive ([read more](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#page)).

To show different components, the given **URL**s can be followed:

- <a href="https://localhost:5001/demo09/1">/demo09/1</a>
- <a href="https://localhost:5001/demo09/2">/demo09/2</a>
- <a href="https://localhost:5001/demo09">/demo09/{productId}</a>

Navigating to one of the above **URL**s will force all the components to be rendered with new parameter values, including the `id` from the route.

To make the `ContainerComponent` to dynamically update its content, add the following markup:

```
@inject NavigationManager NavigationManager

<div class="demo__container_wrapper">
	<!-- ... -->
</div>

<div class="navigation-bar">
    @foreach (int productId in Enumerable.Range(1, 5))
    {
		<a href="#"
		   @onclick="@(() => NavigationManager.NavigateTo($"/demo09/{productId}"))"
		   @onclick:preventDefault
		>
			@productId
		</a>
    }
</div>
```

Lines `7-17` define five links that will navigate to the given **URL**s when the `@onclick` event is triggered ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#uri-and-navigation-state-helpers)). `@onclick:preventDefault` directive attribute is used to prevent a default click - it will cause the navigation to the `#` specified by the `href` attribute, canceling the custom navigation.

[NavigationManager](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.navigationmanager?view=aspnetcore-5.0) can be injected in the same way as the [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0). This is one of the [defaults](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-5.0#default-services) services provided by Blazor:
        
```
// ...

[Inject]
public NavigationManager NavigationManager { get; set; } = default!;

// ...
```

Add `NavLink` components from the `Microsoft.AspNetCore.Components.Routing` namespace ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#navlink-component)) and remove the injected `NavigationManager` to replace the raw `anchor` tag with the build-in Blazor component:

```
@using Microsoft.AspNetCore.Components.Routing

<div class="demo__container_wrapper">
	<!-- ... -->
</div>

<div class="navigation-bar">
    @foreach (int productId in Enumerable.Range(1, 5))
    {
        <NavLink href="@($"/demo09/{productId}")">
            @productId
        </NavLink>
    }
</div>
```

**Note:** `NavLink` component wraps its content with a `<a></a>` tags and provide all unmatched parameters (like `href`) as its own with a usage of the [attribute splatting](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#attribute-splatting-and-arbitrary-parameters). The component is using `NavigationManager` to provide same functionality as was removed from the markup (`NavigationManager` is injected inside the class for the `NavLink` component).

Blazor is trying to reuse components that are already rendered. That is why the `OnInitializedAsync` lifecycle method is not triggered each time the **URL** changes. Change the lifecycle method that the `ContainerComponent` is using to the `OnParametersSetAsync`. It will be triggered after each parameters' value change, including the `ProductId` cascading parameter:

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

namespace Capgemini.Net.Blazor.Components.Demo09.Start
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
                    Properties.Resources.LOG_ERROR_RESPONSE_INVALID_FORMAT,
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
    }
}
```

Lastly, replace the `ContainerComponent` markup to use the customized `NavLink` component - `ProductButton`:

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
    @foreach (int productId in Enumerable.Range(1, 5))
    {
        <ProductButton ProductId="@productId" />
    }
</div>
```

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/09/img/3.jpg)

### Provide default content for unused render fragments

Update `RateComponent`'s markup to show additional parameters of the selected product - `MaxRate` and `CurrentRate`:

```
@inherits RateComponentBase

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
    @Product.CurrentRate
</div>

<div class="average-rate-container">
    @AvgRate(AverageRateContext)
</div>

<div class="max-rate-container">
    @Product.MaxRate
</div>

@code {
    // ...
}
```

Replace raw values for `MaxRate` and `CurrentRate` with `RenderFragment` parameters:

```
@inherits RateComponentBase

<!-- ... -->

<div class="current-rate-container">
    @if (CurrentRate is null)
    {
        @CurrentRateString
    }
    else
    {
        @CurrentRate(CurrentRateContext)
    }
</div>

<div class="average-rate-container">
    @AvgRate(AverageRateContext)
</div>

<div class="max-rate-container">
    @if (MaxRate is null)
    {
        @(Product.MaxRate)
    }
    else
    {
        @MaxRate(Product.MaxRate)
    }
</div>

@code {
    [Parameter]
    public IRateableProduct Product { get; set; } = default!;

    [Parameter]
    public string Icon { get; set; } = "fa-star";

    [Parameter]
    public RenderFragment<AverageRateContext> CurrentRate { get; set; } = default!;

    [Parameter]
    public RenderFragment<AverageRateContext> AvgRate { get; set; } = default!;

    [Parameter]
    public RenderFragment<int> MaxRate { get; set; } = default!;

    private int CurrentRateInt => Product.CurrentRate ?? 0;

    private string CurrentRateString => Product.CurrentRate.HasValue ? Product.CurrentRate.Value.ToString() : "---";

    private AverageRateContext CurrentRateContext => new AverageRateContext()
    {
        MinRate = Product.MinRate,
        AvgRate = (decimal) CurrentRateInt,
        MaxRate = Product.MaxRate,
    };

    private AverageRateContext AverageRateContext => new AverageRateContext()
    {
        MinRate = Product.MinRate,
        AvgRate = Product.AverageRate,
        MaxRate = Product.MaxRate,
    };
}
```

Providing render fragments in place of fixed references allows the `RateComponent` to be more flexible and use same components both for `CurrentRate` and `AvgRate` (as both render fragments are using same `AverageRateContext` class as generic parameter). Components that can be used are:

- `DefaultAverageRateComponent`,
- `AlternativeAverageRateComponent`,
- `Alternative2AverageRateComponent`.

`AvgRate` render fragment is defined by the `WrapperComponent` (lines `4-9`), so it wasn't wrapped with the `@if` statement as two others `RenderFragment` parameters:

```
<ContainerComponent>
    <ChildContent Context="product">
        <RateComponent Product="@product">
            <AvgRate Context="avgContext">
                <DefaultAverageRateComponent 
                    MinRate="@(avgContext.MinRate)"
                    AvgRate="@(avgContext.AvgRate)"
                    MaxRate="@(avgContext.MaxRate)" />
            </AvgRate>
        </RateComponent>
    </ChildContent>
</ContainerComponent>
```

If a render fragment is not provided, the replacement markup is used based on `is null` condition. [Auto-property initializer](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#auto-property-initializers) for `RenderFragment` or `RenderFragment<TValue>` can also be set to a **HTML** markup with a usage of [read more](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#statement-lambdas">statement lambdas</a> (<a href="@https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#define-reusable-renderfragments-in-code)). Replace the default values for all `RenderFragment<TValue>` parameters and remove `@if` statements (use markups from default <mnark>@if</mnark> branches as default values for the parameters):

```
@inherits RateComponentBase

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
        } else
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

    private int CurrentRateInt => Product.CurrentRate ?? 0;

    private string CurrentRateString => Product.CurrentRate.HasValue ? Product.CurrentRate.Value.ToString() : "---";

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
}
```

Lambda statements allow to use any Razor syntax to define the default markup for render fragments and even provide the context objects for that markup. In result `RenderFragment<TValue>` defines a lambda expression (providing `context` parameter of `TValue` type) which defines a lambda statement as its content (which provides `__builder` parameter of `RenderTreeBuilder` type). That way:

- [condition blocks](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#conditionals-if-else-if-else-and-switch) (like `@if` can be used (lines `35-47`),
- custom components can be used, receiving their parameters from the provided `context` parameter (both lines `35-47` and `50-56`),
- simple razor syntax can be used (lines `59-62`).

`CurrentRateString` property was deleted in process.

**Note:** `__builder` parameter of type `RenderTreeBuilder` provided by the statement lambda can also be used to build a markup to be render ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/advanced-scenarios?view=aspnetcore-5.0#manual-rendertreebuilder-logic)):

```
@code {
	// ...
	
    [Parameter]
    public RenderFragment<AverageRateContext> AvgRate { get; set; } = context => __builder =>
    {
        __builder.OpenComponent(0, typeof(DefaultAverageRateComponent));
        __builder.AddAttribute(1, "MinRate", @context.MinRate);
        __builder.AddAttribute(2, "AvgRate", @context.AvgRate);
        __builder.AddAttribute(3, "MaxRate", @context.MaxRate);
        __builder.CloseComponent();
    };

    // ...
}
```

**Note:** to define the inline markup, special combination of Razor syntax can be used together:

- `@:` - [explicit line transition](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#explicit-line-transition) enables Razor to render text as **Razor mark-up** inside the [code block context](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#control-structures),
- `@{ }` - [Razor code block](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#no-locrazor-code-blocks) enables to define **C#** code inside the ***.razor** files.

Together they form `@:@{` syntax (informally called [Wig-pig](https://blazor-university.com/templating-components-with-renderfragements/passing-placeholders-to-renderfragments)). That combination allows to define Razor markup block to be interpreted by the Razor engine during the ***.razor** file compilation (and translate the given markup to **HTML** markup which in turn will be transformed to the `RenderTreeBuilder` set of methods).

```
@code {
	// ...
	
    [Parameter]
    public RenderFragment<AverageRateContext> CurrentRate { get; set; } = context => @: @{
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
    }
    ;

    [Parameter]
    public RenderFragment<AverageRateContext> AvgRate { get; set; } = context => @: @{
        <DefaultAverageRateComponent
            MinRate="@context.MinRate"
            AvgRate="@context.AvgRate"
            MaxRate="@context.MaxRate" />
    }
    ;

    [Parameter]
    public RenderFragment<int> MaxRate { get; set; } = context => @: @{ <div> @(context) </div> }
    ;
	
	// ...
}
```

**Note:** that form of syntax require semicolon character (`;`) to be left over to the new line as a delimiter for the next line of code (`@:` by definition will treat every character in line after it as Razor syntax, so the `;` won't be recognize as the terminal character for the line).