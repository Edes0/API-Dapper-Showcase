namespace Mimbly.Application.Contracts.Dtos.MimboxContact;

using System;
using Mimbly.Application.Common.Validators.MimboxContact;
using Mimbly.CoreServices.Validation;

public class CreateMimboxContactRequestDto
{
    public string Title { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public Guid MimboxId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateMimboxContactRequestDtoValidator());
    }
}
