![Capgemini Blazor](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/capgemini-blazor.png "Capgemini Blazor")

## Agenda

Agenda for this project and presentation:

- **[Introduction to Blazor - part 1](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview)** (Blazor WebAssembly live demo):
  - 12 excercises to folow step by step,
  - introducing Blazor-specific elements like:
    - Razor [components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0) & [parameters](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#component-parameters),
	- [one-way](https://blazor-university.com/components/one-way-binding/) & [two-way](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-5.0) bindings,
	- [rendering fragments](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.renderfragment?view=aspnetcore-5.0) & [generic render fragments](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.renderfragment-1?view=aspnetcore-5.0),
	- [lifecycle methods](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0),
	- built-in components: [cascading values](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters?view=aspnetcore-5.0), [form](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#built-in-forms-components) & [navigation](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#navlink-component) components,
	- [Blazor services](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-5.0&pivots=webassembly) & [API handling](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0),
	- [CSS](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-5.0) isolation.
- **[Introduction to Blazor - part 2](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Presentation)** (presentation):
  - What is Blazor?
  - Blazor Server & Blazor WebAssembly templates,
  - how Blazor renders content?,
  - How to write performant code for Blazor?
  - How to squeeze from Blazor even more performence?
  - Setting up Blazor application with different render modes,
  - JavaScript Interoperability: from .NET to JS,
  - JavaScript Interoperability: calling JS from .NET,
  - JS interop limitations,
  - Razor components class libraries,
  - what is new for Blazor with .NET 5?
  - The future of Blazor framework.
  
---

## Live demo

Blazor WebAssembly live aplication that was used to present the **Introduction to Blazor - part 1** can be found [here](https://pwrgithub194238.github.io/Capgemini.NET.Blazor).

You can find more about the application in the following [repository directory](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview).

**Note:** API for the application deployed on [GitHub Pages](https://pages.github.com/) is not present as the Blazor WebAssembly app was deployed as a standalone application. Therefor demos from **9** to **12** will not load any data. Regardless, application is fully functional in terms for showing how it is working without needing for it to be compiled.
  
---

## Videos

For both live demo and Blazor presentation the video can be found below. For the live demo only the introductory part is available, technical presentation is recorded in full.

[![Introduction to Blazor - part 1](https://img.youtube.com/vi/NfPPmcrQ81w/0.jpg)](https://youtu.be/NfPPmcrQ81w)
[![Introduction to Blazor - part 2](https://img.youtube.com/vi/Yinamtrous8/0.jpg)](https://youtu.be/Yinamtrous8)
  
---

## Prerequirements

To be able to run through each example You have to have just a [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) installed.
  
---

## Going through examples

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/1.png)

The project that can be found in the folowing [repository directory](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/Demo/Capgemini.Net.Blazor.WebAssembly) was devided into multiple assemblies, when each of the **Capgemini.Net.Blazor.Components.Demo\*** consists of one excercise with the following structure:

```
Capgemini.Net.Blazor.Components.Demo01
├── End # completed exercise
│   ├── ...
│   ...
├── Start # initial files to work on
│   ├── ...
│   ...
├── wwwroot # directory for static files, providing everything that is needed for completing the example
│   ├── ...
│   ...
```

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/2.png)

While the entire application was created to have embedded step-by-step instructions in mind, besides having them avaliable live [here](https://pwrgithub194238.github.io/Capgemini.NET.Blazor), instructions for each excercise is also avaliable to read in the **Markdown** file withing the repository:
- [Demo 01](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/01),
- [Demo 02](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/02),
- [Demo 03](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/03),
- [Demo 04](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/04),
- [Demo 05](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/05),
- [Demo 06](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/06),
- [Demo 07](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/07),
- [Demo 08](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/08),
- [Demo 09](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/09),
- [Demo 10](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/10),
- [Demo 11](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/11),
- [Demo 12](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview/README/12).

**Note:** each of those **README** files consists of converted in-app **HTML** markup supported by the powerfull [PrismJS](https://prismjs.com/) to the **Markup** syntax which is more limited in some ways. For example:
- interactive content was replaced with static images,
- coding snippents don't support copying/line highlighting while the instructions might use those line numbers.

To read more about the application, go to dedicated **[README](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Overview)** file or watch the given **Introduction to Blazor - part 1** [video](https://youtu.be/NfPPmcrQ81w).
  
---

## Presentation

There is no additional pre-requirements for the presentation. During the video, besides slides, source files from the [dotnet/aspnetcore](https://github.com/dotnet/aspnetcore) repository was used. The list of used files is listed below:
- [Mvc/Mvc.TagHelpers/src/ComponentTagHelper.cs](https://github.com/dotnet/aspnetcore/blob/master/src/Mvc/Mvc.TagHelpers/src/ComponentTagHelper.cs),
- [Components/Components/src/Routing/Router.cs](https://github.com/dotnet/aspnetcore/blob/master/src/Components/Components/src/Routing/Router.cs),
- [Components/Components/src/RouteView.cs](https://github.com/dotnet/aspnetcore/blob/master/src/Components/Components/src/RouteView.cs),
- [Components/Web/src/Forms/InputNumber.cs](https://github.com/dotnet/aspnetcore/blob/master/src/Components/Web/src/Forms/InputNumber.cs),
- [Components/Web/src/Forms/InputBase.cs](https://github.com/dotnet/aspnetcore/blob/master/src/Components/Web/src/Forms/InputBase.cs).

Some examples was also presented by using the live application from the part 1.

To read more about the application, go to dedicated **[README](https://github.com/PWrGitHub194238/Capgemini.NET/tree/master/Blazor/Presentation)** file or watch the given **Introduction to Blazor - part 1** [video](https://youtu.be/Yinamtrous8).
