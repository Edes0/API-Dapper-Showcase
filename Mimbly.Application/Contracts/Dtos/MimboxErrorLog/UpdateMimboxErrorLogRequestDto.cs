namespace Mimbly.Application.Contracts.Dtos.MimboxErrorLog;

using Mimbly.Application.Common.Validators.MimboxErrorLog;
using Mimbly.CoreServices.Validation;

public class UpdateMimboxErrorLogRequestDto
{
    public bool Discarded { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new UpdateMimboxErrorLogRequestDtoValidator());
    }
}