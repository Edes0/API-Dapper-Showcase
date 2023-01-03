namespace Mimbly.Application.Common.Validators.Company;

using FluentValidation;
using Mimbly.Application.Common.Validators.CustomValidatorRules;
using Mimbly.Application.Contracts.Dtos.Company;

public class UpdateCompanyRequestDtoValidator : AbstractValidator<UpdateCompanyRequestDto>
{
    public UpdateCompanyRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(3, 55).WithMessage("Name length is invalid")
            .Must(BeValidLetters.Validate).WithMessage("Name contains invalid characters");
    }
}