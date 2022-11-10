namespace Mimbly.Application.Contracts.Dtos.CompanyContact;

using System;
using Mimbly.CoreServices.Validation;

public class CreateCompanyContactRequestDto
{
    public string Title { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public Guid CompanyId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateCompanyContactRequestDtoValidator());
    }
}
