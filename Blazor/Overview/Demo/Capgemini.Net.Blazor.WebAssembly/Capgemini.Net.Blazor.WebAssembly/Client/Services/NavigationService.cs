using Capgemini.Net.Blazor.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Services
{
    public sealed class NavigationService : INavigationService, IDisposable
    {
        private static readonly string DEMO = "demo";
        private static readonly string PRODUCT_ID = "productId";
        private static readonly string CHECKLIST = "checklist";
        private static readonly string CHECKLIST_POINT_DESCRIPTION_INDEX = "checklistPoint";

        private static readonly string demoRegex = @"demo0*\d+";
        private static readonly string productIdRegex = @"\d+";
        private static readonly string checklistRegex = @"checklist";
        private static readonly string checklistPointIdxRegex = @"\d+";

        private static readonly Regex demoUrl = new Regex(
            $"^(?<{DEMO}>{demoRegex})$",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex demoWithChecklistUrl = new Regex(
            $"^(?<{DEMO}>{demoRegex})/(?<{CHECKLIST}>{checklistRegex})$",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex demoWithChecklistPointUrl = new Regex(
            $"^(?<{DEMO}>{demoRegex})/(?<{CHECKLIST}>{checklistRegex})/(?<{CHECKLIST_POINT_DESCRIPTION_INDEX}>{checklistPointIdxRegex})$",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex demoWithProductIdUrl = new Regex(
            $"^(?<{DEMO}>{demoRegex})/(?<{PRODUCT_ID}>{productIdRegex})$",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex demoWithProductIdWithChecklistUrl = new Regex(
            $"^(?<{DEMO}>{demoRegex})/(?<{PRODUCT_ID}>{productIdRegex})/(?<{CHECKLIST}>{checklistRegex})$",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex demoWithProductIdWithChecklistPointUrl = new Regex(
            $"^(?<{DEMO}>{demoRegex})/(?<{PRODUCT_ID}>{productIdRegex})/(?<{CHECKLIST}>{checklistRegex})/(?<{CHECKLIST_POINT_DESCRIPTION_INDEX}>{checklistPointIdxRegex})$",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        private readonly NavigationManager navigationManager;

        private static IDictionary<string, string> navigationDictionary = new Dictionary<string, string>();

        public string CurrentUrl => navigationManager.ToBaseRelativePath(navigationManager.Uri);

        public NavigationService(NavigationManager NavigationManager)
        {
            navigationManager = NavigationManager ?? throw new ArgumentNullException(nameof(NavigationManager));
            navigationManager.LocationChanged += NavigationManager_LocationChanged;
            DecomposeRouteUrl();
        }

        public void ChangeProduct(int productId)
        {
            if (navigationDictionary.ContainsKey(DEMO))
            {
                string url = $"/{navigationDictionary[DEMO]}/{productId}";

                if (navigationDictionary.ContainsKey(CHECKLIST) && navigationDictionary.ContainsKey(CHECKLIST_POINT_DESCRIPTION_INDEX))
                {
                    url += $"/{navigationDictionary[CHECKLIST]}/{navigationDictionary[CHECKLIST_POINT_DESCRIPTION_INDEX]}";
                }
                else if (navigationDictionary.ContainsKey(CHECKLIST))
                {
                    url += $"/{navigationDictionary[CHECKLIST]}";
                }

                navigationManager.NavigateTo(url);
            }
        }

        public void CloseSideNav()
        {
            if (navigationDictionary.ContainsKey(DEMO))
            {
                string url = $"/{navigationDictionary[DEMO]}";

                if (navigationDictionary.ContainsKey(PRODUCT_ID))
                {
                    url += $"/{navigationDictionary[PRODUCT_ID]}";
                }

                navigationManager.NavigateTo(url);
            }
        }

        public void CloseSubSideNav()
        {
            if (navigationDictionary.ContainsKey(DEMO))
            {
                string url = $"/{navigationDictionary[DEMO]}";

                if (navigationDictionary.ContainsKey(PRODUCT_ID) && navigationDictionary.ContainsKey(CHECKLIST))
                {
                    url += $"/{navigationDictionary[PRODUCT_ID]}/{navigationDictionary[CHECKLIST]}";
                }
                else if (navigationDictionary.ContainsKey(CHECKLIST))
                {
                    url += $"/{navigationDictionary[CHECKLIST]}";
                }

                navigationManager.NavigateTo(url);
            }
        }

        public void OpenSideNav()
        {
            if (navigationDictionary.ContainsKey(CHECKLIST_POINT_DESCRIPTION_INDEX))
            {
                OpenSubSideNav(int.Parse(navigationDictionary[CHECKLIST_POINT_DESCRIPTION_INDEX]));
            }
            else if (navigationDictionary.ContainsKey(DEMO))
            {
                string url = $"/{navigationDictionary[DEMO]}";

                if (navigationDictionary.ContainsKey(PRODUCT_ID))
                {
                    url += $"/{navigationDictionary[PRODUCT_ID]}";
                }

                url += $"/{CHECKLIST}";

                navigationManager.NavigateTo(url);
            }
        }

        public void OpenSubSideNav(int checklistPointIndex)
        {
            if (navigationDictionary.ContainsKey(DEMO))
            {
                string url = $"/{navigationDictionary[DEMO]}";

                if (navigationDictionary.ContainsKey(PRODUCT_ID))
                {
                    url += $"/{navigationDictionary[PRODUCT_ID]}";
                }

                if (navigationDictionary.ContainsKey(CHECKLIST))
                {
                    url += $"/{navigationDictionary[CHECKLIST]}/{checklistPointIndex}";
                }

                navigationManager.NavigateTo(url);
            }
        }

        public bool ShouldOpenSideNav() => navigationDictionary.ContainsKey(CHECKLIST);

        public bool ShouldOpenSubSideNav(int checklistPointIndex)
            => navigationDictionary.ContainsKey(CHECKLIST_POINT_DESCRIPTION_INDEX)
                && checklistPointIndex == int.Parse(navigationDictionary[CHECKLIST_POINT_DESCRIPTION_INDEX]);

        public void Dispose()
        {
            navigationManager.LocationChanged -= NavigationManager_LocationChanged;
        }

        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e) => DecomposeRouteUrl();

        private void DecomposeRouteUrl()
        {
            navigationDictionary = new Dictionary<string, string>();

            foreach (var kvp in MatchRegex(demoUrl))
            {
                navigationDictionary[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in MatchRegex(demoWithChecklistUrl))
            {
                navigationDictionary[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in MatchRegex(demoWithChecklistPointUrl))
            {
                navigationDictionary[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in MatchRegex(demoWithProductIdUrl))
            {
                navigationDictionary[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in MatchRegex(demoWithProductIdWithChecklistUrl))
            {
                navigationDictionary[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in MatchRegex(demoWithProductIdWithChecklistPointUrl))
            {
                navigationDictionary[kvp.Key] = kvp.Value;
            }
        }

        private IEnumerable<KeyValuePair<string, string>> MatchRegex(Regex regex)
        {
            Match match = regex.Match(CurrentUrl);

            if (match.Success)
            {
                foreach (var matchKey in match.Groups.Keys)
                {
                    yield return new KeyValuePair<string, string>(matchKey, match.Groups[matchKey].Value);
                }
            }
        }
    }
}
