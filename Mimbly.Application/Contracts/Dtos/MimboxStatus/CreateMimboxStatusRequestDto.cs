namespace Mimbly.Application.Contracts.Dtos.MimboxStatus;

using System.Threading.Tasks;
using Mimbly.Application.Common.Validators.MimboxStatus;
using Mimbly.CoreServices.Validation;

public record CreateMimboxStatusRequestDto
{
    public string Name { get; init; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateMimboxStatusRequestDtoValidator());
    }
}
