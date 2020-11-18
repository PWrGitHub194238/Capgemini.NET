using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Demo10.End
{
    public partial class ContainerComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = default!;

        [Inject]
        public ILogger<ContainerComponent> Logger { get; set; } = default!;

        [Parameter]
        public RenderFragment<IRateableProduct> ChildContent { get; set; } = default!;

        [CascadingParameter(Name = "Id")]
        public int ProductId { get; set; }

        public string Endpoint => $"products/{ProductId}";

        private IRateableProduct? product;

        private string loadingMessage = "Loading product";

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var rateableProductViewModel = await HttpClient
                    .GetFromJsonAsync<RateableProductViewModel>(Endpoint);

                if (rateableProductViewModel is not null)
                {
                    product = new RateableProductAdapter(rateableProductViewModel);
                }
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
            finally
            {
                if (product is null)
                {
                    loadingMessage = "Loading error";
                }
            }
        }
    }
}
