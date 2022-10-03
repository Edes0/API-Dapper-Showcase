namespace Mimbly.Application.Contracts.Requests.Identity;

using System.Threading.Tasks;
using Mimbly.Application.Common.Validators;
using Mimbly.CoreServices.Validation;

public class RegisterUserRequestDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new RegisterUserRequestDtoValidator());
    }
}