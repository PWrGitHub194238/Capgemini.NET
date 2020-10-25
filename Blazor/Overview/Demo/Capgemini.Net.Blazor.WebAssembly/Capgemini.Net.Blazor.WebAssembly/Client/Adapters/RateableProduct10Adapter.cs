using Capgemini.Net.Blazor.Api.ViewModels;
using System;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Adapters
{
    public class RateableProduct10Adapter : Components.Rate10.Interfaces.IRateableProduct
    {
        private readonly RateableProductViewModel rateableProductViewModel;

        public RateableProduct10Adapter(RateableProductViewModel rateableProductViewModel)
        {
            this.rateableProductViewModel = rateableProductViewModel ?? throw new ArgumentNullException(nameof(rateableProductViewModel));
        }

        public int? CurrentRate
        {
            get => rateableProductViewModel.CurrentRate;
            set => rateableProductViewModel.CurrentRate = value;
        }

        public decimal AverageRate {
            get => rateableProductViewModel.AverageRate;
            set => rateableProductViewModel.AverageRate = value;
        }

        public int MinRate
        {
            get => rateableProductViewModel.MinRate;
            set => rateableProductViewModel.MinRate = value;
        }

        public int MaxRate
        {
            get => rateableProductViewModel.MaxRate;
            set => rateableProductViewModel.MaxRate = value;
        }
    }
}
