namespace EmailService.Validation
{
    /// <summary>
    /// Specifies validation rules for an array of email addresses
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class EmailAddressArrayAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value"><inheritdoc cref="ValidationAttribute.IsValid(object?)" path="/param[@name='value']"/></param>
        /// <param name="validationContext">The context in which a validation check is performed.</param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            string[]? array = value as string[];

            if (array != null)
            {
                if (array.Length == 0)
                {
                    return new ValidationResult("At least one recipient is required.");
                }

                EmailAddressAttribute emailAttribute = new EmailAddressAttribute();

                foreach (string str in array)
                {
                    if (!emailAttribute.IsValid(str))
                    {
                        return new ValidationResult("At least one recipient is not a valid email address.");
                    }
                }

                return ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}