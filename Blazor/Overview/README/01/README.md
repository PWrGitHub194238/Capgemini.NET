![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/01/Summary.jpg)

# Rate component: from JavaScript through TypeScript to Blazor

The below **README.md** file contains a transcript of the descriptions that can be found in the checklist for each of the exercise\`s points built into the Blazor WebAssembly application on-line ([link](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/)). You can directly go to that descriptions by navigating to any of those links:
 - [copy HTML markup with already provided JavaScript calls](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo01/checklist/1),
 - [migrate TypeScript code directly into Razor Component](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo01/checklist/2),
 - [change DOM event handlers to Blazor event handlers](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo01/checklist/3),
 - [use one-way binding to dynamically update class attribute value](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo01/checklist/4),
 - [deal with lambda variable capturing, causing each rate icon to represent max rate](https://pwrgithub194238.github.io/Capgemini.NET.Blazor/demo01/checklist/5).

### Copy HTML markup with already provided JavaScript calls

Open a file `./Start/RateComponent.razor` Razor component. Its content should resemble snippet that is shown below - this is a default markup provided to the newly created Razor component:

```
<h3>RateComponent</h3>

@code {

}
```

Copy over the entire **HTML** markup and replace the `./Start/RateComponent.razor` content with it:

```
@for (int i = 0; i < 5; i += 1)
{
    <i class="rate-icon far fa-star cursor-pointer"
        onclick="CapgeminiNetBlazorComponents.setRate()"
        onmouseover="@("CapgeminiNetBlazorComponents.showRate(" + i + ")")"
        onmouseout="CapgeminiNetBlazorComponents.revertRate()"></i>
}

@code {

}
```

the above code uses **DOM** events to call **JavaScript** functions (within the `CapgeminiNetBlazorComponents` namespace):


- `showRate(i)` - based on the index of the `<i>` tag hovered, function is used to internally store the given index to mark all previous `<i>` tags as active. Function is trigger on `mouseover` **DOM** event.
- `setRate()` - based on the index of the `<i>` tag currently hovered, if the `<i>` is clicked, the index stored by the `showRate(i)` function is marked as the actual selected rate.
- `revertRate()` - on `mouseout` **DOM** event it reverts the temporary changes made by the `onmouseover` event (by replacing the stored index provided by the `showRate(i)` function with the value stored by the `setRate()`).

Actual **TypeScript** code that was used can be viewed in the `./wwwroot/js/main.ts` file:

```
public rate: number = 0;
public tempRate: number = 0;

constructor() {
	this.updateRateIconActiveState();
}

public updateRateIconActiveState() {
	const rateIcons = document.getElementsByClassName('rate-icon');

	for (let i: number = 0; i < rateIcons.length; i += 1) {
		const rateIcon = rateIcons.item(i);

		if (this.isActive(i)) {
			rateIcon.classList.add(Demo01.ACTIVE_STYLE);
			rateIcon.classList.remove(Demo01.INACTIVE_STYLE);
		} else {
			rateIcon.classList.remove(Demo01.ACTIVE_STYLE);
			rateIcon.classList.add(Demo01.INACTIVE_STYLE);
		}
	}
}

public setRate() {
	this.rate = this.tempRate;
	this.updateRateIconActiveState();
}

public showRate(index: number) {
	this.tempRate = index;
	this.updateRateIconActiveState();
}

public revertRate() {
	this.tempRate = this.rate;
	this.updateRateIconActiveState();
}

