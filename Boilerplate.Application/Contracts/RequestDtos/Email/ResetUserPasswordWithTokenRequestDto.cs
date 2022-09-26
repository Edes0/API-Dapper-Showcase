namespace Boilerplate.Application.Contracts.RequestDtos.Email;

using Common.Validators;
using CoreServices.Validation;

public class ResetUserPasswordWithTokenRequestDto
{
    public string Token { get; set; } = null!;
    public string Password { get; set; } = null!;

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new ResetUserPasswordWithTokenRequestDtoValidator());
    }
}