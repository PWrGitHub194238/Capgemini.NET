using Capgemini.Net.Blazor.Api.ViewModels;
using System;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Adapters
{
    public class RateableProduct9Adapter : Components.Rate9.Interfaces.IRateableProduct
    {
        private readonly RateableProductViewModel rateableProductViewModel;

        public RateableProduct9Adapter(RateableProductViewModel rateableProductViewModel)
        {
            this.rateableProductViewModel = rateableProductViewModel ?? throw new ArgumentNullException(nameof(rateableProductViewModel));
        }

        public int CurrentRate { get; set; }

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
