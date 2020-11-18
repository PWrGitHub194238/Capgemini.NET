using System;
using System.ComponentModel.DataAnnotations;

namespace Capgemini.Net.Blazor.Components.Demo.Examples.Demo08.End
{
    public class RateContext
    {
        [Required]
        [Range(2, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int MaxRate { get; set; } = 6;
    }
}
