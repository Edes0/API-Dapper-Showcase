namespace Mimbly.Application.Common.Validators.AD;

using Contracts.Dtos.AD;
using FluentValidation;

public class AddCompanyRequestDtoValidator : AbstractValidator<AddCompanyRequestDto>
{
    public AddCompanyRequestDtoValidator()
    {
        RuleFor(company => company.Name).NotEmpty();
        RuleFor(company => company.Description).NotEmpty();
        RuleFor(company => company.ParentId).NotEmpty();
    }
}