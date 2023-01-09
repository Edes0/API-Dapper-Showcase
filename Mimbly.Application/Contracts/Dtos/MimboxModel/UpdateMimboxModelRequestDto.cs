namespace Mimbly.Application.Contracts.Dtos.MimboxModel;

using System.Threading.Tasks;
using Mimbly.Application.Common.Validators.MimboxModel;
using Mimbly.CoreServices.Validation;

public record UpdateMimboxModelRequestDto
{
    public string Name { get; init; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UpdateMimboxModelRequestDtoValidator());
    }
}
