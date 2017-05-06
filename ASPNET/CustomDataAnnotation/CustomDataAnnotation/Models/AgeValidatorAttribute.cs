using System;
using System.ComponentModel.DataAnnotations;

namespace CustomDataAnnotation.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AgeValidatorAttribute : ValidationAttribute
    {
        public int MinimumValue { get; set; }
        public AgeValidatorAttribute(int minimum)
        {
            this.MinimumValue = minimum;
        }

        public override bool IsValid(object value)
        {
            var valueToCompare = (int)value;
            if (valueToCompare > MinimumValue)
                return true;

            return false;
        }
    }
}