private isActive(index: number): boolean {
	return index <= this.tempRate;
}
```

`updateRateIconActiveState()` function is used to update state of all `<i>` elements based on the `tempRate` value (which is either set to the selected `rate` or index of the currently hovered `<i>` tag if any is hovered).

### Migrate TypeScript code directly into Razor Component

In order to migrate the **TypeScript** logic that `./wwwroot/js/main.ts` file contains:

- copy entire content of the `Capgemini.Net.Blazor.Components.Demo01` **TypeScript** class,
- paste it into `./Start/RateComponent.razor` file, wrapping it with the `@code {` and `}` syntax which will mark a given code as a [Razor code block](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#no-locrazor-mark-blocks) (in [Razor components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0) use `@mark` over `@functions` to add **C#** members - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0)),
- remove class constructor. Razor components don't allow to define custom constructors - it will be [auto-generated](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#inspect-the-no-locrazor-c-class-generated-for-a-view),
- remove `updateRateIconActiveState` function - its logic will be implemented by Razor syntax,
- replace **TypeScript** variable types syntax with **C#** syntax,
- replace **TypeScript** function return types syntax with **C#** syntax,
- rename **TypeScript** functions from **kamelCase** to **PascalCase** **C#** syntax,
- **__(optionally)__** remove `this.` qualification,
- **__(optionally)__** replace `block body` with `expression body` for one-liner methods ([read more](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members)).

The resulted code should be similar to one given below:

```
@code {
    private static string ACTIVE_STYLE = "fas";
    private static string INACTIVE_STYLE = "far";

    public int rate = 0;
    public int tempRate = 0;

    public void SetRate() => rate = tempRate;

    public void ShowRate(int index) => tempRate = index;

    public void RevertRate() => tempRate = rate;

    private bool IsActive(int index) => index <= tempRate;
}
```

### Change DOM event handlers to Blazor event handlers

Blazor framework supports event handling by using a `@on{EVENT}` markup ([read more](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-5.0#onevent)).

`{EVENT}` can be any of the well-known **DOM event handlers**. To shift from those events to Razor component markup change:

- `onclick="CapgeminiNetBlazorComponents.setRate()"` to `@onclick="SetRate"`,
- `onmouseover="@("CapgeminiNetBlazorComponents.showRate(" + i + ")")"` to `@onmouseover="@(() => ShowRate(i))"`,
- `onmouseout="CapgeminiNetBlazorComponents.revertRate()"` to `@onmouseout=RevertRate`.

Each of those event handlers will trigger the method that can be given as a value of the event handler in several ways:


- by name (`@onclick="SetRate"`) - to make that markup to work, the method `SetRate` have to receive as parameters same argument types as `@onclick` provides (in this case `MouseEventArgs` - [read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/event-handling?view=aspnetcore-5.0#event-argument-types)),
- by [lambda expression](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions) (`@onmouseover="@(() => ShowRate(i))"`) when we need to provide other set of arguments or provide same argument explicitly ([read more](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/event-handling?view=aspnetcore-5.0#lambda-expressions)),
- by name without parentheses. In the same manner as regular **HTML** attribute values can be given - `@onmouseout=RevertRate`.

### Use one-way binding to dynamically update class attribute value

By replacing the **DOM** event handlers with Razor component syntax and `C#` code we didn't copy the function that was responsible for shifting back and forth two classes: `fas` (defined as the `ACTIVE_STYLE` constant and `far` (`INACTIVE_STYLE` (those classes are part of the [Font Awesome](https://fontawesome.com) **CSS**-based icon library):

![](https://github.com/PWrGitHub194238/Capgemini.NET/blob/master/Blazor/Overview/README/01/img/1.jpg)

Highlighted lines from `7` to `13` were responsible for making Font Awesome icons to change style, based on the `this.isActive(i)` result:

```
public updateRateIconActiveState() {
    const rateIcons = document.getElementsByClassName('rate-icon');

    for (let i: number = 0; i < rateIcons.length; i += 1) {
        const rateIcon = rateIcons.item(i);

        if (this.isActive(i)) {
            rateIcon.classList.add(Demo01.ACTIVE_STYLE);
            rateIcon.classList.remove(Demo01.INACTIVE_STYLE);
        } else {
            rateIcon.classList.remove(Demo01.ACTIVE_STYLE);
            rateIcon.classList.add(Demo01.INACTIVE_STYLE);
        }
    }
}
```

In order to bring the same functionality back to live, all that is need to be done is a use of Razor specific syntax called `one-way binding` that will calculate the `IsActive(i)` value and based on that value will decide which style should be applied to the component like so ([read more](https://blazor-university.com/components/one-way-binding)):

```
<i class="@(IsActive(i) ? ACTIVE_STYLE : INACTIVE_STYLE) fa-star cursor-pointer"
    @onclick="SetRate"
    @onmouseover="@(() => ShowRate(i))"
    @onmouseout=RevertRate></i>
```

That one line of code conditionally applies to the `class` attribute's value either `fas` or `far` class.

### Deal with lambda variable capturing, causing each rate icon to represent max rate

As the lambda expression `@(() => ShowRate(i))` was used as a delegate-typed value for `@onmouseover` event handler to be resolved upon `mouseover` **DOM** event, the variable `i` was [captured](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#capture-of-outer-variables-and-variable-scope-in-lambda-expressions) by the expression.

Lambda expressions/statements capture variables by reference, effectively extend their lifetime beyond defined scope, not allowing them to be garbage collected until they keep the reference to them. The side effect of this behavior is that the `@for (int i = 0; i < 5; i += 1)` loop will generate `5` `<i>` elements, each calling `ShowRate(5)`.

To resolve the issue, the separate variable has to be defined within a lifetime of the loop (line `3`), so each time code makes another pass,  new variable will be created and its reference will be kept by lambda expression (line `6`):

```
@for (int i = 0; i < 5; i += 1)
{
    int index = i;
    <i class="@(IsActive(i) ? ACTIVE_STYLE : INACTIVE_STYLE) fa-star cursor-pointer"
        @onclick="SetRate"
        @onmouseover="@(() => ShowRate(index))"
        @onmouseout=RevertRate></i>
}
```