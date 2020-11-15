using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public abstract class RateInputMinMaxNumberAbstractBase<TValue> : RateInputNumberAbstractBase<TValue>
    {
        [Parameter]
        public TValue Min { get; set; } = default!;

        [Parameter]
        public TValue Max { get; set; } = default!;

        protected override void OnParametersSet()
        {
            if (Min is not null && !typeof(IComparable<TValue>).IsAssignableFrom(Min.GetType()))
            {
                throw new InvalidOperationException($"{nameof(RateInputMinMaxNumberAbstractBase<TValue>)} requires a {nameof(Min)} " +
                    $"parameter to implement {nameof(IComparable<TValue>)} and it is of type {typeof(TValue)}.");
            }

            if (Max is not null && !typeof(IComparable<TValue>).IsAssignableFrom(Max.GetType()))
            {
                throw new InvalidOperationException($"{nameof(RateInputMinMaxNumberAbstractBase<TValue>)} requires a {nameof(Max)} " +
                    $"parameter to implement {nameof(IComparable<TValue>)} and it is of type {typeof(TValue)}.");
            }

            if (Min is not null && Max is not null && (Min as IComparable<TValue>)!.CompareTo(Max) > 0)
            {
                throw new InvalidOperationException($"{nameof(RateInputMinMaxNumberAbstractBase<TValue>)} requires a {nameof(Min)} " +
                    $"parameter to be less or equal than {nameof(Max)} parameter.");
            }
        }

        protected override bool TryParseValueFromString(
            string? value,
            [MaybeNullWhen(false)] out TValue result,
            [NotNullWhen(false)] out string? validationErrorMessage)
        {
            if (base.TryParseValueFromString(value, out result, out validationErrorMessage))
            {
                IComparable<TValue>? selfValueComparer = result as IComparable<TValue>;

                if (selfValueComparer is not null)
                {
                    if (Min is not null && selfValueComparer.CompareTo(Min) < 0)
                    {
                        result = Min;
                    }

                    if (Max is not null && selfValueComparer.CompareTo(Max) > 0)
                    {
                        result = Max;
                    }
                }

                return true;
            }

            return false;
        }
    }
}
