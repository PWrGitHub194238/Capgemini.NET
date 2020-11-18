using Capgemini.Net.Blazor.Components.Demo;
using System.ComponentModel.DataAnnotations;

namespace Capgemini.Net.Blazor.Components.Demo08.End
{
    public class RateContext
    {
        public static readonly string[] Icons = {
            "fa-star",
            "fa-grin-stars",
            "fa-angry",
            "fa-sun"
        };

        [Required]
        [Range(2, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int MaxRate { get; set; } = 6;

        [Required]
        [Range(2, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        [CompareTo(CompareToAttribute.CompareTo.LESS_THAN_OR_EQUAL, "MaxRate")]
        public int AvgRate { get; set; } = 3;

        public string Icon { get; set; } = Icons[0];
    }
}
