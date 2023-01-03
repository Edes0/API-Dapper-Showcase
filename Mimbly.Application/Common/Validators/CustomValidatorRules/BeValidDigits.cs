namespace Mimbly.Application.Common.Validators.CustomValidatorRules;

using System.Linq;

internal static class BeValidDigits
{
    internal static bool Validate(string digits)
    {
        digits = digits.Replace(" ", "");
        digits = digits.Replace("-", "");
        return digits.All(char.IsDigit);
    }
}
