using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public partial class ProductButton
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Parameter]
        public int ProductId { get; set; }

        public char CharId => char.Parse(ProductId.ToString());

        public string CurrentUrl => NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

        public string Href => Regex.IsMatch(CurrentUrl, @".*\/\d+$") ? Regex.Replace(CurrentUrl, @"/\d+$", $"/{ProductId}") : $"{CurrentUrl}/{ProductId}";
    }
}
