namespace Mimbly.Api.AAD.DTOs;

using Mimbly.CoreServices.Validation;
using FluentValidation;

public class CreateCompanyDTO
{
    public string? CompanyName { get; set; }
    public string? Description { get; set; }
    public string? ParentId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateCompanyValidator());
    }
}

internal class CreateCompanyValidator : AbstractValidator<CreateCompanyDTO>
{
    public CreateCompanyValidator()
    {
        RuleFor(company => company.CompanyName).NotNull().NotEmpty();
        RuleFor(company => company.Description).NotNull().NotEmpty();
        RuleFor(company => company.ParentId).NotNull().NotEmpty();
    }
}