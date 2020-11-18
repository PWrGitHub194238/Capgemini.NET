using Capgemini.Net.Blazor.Api.ViewModels;
using Capgemini.Net.Blazor.Components.Demo.Interfaces;
using System;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public class RateableProductAdapter : IRateableProduct
    {
        private readonly RateableProductViewModel rateableProductViewModel;

        public RateableProductAdapter(RateableProductViewModel rateableProductViewModel)
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
