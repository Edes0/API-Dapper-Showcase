namespace Mimbly.Application.Contracts.Dtos.Company;

using Mimbly.Application.Common.Validators.Company;
using Mimbly.CoreServices.Validation;

public class UpdateCompanyRequestDto
{
    public string Name { get; set; }

    public Guid? ParentId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UpdateCompanyRequestDtoValidator());
    }
}
