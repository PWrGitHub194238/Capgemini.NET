using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Rate12.Interfaces;
using Capgemini.Net.Blazor.WebAssembly.Client.Adapters;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public partial class Component12 : ComponentBase
    {
        [Inject]
        public Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; } = default!;

        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Parameter]
        public int Id { get; set; }

        public int ProductCount { get; set; }

        public IRateableProduct Product { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            ProductCount = await HttpClient.GetFromJsonAsync<int>($"products/count");
        }

        protected override async Task OnParametersSetAsync()
        {
            var rateableProductViewModel = await HttpClient.GetFromJsonAsync<RateableProductViewModel>($"products/{Id}");

            if (rateableProductViewModel is not null)
            {
                Product = new RateableProduct12Adapter(rateableProductViewModel);
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
                    Product = new RateableProduct12Adapter(rateableProductViewModel);
                }
            }
        }

        public static string GetRateIcon(int productRate, int rateRange) => productRate switch
        {
            int low when low <= rateRange / 5 => "fa-angry",
            int middle_low when rateRange / 5 < middle_low && middle_low <= rateRange / 4 => "fa-sad-tear",
            int middle_low when rateRange / 4 < middle_low && middle_low <= rateRange / 3 => "fa-meh-blank",
            int middle_low when rateRange / 3 < middle_low && middle_low <= rateRange / 2 => "fa-smile-beam",
            int middle_low when rateRange / 2 < middle_low && middle_low <= rateRange / 1 => "fa-grin-stars",
            _ => "fa-grin-stars",
        };
    }
}