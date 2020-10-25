using System.ComponentModel.DataAnnotations;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    public class RateContextAttributes : IRateContext
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int MaxRate { get; set; } = 5;

        [Required]
        public string Icon { get; set; } = "fa-star";

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        //[CompareTo(CompareToAttribute.CompareTo.GREAT_THAN_OR_EQUAL, "I")]
        [CompareTo(CompareToAttribute.CompareTo.LESS_THAN_OR_EQUAL, "MaxRate")]
        public int AvgRate { get; set; } = 0;

        //public int I = 2;

    }
}
