namespace Mimbly.Application.Contracts.Dtos.Mimbox;

using System.Threading.Tasks;
using Common.Validators;
using CoreServices.Validation;

public record CreateMimboxRequestDto
{
    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public int Age { get; init; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateMimboxRequestDtoValidator());
    }
}