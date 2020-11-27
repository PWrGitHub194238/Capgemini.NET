![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/1.png)
![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/2.png)
![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/3.png)
![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/4.png)

## Agenda

This is a short description of each of the examples from 1 to 12 that this project consists of:

- Demo 01: during this exercise the simple icon-base rate component will be built.
  - [Razor Components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0),
  - [Razor code blocks](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#no-locrazor-mark-blocks),
  - [One-way binding](https://blazor-university.com/components/one-way-binding/),
  - [Lambda variable capturing](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#capture-of-outer-variables-and-variable-scope-in-lambda-expressions).
- Demo 02: buttons to select number of icons and their type will be added.
  - [Nested coponents](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#use-components),
  - [Partial classes](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#partial-class-support),
  - [Blazor directives](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#using),
  - [Namespace hierarchy](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#namespaces),
  - [Library components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/class-libraries?view=aspnetcore-5.0&tabs=visual-studio#consume-a-library-component),
  - [Component parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#component-parameters).
- Demo 03: at this point CSS component-related classes will be moved from global wwwroot directory to component, CSS encapsulation wil be added, C# logic separated from the markup, base class for the component created. Decreasing MaxRate while user already rated with MaxRate will be handled by the useage of the lifecycle method.
  - [CSS Isolation](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-5.0),
  - [Web Essentials](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebEssentials2019),
  - [@inherits directive](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#inherits),
  - [Lifecycle](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0).
- Demo 04: the markup for the specific rate component would be extracted, making possible to define it as a Blazor component's child content. Cascading parameters will be added to provide scoped variables for render fragment, making a aparent component to push down values without knowing which component would receive them.
  - [Child content](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#child-content),
  - [Render fragments](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.renderfragment?view=aspnetcore-5.0),
  - [Cascading values](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters?view=aspnetcore-5.0).
- Demo 05: this exercise will introduce a usage of the basic Blazor build-in components, replacing buttons from **Demo 02**. Custom components that inherits from build-in input components will be used to provide custom styling and behaviour.
  - [Fixed cascading values](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#ensure-cascading-parameters-are-fixed),
  - [EditForm component](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.editform?view=aspnetcore-5.0),
  - [Build-in input componrnts](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#built-in-forms-components),
  - [EditContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.editcontext?view=aspnetcore-5.0).
- Demo 06: in this example the context-providing cascading value will be replaced by the generic render fragment, to make parent component able to provide it to use in its child content markup to be written.
  - [Generic RenderFragment](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.renderfragment-1?view=aspnetcore-5.0),
  - [Template parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-parameters),
  - [Template context parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-context-parameters),
- Demo 07: the UI fragment for displaying the average rate will be extracted from the RateComponent to be on the same level and provided as another render fragment.
  - [Multiple generic RenderFragments](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.renderfragment-1?view=aspnetcore-5.0),
  - [Template parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-parameters),
  - [Template context parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-context-parameters).
- Demo 08: during this exercise the validation for the inputs will be enhanced to not allow invalid inputs based on a cross-field validation.
  - [Build-in validation attributes](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#validation-attributes),
  - [Custom validation attributes](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#custom-validation-attributes),
  - [ValidationSummary and ValidationMessage build-in components](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#validation-summary-and-validation-message-components),
  - [Extending buil-in components](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#inputtext-based-on-the-input-even),
  - [Generic components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/templated-components?view=aspnetcore-5.0#template-parameters),
  - [Attribute splatting](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#attribute-splatting-and-arbitrary-parameters),
  - [EditForm events](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.editcontext.onfieldchanged?view=aspnetcore-5.0),
  - [Disposing components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#component-disposal-with-idisposable),
  - [Directive attributes](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#razor-syntax).
- Demo 09: API to fetch the data for the component instead of the from will be added, default markup for render fragments will be introduced.
  - [Dependency Injection](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-5.0),
  - [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0),
  - [Calling API](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0),
  - [JSON helpers](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0#httpclient-and-json-helpers),
  - [Handling API exceptions](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0#handle-errors),
  - [ILogger<TValue> service](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/logging?view=aspnetcore-5.0#log-in-razor-components),
  - [Incomplete async actions handling](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#handle-incomplete-async-actions-at-render),
  - [NavigationManager service](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.navigationmanager?view=aspnetcore-5.0),
  - [NavLink component](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#navlink-component),
  - [Routing parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#route-parameters),
  - [Routing constraints](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#route-constraints),
  - [Reusable render fragments](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#define-reusable-renderfragments-in-code)
- Demo 10: during this exercise the component will be updated to fetch a random product if no product ID was provided and to store rates that user selects by calling the API POST method.
  - [Component events](https://blazor-university.com/components/component-events),
  - [Capture references to components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#capture-references-to-components),
  - [Posting data to API](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-5.0),
  - [StateHasChanged method](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#state-changes).
- Demo 11: HTML markup for each rate icon will be replaced by the Blazor component, replacing the common base class logic.
  - [Bind across components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0#bind-across-more-than-two-components),
  - [Data binding](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0),
  - [Fixed cascading parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-performance-best-practices?view=aspnetcore-5.0#ensure-cascading-parameters-are-fixed).
- Demo 12: last example will enable the user to provide rate icons as render fragment.
  - [Self-referencing cascading value](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters?view=aspnetcore-5.0#tabset-example).
