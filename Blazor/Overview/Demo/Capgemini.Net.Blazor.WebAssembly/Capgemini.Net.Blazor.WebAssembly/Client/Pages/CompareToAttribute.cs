using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Capgemini.Net.Blazor.WebAssembly.Client.Pages
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class CompareToAttribute : ValidationAttribute
    {
        private readonly CompareTo comparer;
        private readonly string otherProperty;

        public CompareToAttribute(CompareTo comparer, string otherProperty)
        {
            this.comparer = comparer;
            this.otherProperty = otherProperty;
            ErrorMessage ??= GetDefaultErrorMessage();
        }

        private string GetDefaultErrorMessage()
        {
            return comparer switch
            {
                CompareTo.LESS_THAN => "The field {0} must be less than {1}.",
                CompareTo.EQUAL => "The field {0} must be equal to {1}.",
                CompareTo.GREATER_THAN => "The field {0} must be greater than {1}.",
                CompareTo.LESS_THAN_OR_EQUAL => "The field {0} must be less than or equal to {1}.",
                CompareTo.GREAT_THAN_OR_EQUAL => "The field {0} must be greater than or equal to {1}.",
                _ => string.Empty,
            };
        }

        public override string FormatErrorMessage(string name) => string.Format(ErrorMessage!, name, otherProperty);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
            Type valueType = value.GetType();

            if (!typeof(IComparable).IsAssignableFrom(valueType))
            {
                throw new ArgumentException(string.Format(DataAnnotationsResources.CompareToAttribute_Uncomparable, valueType));
            }
            
            PropertyInfo? otherPropertyInfo = validationContext.ObjectType.GetProperty(otherProperty);
            if (otherPropertyInfo is null)
            { 
                return new ValidationResult(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        DataAnnotationsResources.CompareAttribute_UnknownProperty,
                        otherProperty),
                    new[] { validationContext.MemberName! });
            }

            object? otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

                if (otherPropertyValue is not null)
                {
                    Type otherPropertyValueType = otherPropertyValue.GetType();

                    if (!typeof(IComparable).IsAssignableFrom(otherPropertyValueType))
                    {
                        throw new ArgumentException(string.Format(DataAnnotationsResources.CompareToAttribute_Uncomparable, otherPropertyValueType));
                    }

                    if (valueType != otherPropertyValueType || !otherPropertyValueType.IsAssignableFrom(valueType))
                    {
                        throw new ArgumentException(string.Format(DataAnnotationsResources.CompareToAttribute_UncomparableTypes, valueType, otherPropertyValueType));
                    }

                    IComparable? selfValueComparer = value as IComparable;

                    if (selfValueComparer is not null)
                    {
                        int compareResult = selfValueComparer.CompareTo(otherPropertyValue);

                        if (comparer.Equals(CompareTo.LESS_THAN_OR_EQUAL))
                        {
                            if (compareResult > (int)CompareTo.EQUAL)
                            {
                                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName! });
                            }
                        }
                        else if (comparer.Equals(CompareTo.GREAT_THAN_OR_EQUAL))
                        {
                            if (compareResult < (int)CompareTo.EQUAL)
                            {
                                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName! });
                            }
                        }
                        else if (compareResult != (int)comparer)
                        {
                            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName! });
                        }
                    }
                }
            }

            return null;
        }

        private static class DataAnnotationsResources
        {
            public const string CompareAttribute_UnknownProperty = "Could not find a property named {0}.";
            public const string CompareToAttribute_Uncomparable = "Could not use the [CompareToAttribute] for a property named {0} that does not implement IComparable interface.";
            public const string CompareToAttribute_UncomparableTypes = "Could not use the [CompareToAttribute] for a different types (cannot compare {0} with {1}).";
        }

        public enum CompareTo
        {
            LESS_THAN = -1,
            EQUAL = 0,
            GREATER_THAN = 1,
            LESS_THAN_OR_EQUAL = 2,
            GREAT_THAN_OR_EQUAL = 3,
        }
    }
}
