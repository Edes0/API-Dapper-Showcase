namespace Mimbly.Application.Contracts.Dtos.AD;

using System.ComponentModel.DataAnnotations;
using CoreServices.Validation;
using FluentValidation;

public class AddCompanyDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public Guid ParentId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateCompanyValidator());
    }
}

internal class CreateCompanyValidator : AbstractValidator<AddCompanyDto>
{
    public CreateCompanyValidator()
    {
        RuleFor(company => company.Name).NotEmpty();
        RuleFor(company => company.Description).NotEmpty();
        RuleFor(company => company.ParentId).NotNull().NotEmpty();
    }
}