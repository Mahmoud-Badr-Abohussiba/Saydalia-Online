using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class EgyptianPhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var phone = value as string;

        if (string.IsNullOrEmpty(phone))
            return ValidationResult.Success;

        var regex = new Regex(@"^(\+20|20|01)[0125]\d{8}$");


        if (!regex.IsMatch(phone))
            return new ValidationResult("Please enter a valid Egyptian phone number.");

        return ValidationResult.Success;
    }
}