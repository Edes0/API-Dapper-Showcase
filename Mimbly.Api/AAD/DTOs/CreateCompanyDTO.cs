namespace Mimbly.Api.AAD.DTOs;

using Mimbly.CoreServices.Validation;
using FluentValidation;

public class CreateCompanyDTO
{

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string CompanyName { get; set; }
    public string Description { get; set; }
    public string ParentId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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