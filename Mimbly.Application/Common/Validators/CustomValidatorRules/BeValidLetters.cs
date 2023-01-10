namespace Mimbly.Application.Common.Validators.CustomValidatorRules;

using System.Linq;

internal static class BeValidLetters
{
    internal static bool Validate(string name)
    {
        name = name.Replace(" ", "");
        name = name.Replace("-", "");
        return name.All(char.IsLetter);
    }
}
