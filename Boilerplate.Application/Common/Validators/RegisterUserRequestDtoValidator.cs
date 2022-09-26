namespace Boilerplate.Application.Common.Validators;

using Boilerplate.Application.Contracts.Requests.Identity;
using FluentValidation;

public class RegisterUserRequestDtoValidator : AbstractValidator<RegisterUserRequestDto>
{
    public RegisterUserRequestDtoValidator()
    {
        RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(255).MinimumLength(8).WithMessage("Password needs to have between 8 and 255 characters");
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Please provide a valid email");
        RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Last name is required.");
    }
}