### Use validation attributes for the form inputs instead of custom logic in setters

Replace all custom properties of the `./Start/RateContext.cs` file with auto-generated properties. Preserve default values from private fields (`maxRate = 6` and `avgRate = 3`):

```
namespace Capgemini.Net.Blazor.Components.Demo08.Start
{
    public class RateContext
    {
        public static readonly string[] Icons = {
            "fa-star",
            "fa-grin-stars",
            "fa-angry",
            "fa-sun"
        };

        public int MaxRate { get; set; } = 6;

        public int AvgRate { get; set; } = 3;

        public string Icon { get; set; } = Icons[0];
    }
}
```

**Note:** as the `CapSelect` component was used to render the drop-down list for the `Icons` array, there is also no need to keep the `IconIndex` property with its custom setter (`CapSelect` renders its options through the `@for (int i = 0; i < RateContext.Icons.Length; i += 1)` loop).

Simplify the markup of the `ContainerComponent.razor` file to make the `CapSelect` component to work directly with the **Font Awesome**'s **CSS** classes provided by the `RateContext.Icons`. As the component no longer needs to handle converting index value of the `Icons` array to one of its value with the delegate (`ParseValue="@((option) => RateContext.Icons[option])">`) that parameter can be removed and the entire loop simplified:

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
        <CapInput Label="Max rate" InputName="@nameof(rateContext.MaxRate)">
            <InputNumber @bind-Value="@rateContext.MaxRate" />
        </CapInput>

        <CapInput Label="Avg rate" InputName="@nameof(rateContext.AvgRate)">
            <InputNumber @bind-Value="@rateContext.AvgRate" />
        </CapInput>

        <CapSelect Label="Icon" @bind-Value="rateContext.Icon">
            @foreach (string icon in RateContext.Icons)
            {
                <CapOption Key="@icon" Value="@icon">
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
        @AvgRate(AverageRateContext)
    </div>
</div>
```

Once the custom statements for class property setters were removed, inputs can be used to set any value for the properties that are binded to them with `@bind-Value="@rateContext.<PROPERTY>"` markup. To add a validation for the inputs, the [build-in attributes](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#validation-attributes) can be used.

Modify the `RateContext` class by adding a list of validation attributes

- `MaxRate` property should be obligatory and the value cannot be smaller that `2` (to show at least two rate icons, to enable the user to rate). Use the following attributes:
  - `[Required]`
  - `[Range(2, int.MaxValue)]`
- `AvgRate` property should also be required. Value of the property is bounded by the lower possible value of the `MaxRate` input and current value of that property. Use the following attributes:
  - `[Required]`
  - `[Range(2, int.MaxValue)]`
  - `[CompareTo(CompareToAttribute.CompareTo.LESS_THAN_OR_EQUAL, "MaxRate")]`
- `Icon` can be left without any kind of validation - the `CapSelect` component child's content doesn't allow the user to select other values that those defined with `CapOption` components.

```
using Capgemini.Net.Blazor.Components.Demo;
using System.ComponentModel.DataAnnotations;

namespace Capgemini.Net.Blazor.Components.Demo08.Start
{
    public class RateContext
    {
        public static readonly string[] Icons = {
            "fa-star",
            "fa-grin-stars",
            "fa-angry",
            "fa-sun"
        };

        [Required]
        [Range(2, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int MaxRate { get; set; } = 6;

        [Required]
        [Range(2, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        [CompareTo(CompareToAttribute.CompareTo.LESS_THAN_OR_EQUAL, "MaxRate")]
        public int AvgRate { get; set; } = 3;

        public string Icon { get; set; } = Icons[0];
    }
}
```

List of the build-in validation attributes does not have one that can compare one property against another in other way than for equality (see [CompareAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.compareattribute?view=net-5.0)). `CompareTo` attribute is a [custom](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#custom-validation-attributes) validation attribute defined in the `Capgemini.Net.Blazor.Components.Demo` namespace, allowing to compare a given property with any other specified (one of the `CompareToAttribute.CompareTo` enum values can be used to define a desired relation to check).

**Note:** `[Compare]` is a part of the [build-in](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#validation-attribute) validation attributes defined in the `System.ComponentModel.DataAnnotations` namespace. Not all attributes are suitable to be used for Blazor application (i.e [[Remote]](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#remote-attribute)). Blazor introduces its own package for validation purposes that can be used to extend the attribute-based validation capabilities (for example `[Compare]` can be replaced with the `[CompareProperty]` - [read more](https://www.nuget.org/packages/Microsoft.AspNetCore.Components.DataAnnotations.Validation)).

To make the `EditForm` recognize the validation attributes of the object type provided by the `EditContext` parameter (or directly through the `Model` property), `DataAnnotationsValidator` component has to be used. Modify the markup of the `ContainerComponent` to provide additional functionality:

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
        <DataAnnotationsValidator />

        <CapInput Label="Max rate" InputName="@nameof(rateContext.MaxRate)">
            <ChildContent>
                <InputNumber @bind-Value="@rateContext.MaxRate" />
            </ChildContent>
            <ValidationContent>
                <ValidationMessage For="@(() => @rateContext.MaxRate)" />
            </ValidationContent>
        </CapInput>

        <CapInput Label="Avg rate" InputName="@nameof(rateContext.AvgRate)">
            <ChildContent>
                <InputNumber @bind-Value="@rateContext.AvgRate" />
            </ChildContent>
            <ValidationContent>
                <ValidationMessage For="@(() => @rateContext.AvgRate)" />
            </ValidationContent>
        </CapInput>

        <CapSelect Label="Icon" @bind-Value="rateContext.Icon">
            @foreach (string icon in RateContext.Icons)
            {
                <CapOption Key="@icon" Value="@icon">
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
        @AvgRate(AverageRateContext)
    </div>
</div>
```

**Note:** to make validation summary visible, `ValidationSummary` component can be used - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#validation-summary-and-validation-message-components).

`CapInput` component is capable of receiving multiple render fragments, just like the `ContainerComponent`. Both `<ChildContent>` and `<ValidationContent>` do not provide a context values to be used - they are simple placeholders for:


- input (either custom or build-in component/markup i.e. `<InputNumber @bind-Value="@rateContext.MaxRate" />`),

- validation message (either custom or build-in component/markup i.e. `<ValidationMessage For="@(() => @rateContext.MaxRate)" />`),

Of course both parameters of the `RenderFragment` type can accept any other markup as well.

**Note:** without the `Components.DataAnnotations.Validation` package, Blazor is not able to validate complex types (the package provides `ObjectGraphDataAnnotationsValidator` component to make a replacement for `DataAnnotationsValidator`. That component supports additional validation attributes defined within the package i.e. `ValidateComplexType`).

### Prevent invalid values to be used, provide custom input controls

Providing invalid values for either of the `Max rate` or `Avg rate` inputs will cause the application to rise exceptions. Displaying validation errors with `DataAnnotationsValidator` or `ValidationSummary` won't prevent a user to input those values.

Create new `AvgRateInputNumber` Razor component that derives from the [build-in](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#built-in-forms-components) `InputNumber<TValue>` component (see the [example](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#inputtext-based-on-the-input-event)):

```
@typeparam TValue
@inherits InputNumber<TValue>

<input
    @attributes="AdditionalAttributes"
    class="@CssClass"
    value="@CurrentValue"
    @oninput="@((value) => CurrentValueAsString = value.Value?.ToString())" />
```

`InputNumber<TValue>` component which the `AvgRateInputNumber` [@inherits](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#inherits) defines:

- `AdditionalAttributes` - a dictionary type parameter that will capture all **DOM** attributes that were provided to the component but the component does not define them as its parameters. This allows to pass through any attributes and their values down the tree of components/sub-components, making components in the tree to capture a needed subset of attributes. It is mainly used for [attributes splatting](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#attribute-splatting-and-arbitrary-parameters) to provide attributes to any non-blazor markup (like `<input>`).
- `CssClass` - a readonly property that represents the value of the `class` attribute that was captured by the `AdditionalAttributes` dictionary (i.e. `<ADDITIONAL CLASSES>` for the `<AvgRateInputNumber class="<ADDITIONAL CLASSES>" ... />`). **CSS** classes defined in that way will be merged with validation-specific **CSS** classes that inherited `InputNumber<TValue>` class handles.
- `CurrentValue` - wrapper for the `Value` parameter of the `InputNumber<TValue>` component. On that property change, value binded to the input will be updated - `Value` will be notified of that change and `@bind-Value` markup will propagate the change to it's value (i.e. `VALUE` for the `<AvgRateInputNumber @bind-Value="<VALUE>" ... />`).
- `CurrentValueAsString` - as it is named, the property wraps the `CurrentValue` and provide its `string` representation on get (by `FormatValueAsString` method, which by default is implemented as `CurrentValue.ToString()`) and converts the given `string` to the `TValue` type, allowing the value to be assigned to the `CurrentValue` (which in turn will trigger `Value` parameter change and in that way the variable binded with the `@bind-Value` directive will be updated).

**Note:** `InputNumber<TValue>` accepts a generic type (based on the [documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.inputnumber-1?view=aspnetcore-5.0), component's `TValue` can be any of the **.NET** numeric types). In order to mark the custom `AvgRateInputNumber ` Razor component as generic, `@typeparam` attribute has to be used ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-parameters)). Attribute can be omitted if the component inherits from the `InputNumber<TValue>` but the generic type of the base class is fixed (i.e. `InputNumber<int>`):

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/08/img/1.jpg)

`AvgRateInputNumber` alters the default behavior of the `InputNumber` component to notify the `EditForm` component of the input change on every change (using `input` event) - by default `EditForm` will be notified of the change only on the input's `change` event (i.e. after moving to the next `<input>` control - [read more](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.editcontext.notifyvalidationstatechanged?view=aspnetcore-5.0)):

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/08/img/2.jpg)

**Note:** `<input>` field of numeric type (`type="number"`) allows the user to change its value with arrow keys (`[↑]` and `[↓]`). Changing the value in that way will trigger the `input`, not `change` event. The second event is triggered if the user selects the input's value, provides a new one as a string and leaves the field.

Update the `ContainerComponent` razor file with the custom `AvgRateInputNumber` component:

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
        <DataAnnotationsValidator />

        <CapInput Label="Max rate" InputName="@nameof(rateContext.MaxRate)">
            <ChildContent>
                <AvgRateInputNumber @bind-Value="@rateContext.MaxRate" type="number" />
            </ChildContent>
            <ValidationContent>
                <ValidationMessage For="@(() => @rateContext.MaxRate)" />
            </ValidationContent>
        </CapInput>

		<!-- ... -->
    </EditForm>

    <!-- ... -->
</div>
```

**Note:** `type="number"` attribute key-value pair will be captured by the internal `AdditionalAttributes` dictionary parameter. `Type` is an [attribute](https://developer.mozilla.org/pl/docs/Web/HTML/Element/Input) for the `<input>` markup and it is passed with other attributes, captured by this dictionary:

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/08/img/3.jpg)

`<input>` tag with a `type="number"` supports useful range of attributes like:

- `min`,
- `max`,
- `step`

which can be used to make the values of those inputs be bounded by the required properties without changing the `AvgRateInputNumber`/`InputNumber` components or any other that derives from `InputBase` (thanks to the `AdditionalAttributes` parameter it provides and passes all captured key-value pairs as attributes for the `<input>`). Modify the `ContainerComponent` to include those extra attributes:

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
        <DataAnnotationsValidator />

        <CapInput Label="Max rate" InputName="@nameof(rateContext.MaxRate)">
            <ChildContent>
                <AvgRateInputNumber
                    @bind-Value="@rateContext.MaxRate"
                    type="number"
                    min="@rateContext.AvgRate" />
            </ChildContent>
            <ValidationContent>
                <ValidationMessage For="@(() => @rateContext.MaxRate)" />
            </ValidationContent>
        </CapInput>

        <CapInput Label="Avg rate" InputName="@nameof(rateContext.AvgRate)">
            <ChildContent>
                <AvgRateInputNumber
                    @bind-Value="@rateContext.AvgRate"
                    type="number"
                    min="2"
                    max="@rateContext.MaxRate" />
            </ChildContent>
            <ValidationContent>
                <ValidationMessage For="@(() => @rateContext.AvgRate)" />
            </ValidationContent>
        </CapInput>

        <CapSelect Label="Icon" @bind-Value="rateContext.Icon">
            @foreach (string icon in RateContext.Icons)
            {
                <CapOption Key="@icon" Value="@icon">
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
        @AvgRate(AverageRateContext)
    </div>
</div>
```

**Note:** usage of [attribute splatting](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#attribute-splatting-and-arbitrary-parameters) is not recommended from the performance point of view. Care must be taken if a parameter marked with `[Parameter(CaptureUnmatchedValues = true)]` is used for a components with multiple instances in the application (i.e. rendered by a loop) - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#avoid-attribute-splatting-with-captureunmatchedvalues).

**Note:** position of the `@attributes="AdditionalAttributes"` attribute matters. By default when `@attributes` are splatted on the element, the attributes are processed from right to left ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#attribute-splatting-and-arbitrary-parameters)).

### Use edit form context to provide cross-field value correctness

Although either of `AvgRateInputNumber` inputs aren't allowed the invalid values to be selected, changing value of one input can still generate an invalid state of the application (i.e. decreasing the value of the `MaxRate` property will decrease the allowed range of the `AvgRate` input and at some point the value of that input might extend the `max="@rateContext.MaxRate"`, throwing a runtime exception in result). To fix that values of both inputs has to be connected to each other.

Create the `./Start/MaxRateInputNumber.razor` file with the given content:

```
@typeparam TValue
@inherits RateInputNumberBase<TValue>
@implements IDisposable

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       value="@CurrentValue"
       @oninput="@((value) => CurrentValueAsString = value.Value?.ToString())" />

@code {
    protected override void OnInitialized()
    {
        EditContext.OnFieldChanged += HandleFieldChanged;
        base.OnInitialized();
    }

    private void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        => OnSelectedFieldChanged<RateContext>(nameof(RateContext.AvgRate), e, UpdateField);

    private void UpdateField(RateContext model, FieldIdentifier fieldIdentifier)
    {
        if (model.MaxRate < model.AvgRate)
        {
            CurrentValueAsString = model.AvgRate.ToString();
            EditContext.Validate();
        }
    }

    public void Dispose()
    {
        EditContext.OnFieldChanged -= HandleFieldChanged;
    }
}
```

- The component inherits from the `RateInputNumberBase<TValue>` class (line `2`). The class provides the `OnSelectedFieldChanged<T>(string fieldName, FieldChangedEventArgs args, Action<T, FieldIdentifier> action)` method which will trigger an `action` whenever a value of the `fieldName` input changed.
- `EditForm` component that wraps inputs for `MaxRate`, `AvgRate` and `Icon` provides a `EditContext` object for all of them:

```
<!-- ... -->

<EditForm Model="@rateContext">
	<!-- ... -->
</EditForm>

<!-- ... -->
```

This object is shared across all inputs inside the given `EditForm` (`EditContext` that the `EditForm` is building based on the provided model `Model="@rateContext"` is shared by the usage of `AdditionalAttributes` component like so: `<CascadingValue IsFixed="true" Value="_editContext">`). Line `13` is using one of the `EditContext`'s events to make a subscription for any input value change ([read more](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.editcontext.onfieldchanged?view=aspnetcore-5.0)).

- With `OnFieldChanged` callback raised because of the `AvgRate` input has changed, method called by the `HandleFieldChanged` (lines `17-18`) makes the `UpdateField` method to be executed (`OnFieldChanged` is called upon any input value change but `OnSelectedFieldChanged` filters those calls and passes through only the callbacks triggered by the `RateContext.AvgRate` input).
- If the `AvgRate` change would result in `AvgRate` to be greater than `MaxRate` (which would make the `AvgRate` value invalid, resulting in throwing an exception), input's value for `MaxRate` will be updated to be the same as `AvgRate` (lines `22-26`). Then the validation will be triggered (otherwise the validation result from the validation triggered by the `[CompareTo(CompareToAttribute.CompareTo.LESS_THAN_OR_EQUAL, "MaxRate")]` before the `MaxRate` had a chance to be updated, won't be overwritten).
- On component destruction, any subscription done to the `OnFieldChanged` event has to be disposed gracefully (line `31`) - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#component-disposal-with-idisposable).

Replace the content for the other input custom component - `AvgRateInputNumber`. Add same `@code {}` block as `MaxRateInputNumber` has (change the filed name that the component is listening for from `nameof(RateContext.AvgRate)` to `nameof(RateContext.MaxRate)` and the property to be assigned to the `CurrentValueAsString` - from `AvgRate` to `MaxRate`):

```
@typeparam TValue
@inherits RateInputNumberBase<TValue>
@implements IDisposable

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       value="@CurrentValue"
       @oninput="@((value) => CurrentValueAsString = value.Value?.ToString())" />

@code {
    protected override void OnInitialized()
    {
        EditContext.OnFieldChanged += HandleFieldChanged;
        base.OnInitialized();
    }

    private void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        => OnSelectedFieldChanged<RateContext>(`nameof(RateContext.MaxRate)`, e, UpdateField);

    private void UpdateField(RateContext model, FieldIdentifier fieldIdentifier)
    {
        if (model.MaxRate < model.AvgRate)
        {
            CurrentValueAsString = model.`MaxRate`.ToString();
            EditContext.Validate();
        }
    }

    public void Dispose()
    {
        EditContext.OnFieldChanged -= HandleFieldChanged;
    }
}
```

Remove the `min` and `max` boundaries for both input components in the `ContainerComponent` (except for the default `min="2"`). Replace the `AvgRateInputNumber` component for the `Max rate` input to the `MaxRateInputNumber` component:

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
        <DataAnnotationsValidator />

        <CapInput Label="Max rate" InputName="@nameof(rateContext.MaxRate)">
            <ChildContent>
                <`MaxRateInputNumber`
                    @bind-Value="@rateContext.MaxRate"
                    type="number"
                    min="2" />
            </ChildContent>
            <ValidationContent>
                <ValidationMessage For="@(() => @rateContext.MaxRate)" />
            </ValidationContent>
        </CapInput>

        <CapInput Label="Avg rate" InputName="@nameof(rateContext.AvgRate)">
            <ChildContent>
                <AvgRateInputNumber
                    @bind-Value="@rateContext.AvgRate"
                    type="number"
                    min="2" />
            </ChildContent>
            <ValidationContent>
                <ValidationMessage For="@(() => @rateContext.AvgRate)" />
            </ValidationContent>
        </CapInput>

        <CapSelect Label="Icon" @bind-Value="rateContext.Icon">
            @foreach (string icon in RateContext.Icons)
            {
                <CapOption Key="@icon" Value="@icon">
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
        @AvgRate(AverageRateContext)
    </div>
</div>
```

**Note:** both `@code {}` blocks for `AvgRateInputNumber` and `MaxRateInputNumber` can be simplified with the `Capgemini.Net.Blazor.Components.Demo.RateInputNumberAbstractBase` class. See final `AvgRateInputNumber`:

```
@typeparam TValue
@inherits RateInputNumberAbstractBase<TValue>
@implements IDisposable

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       value="@CurrentValue"
       @oninput="@((value) => CurrentValueAsString = value.Value?.ToString())" />

@code {
    protected override void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        => OnSelectedFieldChanged<RateContext>(nameof(RateContext.MaxRate), e, UpdateField);

    private void UpdateField(RateContext model, FieldIdentifier fieldIdentifier)
    {
        if (model.MaxRate < model.AvgRate)
        {
            CurrentValueAsString = model.MaxRate.ToString();
            EditContext.Validate();
        }
    }
}
```

...and `MaxRateInputNumber` code:

```
@typeparam TValue
@inherits RateInputNumberAbstractBase<TValue>
@implements IDisposable

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       value="@CurrentValue"
       @oninput="@((value) => CurrentValueAsString = value.Value?.ToString())" />

@code {
    protected override void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        => OnSelectedFieldChanged<RateContext>(nameof(RateContext.AvgRate), e, UpdateField);

    private void UpdateField(RateContext model, FieldIdentifier fieldIdentifier)
    {
        if (model.MaxRate < model.AvgRate)
        {
            CurrentValueAsString = model.AvgRate.ToString();
            EditContext.Validate();
        }
    }
}
```

### Change the behavior of the number inputs to forbids invalid values to be set

Both `MaxRateInputNumber` and `AvgRateInputNumber` components can still be set to invalid values, as by default `min`, `max` and `step` attributes only works if the value is selected by any of the arrow keys (`[↑]` and `[↓]`). Those parameters don't work if a user changes the value of the input in the other way (i.e. by deleting the content of the input and type a new value manually).

Create a new base class for `MaxRateInputNumber` and `AvgRateInputNumber` components - `RateInputMinMaxNumberAbstractBase`:

```
using Capgemini.Net.Blazor.Components.Demo;
using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Capgemini.Net.Blazor.Components.Demo08.Start
{
    public abstract class RateInputMinMaxNumberAbstractBase<TValue> : RateInputNumberAbstractBase<TValue>
    {
        [Parameter]
        public TValue Min { get; set; } = default!;

        [Parameter]
        public TValue Max { get; set; } = default!;

        protected override bool TryParseValueFromString(
            string? value,
            [MaybeNullWhen(false)] out TValue result,
            [NotNullWhen(false)] out string? validationErrorMessage)
        {
            if (base.TryParseValueFromString(value, out result, out validationErrorMessage))
            {
                IComparable<TValue>? selfValueComparer = result as IComparable<TValue>;

                if (selfValueComparer is not null)
                {
                    if (Min is not null && selfValueComparer.CompareTo(Min) < 0)
                    {
                        result = Min;
                    }

                    if (Max is not null && selfValueComparer.CompareTo(Max) > 0)
                    {
                        result = Max;
                    }
                }

                return true;
            }

            return false;
        }
    }
}
```

`RateInputMinMaxNumberAbstractBase` class adds custom parameters for `Min` and `Max` (lines `10-11`, `13-14`). Those parameters will be used to replace the default `min`, `max` attributes for the `<input>` components that were used.

The class overwrites the `TryParseValueFromString` method which is used by the `InputBase<TValue>` to update the `CurrentValue` (of type `TValue`) if the `CurrentValueAsString` is changed - like on the `input` event in line `10`:

```
@typeparam TValue
@inherits RateInputNumberAbstractBase<TValue>
@implements IDisposable

<!-- ... -->

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       value="@CurrentValue"
       @oninput="@((value) => `CurrentValueAsString = value.Value?.ToString())`" />

@code {
	// ...
}
```

It handles the conversion to the `TValue` type and returns it by the `result` output parameter. By adding a custom logic, this value can be forced to not extend any of the `Min` or `Max` parameters (lines `27-35`).

Replace the class that both `MaxRateInputNumber` and `AvgRateInputNumber` inherits from `RateInputNumberAbstractBase` to the `RateInputMinMaxNumberAbstractBase` abstract class:

```
@typeparam TValue
@inherits `RateInputMinMaxNumberAbstractBase`<TValue>
@implements IDisposable

<!-- ... -->

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       value="@CurrentValue"
       @oninput="@((value) => CurrentValueAsString = value.Value?.ToString())" />

@code {
	// ...
}
```

```
@typeparam TValue
@inherits `RateInputMinMaxNumberAbstractBase`<TValue>
@implements IDisposable

<!-- ... -->

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       value="@CurrentValue"
       @oninput="@((value) => CurrentValueAsString = value.Value?.ToString())" />

@code {
	// ...
}
```

Update the markup of the `ContainerComponent` to use `Min`, `Max` parameters, provided by the new base class:

```
<div class="demo__container_wrapper">
    <EditForm Model="@rateContext">
        <DataAnnotationsValidator />

        <CapInput Label="Max rate" InputName="@nameof(rateContext.MaxRate)">
            <ChildContent>
                <MaxRateInputNumber
                    @bind-Value="@rateContext.MaxRate"
                    Min="@rateContext.AvgRate"
                    Max="@int.MaxValue"
                    type="number" />
            </ChildContent>
            <ValidationContent>
                <ValidationMessage For="@(() => @rateContext.MaxRate)" />
            </ValidationContent>
        </CapInput>

        <CapInput Label="Avg rate" InputName="@nameof(rateContext.AvgRate)">
            <ChildContent>
                <AvgRateInputNumber
                    @bind-Value="@rateContext.AvgRate"
                    Min="2"
                    Max="@rateContext.MaxRate"
                    type="number" />
            </ChildContent>
            <ValidationContent>
                <ValidationMessage For="@(() => @rateContext.AvgRate)" />
            </ValidationContent>
        </CapInput>

        <CapSelect Label="Icon" @bind-Value="rateContext.Icon">
            @foreach (string icon in RateContext.Icons)
            {
                <CapOption Key="@icon" Value="@icon">
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
        @AvgRate(AverageRateContext)
    </div>
</div>
```

Finally update the markup of both `MaxRateInputNumber` and `AvgRateInputNumber` components. Replace the `value` attribute and the `@oninput` directive:

```
@typeparam TValue
@inherits RateInputMinMaxNumberAbstractBase<TValue>
@implements IDisposable

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       @bind-value="@CurrentValueAsString"
       @bind-value:event="oninput" />

@code {
    protected override void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        => OnSelectedFieldChanged<RateContext>(nameof(RateContext.AvgRate), e, UpdateField);

    private void UpdateField(RateContext model, FieldIdentifier fieldIdentifier)
    {
        if (model.MaxRate < model.AvgRate)
        {
            CurrentValueAsString = model.AvgRate.ToString();
            EditContext.Validate();
        }
    }
}
```

```
@typeparam TValue
@inherits RateInputMinMaxNumberAbstractBase<TValue>
@implements IDisposable

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       @bind-value="@CurrentValueAsString"
       @bind-value:event="oninput" />

@code {
    protected override void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        => OnSelectedFieldChanged<RateContext>(nameof(RateContext.MaxRate), e, UpdateField);

    private void UpdateField(RateContext model, FieldIdentifier fieldIdentifier)
    {
        if (model.MaxRate < model.AvgRate)
        {
            CurrentValueAsString = model.MaxRate.ToString();
            EditContext.Validate();
        }
    }
}
```

In place of the deleted markup, the `@bind-value` was used. By default it is a shorthand for the given markup ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0)):

```
@typeparam TValue
@inherits RateInputMinMaxNumberAbstractBase<TValue>
@implements IDisposable

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       value="@CurrentValueAsString"
       @onchange="@((ChangeEventArgs __e) => CurrentValueAsString = 
			__e.Value.ToString())" />

@code {
	// ...
}
```

By using a [directive attribute](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#razor-syntax) `@bind-value:event="oninput"` the default event for the binding can be changed. By default that event is `onchange`. With the event changed, the equivalent markup would be like this:

```
@typeparam TValue
@inherits RateInputMinMaxNumberAbstractBase<TValue>
@implements IDisposable

<input @attributes="AdditionalAttributes"
       class="@CssClass"
       value="@CurrentValueAsString"
       `@oninput`="@((ChangeEventArgs __e) => CurrentValueAsString = 
			__e.Value.ToString())" />

@code {
	// ...
}
```

and the new value will be assigned to the `CurrentValueAsString` variable on every input's change (not only on input's blur).

Directive attributes can also control other aspects of the event like:

- `@on{EVENT}:preventDefault` - to prevent a default `{EVENT}` behavior ([see example](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/event-handling?view=aspnetcore-5.0#prevent-default-actions)),
- `@on{EVENT}:stopPropagation` - to prevent the `{EVENT}` to be propagated ([see example](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/event-handling?view=aspnetcore-5.0#stop-event-propagation)).

**Note:** by providing values for both `Min` and `Max` parameters, field values will no longer be updated on other field's value change (i.e. `Max rate` won't be changed alongside with the `Avg rate` if the last one would be increased in the way it's value would be greater than `Max rate` - `Max="@rateContext.MaxRate"` used to bound the value of the `AvgRateInputNumber` will prevent the scenario from happening).