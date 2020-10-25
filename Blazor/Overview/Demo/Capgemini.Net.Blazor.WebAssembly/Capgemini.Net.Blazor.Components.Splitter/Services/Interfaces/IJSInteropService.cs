using Capgemini.Net.Blazor.Components.Splitter.Base;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Capgemini.Net.Blazor.Components.Splitter.Services.Interfaces
{
    public interface IJSInteropService
    {
        ValueTask<DOMRect> GetBoundingClientRect(ElementReference containerRef);
    }
}
