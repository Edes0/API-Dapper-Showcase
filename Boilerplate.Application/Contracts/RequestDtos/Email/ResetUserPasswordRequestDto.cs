namespace Boilerplate.Application.Contracts.RequestDtos.Email;

using Common.Validators;
using CoreServices.Validation;

public class ResetUserPasswordRequestDto
{
    public string Email { get; set; } = null!;

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new ResetUserPasswordRequestDtoValidator());
    }
}