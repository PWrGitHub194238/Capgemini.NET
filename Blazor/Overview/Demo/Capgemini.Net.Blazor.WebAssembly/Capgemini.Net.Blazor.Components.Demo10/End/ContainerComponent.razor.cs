using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo10.End
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public ILogger<ContainerComponent> Logger { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        [CascadingParameter(Name = "Id")]
        public int ProductId { get; set; }

        public string Endpoint => $"products/{ProductId}";

        public string RateEndpoint => $"{Endpoint}/rate";

        private IRateableProduct? product;

        private string loadingMessage = "Loading product";

        private int? numberOfProducts;

        protected override async Task OnInitializedAsync()
        {
            numberOfProducts = await GetFromJsonAsync<int?>("products/count");

            //if (numberOfProducts.HasValue && !HasProductIdParameterInRoute())
            //{
            //    NavigationManager.NavigateTo(
            //        GetRouteWithRandomProductId(numberOfProducts.Value));
            //}

            //bool HasProductIdParameterInRoute() => Regex.IsMatch(NavigationManager.ToBaseRelativePath(NavigationManager.Uri), @"^demo\d+/\d+");

            //string GetRouteWithRandomProductId(int numberOfProducts)
            //    => Regex.Replace(NavigationManager.ToBaseRelativePath(NavigationManager.Uri), @"demo(\d+)(.*)", $"demo$1/{new Random().Next(1, numberOfProducts)}$2");
        }

        protected override async Task OnParametersSetAsync()
        {
            var rateableProductViewModel = await GetFromJsonAsync<RateableProductViewModel>(Endpoint);

            if (rateableProductViewModel is not null)
            {
                product = new RateableProductAdapter(rateableProductViewModel);
            }

            if (product is null)
            {
                loadingMessage = "Loading error";
            }
        }

        public async Task ChangeProductRate(int productRate)
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(RateEndpoint, productRate);

            if (response.IsSuccessStatusCode)
            {
                var rateableProductViewModel = await response.Content.ReadFromJsonAsync<RateableProductViewModel>();

                if (rateableProductViewModel is not null)
                {
                    product = new RateableProductAdapter(rateableProductViewModel);
                }
            }
        }

        private async Task<TValue?> GetFromJsonAsync<TValue>(string endpoint)
        {
            try
            {
                TValue? result = await HttpClient
                    .GetFromJsonAsync<TValue>(endpoint);
                return result;
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

            return default;
        }
    }
}
