using Microsoft.AspNetCore.Components.Forms;
using System;

namespace Capgemini.Net.Blazor.Components.Demo
{
    public abstract class RateInputNumberAbstractBase<TValue> : InputNumber<TValue>, IDisposable
    {
        protected override void OnInitialized()
        {
            EditContext.OnFieldChanged += HandleFieldChanged;
            base.OnInitialized();
        }

        abstract protected void HandleFieldChanged(object? sender, FieldChangedEventArgs e);

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EditContext.OnFieldChanged -= HandleFieldChanged;
            }
        }
    }
}
