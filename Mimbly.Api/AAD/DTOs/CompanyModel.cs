namespace Mimbly.Api.AAD.DTOs;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.CoreServices.Validation;

public class CompanyModel : CreateCompanyRequestDto
{
    public string? Description { get; set; }

    public new async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateCompanyValidator());
    }
}

internal class CreateCompanyValidator : AbstractValidator<CompanyModel>
{
    public CreateCompanyValidator()
    {
        RuleFor(company => company.Name).NotNull();
        RuleFor(company => company.Description).NotNull();
    }
}
