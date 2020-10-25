using Microsoft.AspNetCore.Components;

namespace Capgemini.Net.Blazor.Components
{
    public partial class CapCheckbox
    {
        [Parameter]
        public bool State { get; set; }

        [Parameter]
        public string Label { get; set; } = default!;

        [Parameter]
        public EventCallback<bool> StateChanged { get; set; }

        internal bool IsDone
        {
            get => State;
            set
            {
                State = value;
                StateChanged.InvokeAsync(State);
            }
        }
    }
}
