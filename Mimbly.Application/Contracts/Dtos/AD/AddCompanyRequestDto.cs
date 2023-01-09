namespace Mimbly.Application.Contracts.Dtos.AD;

using System.ComponentModel.DataAnnotations;
using Common.Validators.AD;
using CoreServices.Validation;

public record AddCompanyRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public Guid ParentId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new AddCompanyRequestDtoValidator());
    }
}