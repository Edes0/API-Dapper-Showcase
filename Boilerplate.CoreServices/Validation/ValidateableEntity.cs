namespace Boilerplate.CoreServices.Validation;
using System.Linq;
using System.Threading.Tasks;
using Boilerplate.CoreServices.Exceptions;
using FluentValidation;

public static class ValidatableEntity
{
    public static async Task ValidateEntityByFluentRules<T>(T toValidate, AbstractValidator<T> validator)
    {
        var validationResults = await validator.ValidateAsync(toValidate);

        if (validationResults.IsValid)
        {
            return;
        }

        throw new BadRequestException(
            validationResults.Errors.Aggregate(string.Empty, (current, error) => current + $"{error.PropertyName} - {error.ErrorMessage} \n"));
    }
}