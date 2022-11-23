namespace Mimbly.Api.AAD.DTOs;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.CoreServices.Validation;

public class CreateCompanyDTO : CreateCompanyRequestDto
{
    public string? Description { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateCompanyValidator());
    }
}

internal class CreateCompanyValidator : AbstractValidator<CreateCompanyDTO>
{
    public CreateCompanyValidator()
    {
        RuleFor(company => company.Name).NotNull();
        RuleFor(company => company.Description).NotNull();
    }
}
