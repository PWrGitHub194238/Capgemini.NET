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

        public string BaseHref => Regex.Replace(CurrentUrl, @"demo(\d+)(.*)", $"demo$1/{ProductId}");

        public string Href => Regex.IsMatch(CurrentUrl, @"^demo\d+/\d+")
            ? Regex.Replace(CurrentUrl, @"demo(\d+)/\d+", $"demo$1/{ProductId}")
            : Regex.Replace(CurrentUrl, @"demo(\d+)(.*)", $"demo$1/{ProductId}$2");

        private void Navigate() => NavigationManager.NavigateTo(Href);
    }
}
