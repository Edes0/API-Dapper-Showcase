namespace Mimbly.CoreServices.Validation;

using System.Text.RegularExpressions;

public static class Validator
{
    /// <summary>
    /// Return true if the email is a valid email, false otherwise.
    /// </summary>
    /// <param name="email">The email to validate</param>
    /// <returns>True if this is a valid email, false otherwise.</returns>
    public static bool IsValidEmail(string email)
    {
        // http://stackoverflow.com/a/201336 with optional root domain (name@domain is valid)
        return !string.IsNullOrWhiteSpace(email)
               && Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.?\w+([-.]\w+)*$");
    }
}