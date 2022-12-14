namespace Mimbly.Application.Contracts.Dtos.Company;

using System;
using Mimbly.Application.Common.Validators.Company;
using Mimbly.CoreServices.Validation;

public class CreateCompanyRequestDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateCompanyRequestDtoValidator());
    }
}
