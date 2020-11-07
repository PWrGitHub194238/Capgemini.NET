using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components
{
    public partial class CapOption<TValue>
    {
        [CascadingParameter]
        public CapSelect<TValue> CapSelect { get; set; } = default!;

        [Parameter]
        public string Key { get; set; } = default!;

        [Parameter]
        public TValue Value { get; set; } = default!;

        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            if (Key is null)
            {
                throw new InvalidOperationException($"{nameof(CapOption<TValue>)} requires a {nameof(Key)} parameter.");
            }
        }

        protected override void OnInitialized() =>  CapSelect.AddOption(this);
    }
}
