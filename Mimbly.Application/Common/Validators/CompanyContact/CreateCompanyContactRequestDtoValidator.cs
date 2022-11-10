namespace Mimbly.Application.Common.Validators.CompanyContact;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.CompanyContact;

public class CreateCompanyContactRequestDtoValidator : AbstractValidator<CreateCompanyContactRequestDto>
{
    public CreateCompanyContactRequestDtoValidator()
    {
        RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required");

        RuleFor(x => x.FirstName)
               .NotNull().WithMessage("First name is required");

        RuleFor(x => x.LastName)
               .NotNull().WithMessage("Last name is required");

        RuleFor(x => x.Email)
               .NotNull().WithMessage("Email is required")
               .EmailAddress().WithMessage("Enter in the format: name@example.com");

        RuleFor(x => x.PhoneNumber)
               .NotNull().WithMessage("Phone number is required");

        RuleFor(x => x.CompanyId)
                .NotNull().WithMessage("Company is required");
    }
}
