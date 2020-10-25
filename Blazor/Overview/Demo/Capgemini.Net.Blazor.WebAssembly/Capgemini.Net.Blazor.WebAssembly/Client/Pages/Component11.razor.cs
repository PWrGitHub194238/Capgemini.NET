using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Rate11.Interfaces;
using Capgemini.Net.Blazor.WebAssembly.Client.Adapters;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public partial class Component11 : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Parameter]
        public int Id { get; set; }

        public IRateableProduct Product { get; set; } = default!;

        protected override async Task OnParametersSetAsync()
        {
            var rateableProductViewModel = await HttpClient.GetFromJsonAsync<RateableProductViewModel>($"products/{Id}");

            if (rateableProductViewModel is not null)
            {
                Product = new RateableProduct11Adapter(rateableProductViewModel);
            }
        }

        public async Task ChangeProductRate(int productRate)
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync($"products/{Id}/rate", productRate);

            if (response.IsSuccessStatusCode)
            {
                var rateableProductViewModel = await response.Content.ReadFromJsonAsync<RateableProductViewModel>();

                if (rateableProductViewModel is not null)
                {
                    Product = new RateableProduct11Adapter(rateableProductViewModel);
                }
            }
        }
    }
}
