namespace Mimbly.Application.Common.Validators.CompanyContact;

using FluentValidation;
using Mimbly.Application.Common.Validators.CustomValidatorRules;
using Mimbly.Application.Contracts.Dtos.CompanyContact;

public class CreateCompanyContactRequestDtoValidator : AbstractValidator<CreateCompanyContactRequestDto>
{
    public CreateCompanyContactRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(3, 55).WithMessage("Title length is invalid")
            .Must(BeValidLetters.Validate).WithMessage("Title contains invalid characters");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(3, 55).WithMessage("First name length is invalid")
            .Must(BeValidLetters.Validate).WithMessage("First name contains invalid characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(3, 55).WithMessage("Last name length is invalid")
            .Must(BeValidLetters.Validate).WithMessage("Last name contains invalid characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Enter in the format: name@example.com");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Length(3, 55).WithMessage("Phone number length is invalid")
            .Must(BeValidDigits.Validate).WithMessage("Phone number contains invalid characters");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Company is required");
    }
}