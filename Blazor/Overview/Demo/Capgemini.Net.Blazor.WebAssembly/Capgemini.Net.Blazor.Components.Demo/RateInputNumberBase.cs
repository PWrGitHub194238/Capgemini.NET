using Microsoft.AspNetCore.Components.Forms;
using System;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public class RateInputNumberBase<TValue> : InputNumber<TValue>
    {
        protected void OnSelectedFieldChanged<T>(string fieldName, FieldChangedEventArgs args, Action<T, FieldIdentifier> action)
            where T : class
        {
            if (args.FieldIdentifier.FieldName.Equals(fieldName) && EditContext.IsModified(args.FieldIdentifier))
            {
                T? model = EditContext.Model as T;

                if (model is not null)
                {
                    action.Invoke(model, args.FieldIdentifier);
                }
            }
        }
    }
}
