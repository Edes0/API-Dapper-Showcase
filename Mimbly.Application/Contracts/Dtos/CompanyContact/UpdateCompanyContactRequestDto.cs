namespace Mimbly.Application.Contracts.Dtos.CompanyContact;

using Mimbly.Application.Common.Validators.CompanyContact;
using Mimbly.CoreServices.Validation;

public class UpdateCompanyContactRequestDto
{
    public string Title { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public Guid CompanyId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UpdateCompanyContactRequestDtoValidator());
    }
}
