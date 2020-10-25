using Capgemini.Net.Blazor.Components.Rate10.Base;
using System;

namespace Capgemini.Net.Blazor.Components.Rate10
{
    public partial class Rate : RateBase
    {

        protected override bool ShouldRender()
        {
            Console.WriteLine("ShouldRender");
            return base.ShouldRender();
        }
    }
}
