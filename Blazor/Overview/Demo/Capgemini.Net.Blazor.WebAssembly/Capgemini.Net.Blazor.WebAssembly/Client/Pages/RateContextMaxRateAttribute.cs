using System.ComponentModel.DataAnnotations;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public class RateContextMaxRateAttribute : IRateContext
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int MaxRate { get; set; } = 5;

        public string Icon { get; set; } = "fa-star";

        public int AvgRate { get; set; } = 0;

    }
}
