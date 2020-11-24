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
- Demo 06:
